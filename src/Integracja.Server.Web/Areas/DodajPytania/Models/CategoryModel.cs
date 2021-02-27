using Integracja.Server.Infrastructure.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Web.Areas.DodajPytania.Models
{
    public class CategoryModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Podaj nazwę kategorii")]
        public string Name { get; set; }
        public int QuestionsCount { get; set; }

        public static List<CategoryModel> ToList( IEnumerable<CategoryGetAll> dtoList )
        {
            var mapper = Mappers.WebAutoMapper.Initialize();
            List<CategoryModel> resultList = new List<CategoryModel>();
            foreach (var dtoCategory in dtoList)
                resultList.Add(mapper.Map<CategoryModel>(dtoCategory));
            return resultList;
        }
    }
}
