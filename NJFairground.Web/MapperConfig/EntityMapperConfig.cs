
namespace NJFairground.Web.MapperConfig
{
    using AutoMapper;
    using NJFairground.Web.Areas.Admin.Models;
    using NJFairground.Web.Data.Context;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using System;
    using System.Linq;

    public class EntityMapperConfig : Profile
    {
        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        /// <value>
        /// The name of the profile.
        /// </value>
        public override string ProfileName
        {
            get
            {
                return "EntityMapperConfig";
            }
        }

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            try
            {
                Mapper.CreateMap<SplashImage, SplashImageModel>()
                   .IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

                Mapper.CreateMap<HitCounter, HitCounterModel>()
                   .IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

                Mapper.CreateMap<Banner, BannerModel>()
                   .IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

                Mapper.CreateMap<BannerItem, BannerItemModel>()
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => CommonUtility.ResolveServerUrl(src.ImageUrl, false)))
                    .IgnoreAllNonExisting();
                Mapper.CreateMap<BannerItemModel, BannerItem>().IgnoreAllNonExisting();

                Mapper.CreateMap<PageBanner, PageBannerModel>()
                   .IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

                Mapper.CreateMap<NJFairground.Web.Data.Context.Page, PageModel>()
                    //.ForMember(dest => dest.PageBanner, opt => opt.ResolveUsing<CustomPageBannerResolver>())
                    .IgnoreAllNonExisting();
                Mapper.CreateMap<PageModel, NJFairground.Web.Data.Context.Page>().IgnoreAllNonExisting();

                Mapper.CreateMap<PageItem, PageItemModel>()
                    //.ForMember(dest => dest.PageBanner, opt => opt.ResolveUsing<CustomPageItemBannerResolver>())
                    .IgnoreAllNonExisting();
                Mapper.CreateMap<PageItemModel, PageItem>().IgnoreAllNonExisting();

                Mapper.CreateMap<UserInfo, UserInfoModel>()
                    .ForMember(dest => dest.FavoritePages, opt => opt.MapFrom(src =>
                        src.FavoritePages.Where(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList()))
                    .ForMember(dest => dest.UserImages, opt => opt.MapFrom(src =>
                        src.UserImages.Where(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList()))
                    .IgnoreAllNonExisting();

                Mapper.CreateMap<UserInfoModel, UserInfo>().IgnoreAllNonExisting();

                Mapper.CreateMap<UserImage, UserImageModel>()
                    .ForMember(dest => dest.IsFavorite, opt => opt.MapFrom(src => (src.FavoriteImages.Count > 0)))
                    //.ForMember(dest => dest.UserImageUrl, opt => opt.MapFrom(src => CommonUtility.ResolveServerUrl(src.UserImageUrl, false)))
                    .IgnoreAllNonExisting();
                Mapper.CreateMap<UserImageModel, UserImage>().IgnoreAllNonExisting();

                Mapper.CreateMap<FavoriteImage, FavoriteImageModel>()
                   .IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

                Mapper.CreateMap<FavoritePage, FavoritePageModel>()
                   .IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

                Mapper.CreateMap<Event, EventModel>()
                   .IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

                Mapper.CreateMap<SchedularSchema, EventModel>()
                    .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.id))
                    .ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src => src.title))
                    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.start))
                    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.end))
                    .ForMember(dest => dest.EventDesc, opt => opt.MapFrom(src => src.description))
                    .IgnoreAllNonExisting();

                Mapper.CreateMap<EventModel, SchedularSchema>()
                    .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.EventId))
                    .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.EventTitle))
                    .ForMember(dest => dest.start, opt => opt.MapFrom(src => src.StartDate))
                    .ForMember(dest => dest.end, opt => opt.MapFrom(src => src.EndDate))
                    .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.EventDesc))
                    .IgnoreAllNonExisting();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
        }
    }
}