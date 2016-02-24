using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Umc.Core.IoC;

namespace WebForm_DependencyInjection
{
    public class Global : System.Web.HttpApplication
    {
        private static IFrameworkContainer container;

        protected void Application_Start(object sender, EventArgs e)
        {
            container = new FrameworkContainerForUnity();

            var catalog = new Umc.Core.IoC.Catalog.FrameworkAssemblyCatalog(Assembly.GetExecutingAssembly());
            var visitor = new Umc.Core.IoC.Configuration.FrameworkDependencyVisitor(catalog);
            var resolver = new Umc.Core.IoC.Unity.FrameworkCompositionResolverForUnity((FrameworkContainerForUnity)container, visitor.VisitTypes());
            resolver.Compose();

            Application.Lock();
            Application["container"] = container;
            Application.UnLock();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}