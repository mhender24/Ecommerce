using Ecommerce.DataAccessLayer;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Ecommerce.Controllers
{
    public class CustomControllerFactory : IControllerFactory
    {
        private readonly string _projectNamespace;
        private readonly string _controllerNamespace;

        public CustomControllerFactory(string projectNamespace, string controllerNamespace)
        {
            _projectNamespace = projectNamespace;
            _controllerNamespace = controllerNamespace;
        }
        public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            Type controllerType = Type.GetType(string.Concat(_projectNamespace, ".", _controllerNamespace, ".", controllerName, "Controller"));
            var contextParams = new object[1];

            switch (controllerType.Name)
            {
                case "SupplierController":
                    contextParams[0] = new SupplierRepository(new Data());
                    break;
                case "ProductController":
                    contextParams[0] = new GenericUnitOfWork();
                    break;
                default:
                    break;
            }
            IController controller;
            controller = contextParams[0] == null ? Activator.CreateInstance(controllerType) as Controller : Activator.CreateInstance(controllerType, new[] { contextParams[0] }) as Controller;
            return controller;
        }

        public System.Web.SessionState.SessionStateBehavior GetControllerSessionBehavior(
           System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }
        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}