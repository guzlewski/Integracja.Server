using Integracja.Server.Infrastructure.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Web.Models.Shared.Category
{
    public class CategoryModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Podaj nazwę kategorii")]
        public string Name { get; set; }
        public int QuestionsCount { get; set; }
        public bool IsPublic { get; set; }

        public CategoryModify ToCategoryModify()
        {
            var mapper = Mappers.WebAutoMapper.Initialize();
            return mapper.Map<CategoryModify>(this);
        }
        public CategoryAdd ToCategoryAdd()
        {
            var mapper = Mappers.WebAutoMapper.Initialize();
            return mapper.Map<CategoryAdd>(this);
        }

        public static CategoryModel ConvertToCategoryModel( CategoryGet category )
        {
            var mapper = Mappers.WebAutoMapper.Initialize();
            return mapper.Map<CategoryModel>(category);
        }

        public static List<CategoryModel> ConvertToList( IEnumerable<CategoryGetAll> dtoList )
        {
            var mapper = Mappers.WebAutoMapper.Initialize();
            List<CategoryModel> resultList = new List<CategoryModel>();
            foreach (var dtoCategory in dtoList)
                resultList.Add(mapper.Map<CategoryModel>(dtoCategory));
            return resultList;
        }
    }
}
