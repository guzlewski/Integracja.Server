using Integracja.Server.Infrastructure.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Web.Models.Shared.Category
{
    public class CategoryModel
    {
        [Required]
        public int? Id { get; set; }
        [Required(ErrorMessage = "Podaj nazwę kategorii")]
        public string Name { get; set; }
        public int QuestionsCount { get; set; }
        public bool IsPublic { get; set; }

        public EditCategoryDto ToCategoryModify()
        {
            var mapper = Mappers.WebAutoMapper.Initialize();
            return mapper.Map<EditCategoryDto>(this);
        }
        public CreateCategoryDto ToCategoryAdd()
        {
            var mapper = Mappers.WebAutoMapper.Initialize();
            return mapper.Map<CreateCategoryDto>(this);
        }

        public static CategoryModel ConvertToCategoryModel( DetailCategoryDto category )
        {
            var mapper = Mappers.WebAutoMapper.Initialize();
            return mapper.Map<CategoryModel>(category);
        }
    }
}
