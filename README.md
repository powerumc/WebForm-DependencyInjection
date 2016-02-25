# WebForm-DependencyInjection
Showcase how to inject dependency in the ASP.NET WebForm

Generally, We doesn't use the `Depenedency Injection` in the ASP.NET WebForm. Because, ASP.NET WebForm doesn't supports it.

But, in the WebForm, We can use the `Dependency Injection` with Umc.Core Frameworks. Umc.Core has implemented `IHttpHandlerFactory` interface.

## Setup web.config

First, We have to add handler factory in the web.config.

```xml
<handlers>
  <add name="WebFormPageHandlerFactory" verb="*" path="*.aspx"  type="WebForm_DependencyInjection.FrameworkContainerPageHandlerFactory"/>
</handlers>
```

## Setup IoC Container and Composition

We have to add code for setup IoC container and compose its.

```C#
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
  }  
```

## Use it.

We have a IEmailService for example. It is so simple code.

```C#
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Umc.Core;

namespace WebForm_DependencyInjection.Services
{
    public interface IEmailService
    {
        bool Send(string to, string contents);
    }

    [DependencyContract(typeof(IEmailService))]
    public class EmailService : IEmailService
    {
        public bool Send(string to, string contents)
        {
            HttpContext.Current.Response.Write("Send email.");
            return true;
        }
    }
}
```

Last, We just inject in your code. `[DependencyInjection]` attribute is the property get, set. If we don't use, `EmailService` property has null value, throw `NullReferenceException`

It is all.!!

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Umc.Core;
using WebForm_DependencyInjection.Services;

namespace WebForm_DependencyInjection
{
    public partial class Index : System.Web.UI.Page
    {
        // Here.. Injection for IEmailService.
        [DependencyInjection]
        protected IEmailService EmailService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            EmailService.Send("", "");
        }
    }
}
```
