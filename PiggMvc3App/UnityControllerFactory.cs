using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using System.Web.Routing;

namespace PiggMvc3App
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        private IUnityContainer _container;
        private IControllerFactory _innerFactory;

        public UnityControllerFactory(IUnityContainer container)
            : this(container, new DefaultControllerFactory())
        {
        }

        protected UnityControllerFactory(IUnityContainer container, IControllerFactory innerFactory)
        {
            _container = container;
            _innerFactory = innerFactory;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                Controller ctrl = null;
                try
                {
                    ctrl = _container.Resolve<Controller>(controllerName.ToLowerInvariant());
                }
                catch (Exception)
                {
                    ctrl = _innerFactory.CreateController(requestContext, controllerName) as Controller;
                }
                ctrl.ActionInvoker = new UnityActionInvoker(_container);
                return ctrl;
            }
            catch (Exception) { }
            return null;
        }

        //protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        //{
        //    Controller controller = null; 
        //    if (controllerType != null) 
        //    {
        //        if (!typeof(IController).IsAssignableFrom(controllerType)) 
        //        { 
        //            throw new ArgumentException(string.Format("Type requested is not a controller: {0}", controllerType.Name), "controllerType"); 
        //        }
        //        controller = _container.Resolve(controllerType) as Controller; 
        //        controller.ActionInvoker = new UnityActionInvoker(_container);
        //    } 
            
        //    return controller; 
        //}

        public override void ReleaseController(IController controller)
        {
            _container.Teardown(controller);
        }
    }
}