using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using TargetApp.Modules;

namespace TargetApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        IContainer container;

        protected void Application_Start()
        {
            container = RegisterContainer();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected IContainer RegisterContainer()
        {
            var builder = new ContainerBuilder();

            var thisAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(thisAssembly);
            builder.RegisterApiControllers(thisAssembly);

            builder.RegisterModule(new ApplicationModule());

            var container = builder.Build();

            // set mvc resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // set webapi resolver
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            return container;
        }
    }
}
