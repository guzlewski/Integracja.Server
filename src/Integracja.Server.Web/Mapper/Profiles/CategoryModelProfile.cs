using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Category;

namespace Integracja.Server.Web.Mapper.Profiles
{
    public class CategoryModelProfile : Profile
    {

        public CategoryModelProfile()
        {
            CreateMap<CategoryModel, CreateCategoryDto>();

            CreateMap<CategoryModel, EditCategoryDto>();

            CreateMap<CategoryDto, CategoryModel>();

            CreateMap<DetailCategoryDto, CategoryModel>();


            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();
        }

    }
}
