using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Pigg.CQRS;
using Pigg.CQRS.CommandHandlers;
using Pigg.CQRS.Commands;
using Pigg.CQRS.Domain;
using Pigg.CQRS.EventHandlers;
using Pigg.CQRS.Events;
using Pigg.CQRS.ReadModel;
using Pigg.Data;
using Pigg.Data.Repositories;
using PiggMvc3App.Helpers;
using System.Web.Security;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using PiggMvc3App.Models;
using System.Security.Principal;

namespace PiggMvc3App
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });
            routes.IgnoreRoute("{*robotstxt}", new { robotstxt = @"(.*/)?robots.txt(/.*)?" });
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" }); 

            // Submission/* 
            routes.MapRoute(
                  "Pages",
                  "Pages/{method}",
                  new { controller = "Pages", action = "Handle", method = "" }
                  );

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" },  // Parameter defaults
                new { controller = @"[^\.]*" }                          // Parameter contraints
            );

            routes.MapRoute(
                "OpenIdDiscover",
                "Auth/Discover");            

            //// catch all
            //routes.MapRoute("Home", "{*url}",
            //    new { controller = "Home", action = "Index" }
            //); 

        }

        public override void Init()
        {
            AuthenticateRequest += MvcApplication_AuthenticateRequest;
            //this.PostAuthenticateRequest += new EventHandler(MvcApplication_PostAuthenticateRequest);
            base.Init();
        }

        protected void Application_Start()
        {
            //AjaxHelper.GlobalizationScriptPath = "http://ajax.microsoft.com/ajax/4.0/1/globalization/";

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.Add(new MobileCapableWebFormViewEngine());

            InitContainer();            

            // The following lines trigger the creation of 
            // the security database file 
            // aspnetdb.mdf, if it does not exist.
            // This assumes that the database used is SQL Express 
            // (that is the default, as per machine.config).
            // You must still add the users to their roles. 
            // You can do this by using the ASP.NET Configuration tool.
            if (!Roles.RoleExists(Constants.AdminRole))
                Roles.CreateRole(Constants.AdminRole);
            if (!Roles.RoleExists(Constants.WriterRole))
                Roles.CreateRole(Constants.WriterRole);

            string[] administrators = Roles.GetUsersInRole(Constants.AdminRole);
            if (administrators.Length == 0)
                createDefaultAdminUser();

            IBus bus = new Bus();
            var storage = new EventStore(bus);
            var repo = new Repository<CustomPage>(storage);
            var commands = new CustomPageCommandHandlers(new CustomPageRepository(new DatabaseFactory()), repo);
            bus.RegisterHandler<CreateCustomPage>(commands.Handle);
            bus.RegisterHandler<UpdateCustomPage>(commands.Handle);
            bus.RegisterHandler<DeleteCustomPage>(commands.Handle);

            var customPageReporting = new CustomPageEventHandlers();

            bus.RegisterHandler<CustomPageCreated>(customPageReporting.Handle);
            bus.RegisterHandler<CustomPageDeleted>(customPageReporting.Handle);
            bus.RegisterHandler<CustomPageUpdated>(customPageReporting.Handle);

            ServiceLocator.Bus = bus;
        }

        private static void InitContainer()
        {
            // Load unity configurations            
            var container = new UnityContainer().LoadConfiguration();

            // Register the relevant types for the               
            // container here through classes or configuration 
            // examples:               
            //container.RegisterType<IDatabaseFactory, DatabaseFactory>();

            //container.RegisterType<IBus, Bus>();
            container.RegisterType<ICustomPageReader, CustomPageReader>();

            //container.RegisterType<Pigg.Contracts.Repositories.ICustomPageRepository, Pigg.Data.CustomPageRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            var factory = new UnityControllerFactory(container); 
            ControllerBuilder.Current.SetControllerFactory(factory);
        }

        private void createDefaultAdminUser()
        {
            if (Roles.IsUserInRole("admin", Constants.AdminRole))
                Roles.RemoveUserFromRole("admin", Constants.AdminRole);
            Membership.DeleteUser("admin");
            Membership.CreateUser("admin", "password");
            Roles.AddUserToRole("admin", Constants.AdminRole);
        }

        void MvcApplication_AuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;
            var encTicket = authCookie.Value;
            if (String.IsNullOrEmpty(encTicket)) return;
            var ticket = FormsAuthentication.Decrypt(encTicket);
            var id = new PiggIdentity(ticket);
            var prin = new GenericPrincipal(id, null);
            HttpContext.Current.User = prin;
        }
    }
}