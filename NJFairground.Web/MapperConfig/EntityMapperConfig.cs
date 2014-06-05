
namespace NJFairground.Web.MapperConfig
{
    using System;
    using AutoMapper;
    using NJFairground.Web.Models;
    using NJFairground.Web.Data.Context;
    using NJFairground.Web.Utilities;

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
                Mapper.CreateMap<NJFairground.Web.Data.Context.Page, PageModel>()
                    .IgnoreAllNonExisting().MapBothWays().IgnoreAllNonExisting();
                Mapper.CreateMap<NJFairground.Web.Data.Context.PageItem, PageItemModel>()
                   .IgnoreAllNonExisting().MapBothWays().IgnoreAllNonExisting();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
        }
    }
}