
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
        public static void Initialize(Container container)
        {
            try
            {
                //var container = SimpleDependencyInjector.Instance;
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

    public class SimpleDependencyInjector : IServiceProvider
    {
        public readonly Container Container;
        public SimpleDependencyInjector()
        {
            Container = Bootstrap();
        }

        internal Container Bootstrap()
        {
            var container = ContainerProvider.Instance;
            //InjectorInitializer.Initialize(container);
            return container;
        }

        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)Container).GetService(serviceType);
        }
    }

    public class ContainerProvider
    {
        private static volatile Container instance;
        private static object syncRoot = new Object();

        private ContainerProvider() { }

        public static Container Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Container();
                    }
                }
                return instance;
            }
        }
    }
}