using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Pigg.Contracts;
using Pigg.CQRS.ReadModel;
using Pigg.Data;
using PiggMvc.Models;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace PiggMvc
{
    public class IocConfig
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>();
            builder.RegisterType<WebLocalizator>().As<ILocalizator>();
            builder.RegisterType<PiggUow>().As<IPiggUow>();
            builder.RegisterType<CustomPageReader>().As<IReadModelFacade>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(container);
        }
    }
}