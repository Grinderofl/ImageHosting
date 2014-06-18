using System.Net;
using System.Timers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;

namespace ImageHosting
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Autofac();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var timer = new Timer(1000*60*5);
            timer.Elapsed += (sender, args) =>
            {
                var request = WebRequest.Create("/Refresh");
                request.GetResponse();
            };
            timer.Start();
        }

        private void Autofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof (MvcApplication).Assembly).InstancePerRequest();
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
