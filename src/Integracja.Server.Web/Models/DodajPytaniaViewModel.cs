using Integracja.Server.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Integracja.Server.Web.Models
{
    // potrzebne żeby zaagregować parę modeli które mamy na jednej stronie przykładowo DodajPytania gdzie potrzeba wyświetlać kategorię i będziemy dodawać pytania/odpowiedzi
    public class DodajPytaniaViewModel : PageModel
    {
        
        public IEnumerable<CategoryGetAll> Categories { get; set; }
        public CategoryAdd NewCategory { get; set; }
        public QuestionViewModel QuestionViewModel { get; set; }

        public DodajPytaniaViewModel() : base()
        {
            Categories = new List<CategoryGetAll>();
            QuestionViewModel = new QuestionViewModel("Twoje pytanie", true);
            NewCategory = new CategoryAdd();
        }
    }
}
