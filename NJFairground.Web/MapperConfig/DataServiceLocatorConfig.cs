
namespace NJFairground.Web.MapperConfig
{
    using System;
    using AutoMapper;
    using FluentValidation;
    using NJFairground.Web.Data.Implementation;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models.ValidationRules.Factory;
    using NJFairground.Web.Utilities;
    using NJFairground.Web.Models;
    using SimpleInjector;
    using SimpleInjector.Extensions;
    using SimpleInjector.Advanced;
    using SimpleInjector.Packaging;
    using NJFairground.Web.Models.ValidationRules;

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
                container.RegisterManyForOpenGeneric(typeof(IValidator<>), typeof(IValidator<>).Assembly);
                //container.Register<IValidator<PageItemModel>, PageItemModelValidation>(Lifestyle.Singleton);
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
                container.Register<IPageDataRepository, PageDataRepository>();
                container.Register<IPageItemDataRepository, PageItemDataRepository>();
                container.Register<IEventDataRepository, EventDataRepository>();
                container.Register<IUserInfoDataRepository, UserInfoDataRepository>();
                container.Register<IFavoriteImageDataRepository, FavoriteImageDataRepository>();
                container.Register<IFavoritePageDataRepository, FavoritePageDataRepository>();
                container.Register<IUserImageDataRepository, UserImageDataRepository>();
                container.Register<IBannerDataRepository, BannerDataRepository>();
                container.Register<IBannerItemDataRepository, BannerItemDataRepository>();
                container.Register<IPageBannerDataRepository, PageBannerDataRepository>();
                container.Register<ISplashImageDataRepository, SplashImageDataRepository>();
                container.Register<IHitCounterDataRepository, HitCounterDataRepository>();
                container.Register<IDeviceRegistryDataRepository, DeviceRegistryDataRepository>();
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