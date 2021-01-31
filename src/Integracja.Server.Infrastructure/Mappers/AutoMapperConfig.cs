using AutoMapper;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Infrastructure.DTO;

namespace Integracja.Server.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>()
                    .ForMember(dto => dto.AuthorNickname, m => m.MapFrom(c => c.Author.UserName));
                cfg.CreateMap<CategoryDto, Category>();
                cfg.CreateMap<Category, CategoryDetailsDto>();
            })
            .CreateMapper();
        }
    }
}
