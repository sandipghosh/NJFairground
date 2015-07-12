
namespace NJFairground.Web.MapperConfig
{
    using AutoMapper;
    using FluentValidation;
    using NJFairground.Web.Data.Implementation;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using NJFairground.Web.Models.ValidationRules;
    using NJFairground.Web.Models.ValidationRules.Factory;
    using NJFairground.Web.Utilities;
    using SimpleInjector;
    using SimpleInjector.Advanced;
    using SimpleInjector.Packaging;
    using System;

    public class DataServiceLocatorConfig : IPackage
    {
        /// <summary>
        /// Registers the set of services in the specified <paramref name="container" />.
        /// </summary>
        /// <param name="container">The container the set of services is registered into.</param>
        public void RegisterServices(Container container)
        {
            try
            {
                this.RegisterValidationService(container);
                this.RegisterServiceLocator(container);
                this.AddMapperProfile(container);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(container);
            }
        }

        /// <summary>
        /// Registers the service locator.
        /// </summary>
        /// <param name="container">The container.</param>
        private void RegisterValidationService(Container container)
        {
            try
            {
                container.Register<IServiceProvider, SimpleDependencyInjector>(Lifestyle.Singleton);
                container.Register<IValidatorFactory, FluentValidatorFactory>(Lifestyle.Singleton);

                container.Register<IValidator<PageItemModel>, PageItemModelValidation>(Lifestyle.Singleton);
                container.Register<IValidator<PageModel>, PageModelValidation>(Lifestyle.Singleton);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(container);
            }
        }

        /// <summary>
        /// Registers the service locater.
        /// </summary>
        /// <param name="container">The container.</param>
        private void RegisterServiceLocator(Container container)
        {
            try
            {
                container.Register<IPageDataRepository, PageDataRepository>(Lifestyle.Singleton);
                container.Register<IPageItemDataRepository, PageItemDataRepository>(Lifestyle.Singleton);
                container.Register<IEventDataRepository, EventDataRepository>(Lifestyle.Singleton);
                container.Register<IUserInfoDataRepository, UserInfoDataRepository>(Lifestyle.Singleton);
                container.Register<IFavoriteImageDataRepository, FavoriteImageDataRepository>(Lifestyle.Singleton);
                container.Register<IFavoritePageDataRepository, FavoritePageDataRepository>(Lifestyle.Singleton);
                container.Register<IUserImageDataRepository, UserImageDataRepository>(Lifestyle.Singleton);
                container.Register<IBannerDataRepository, BannerDataRepository>(Lifestyle.Singleton);
                container.Register<IBannerItemDataRepository, BannerItemDataRepository>(Lifestyle.Singleton);
                container.Register<IPageBannerDataRepository, PageBannerDataRepository>(Lifestyle.Singleton);
                container.Register<ISplashImageDataRepository, SplashImageDataRepository>(Lifestyle.Singleton);
                container.Register<IHitCounterDataRepository, HitCounterDataRepository>(Lifestyle.Singleton);
                container.Register<IHitCounterDetailDataRepository, HitCounterDetailDataRepository>();
                container.Register<IDeviceRegistryDataRepository, DeviceRegistryDataRepository>(Lifestyle.Singleton);
                container.Register<INotificationLogDataRepository, NotificationLogDataRepository>(Lifestyle.Singleton);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(container);
            }
        }

        /// <summary>
        /// Adds the mapper profile.
        /// </summary>
        /// <param name="container">The container.</param>
        private void AddMapperProfile(Container container)
        {
            try
            {
                container.AppendToCollection(typeof(Profile),
                    Lifestyle.Singleton.CreateRegistration(typeof(Profile),
                    typeof(EntityMapperConfig), container));
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(container);
            }
        }
    }
}