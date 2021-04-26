using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace PiggMvc3App
{
    public class UnityActionInvoker : ControllerActionInvoker
    {
        private readonly IUnityContainer _container;

        public UnityActionInvoker(IUnityContainer kernel)
        {
            _container = kernel;
        }

        protected override ActionExecutedContext InvokeActionMethodWithFilters(
            ControllerContext controllerContext,
            IList<IActionFilter> filters,
            ActionDescriptor actionDescriptor,
            IDictionary<string, object> parameters)
        {
            foreach (IActionFilter actionFilter in filters)
            {
                _container.BuildUp(actionFilter.GetType(), actionFilter);
            }
            return base.InvokeActionMethodWithFilters(
                controllerContext, filters, actionDescriptor, parameters);
        }
    }


}