using System.Linq;
using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Infrastructure.Models;

namespace Integracja.Server.Infrastructure.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            int userId = 0;

            CreateMap<Category, CategoryDto>()
                .ForMember(
                    categoryDto => categoryDto.QuestionsCount,
                    opt => opt.MapFrom(category => category.Questions.Where(question => !question.IsDeleted && (question.IsPublic || question.OwnerId == userId)).Count()));

            CreateMap<Category, DetailCategoryDto>()
                .ForMember(
                    detailCategoryDto => detailCategoryDto.Questions,
                    opt => opt.MapFrom(category => category.Questions.Where(question => !question.IsDeleted && (question.IsPublic || question.OwnerId == userId))));

            CreateMap<CreateCategoryDto, Category>();

            CreateMap<EditCategoryDto, Category>();
        }
    }
}
