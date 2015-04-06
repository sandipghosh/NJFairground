
namespace NJFairground.Web
{
    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;
    using SimpleInjector.Integration.WebApi;
    using System.Reflection;
    using System.Web.Mvc;
    using System;
    using NJFairground.Web.Utilities;
    using System.Web.Http;

    public class InjectorInitializer
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            try
            {
                var container = new Container();
                container.RegisterPackages();

                container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
                container.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

                container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
                container.RegisterMvcIntegratedFilterProvider();
                container.Verify();

                MapperInitializer.RegisterMapper(container);
                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
                System.Web.Mvc.DependencyResolver.SetResolver(new SimpleInjector.Integration.Web.Mvc.SimpleInjectorDependencyResolver(container));
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
        }
    }
}