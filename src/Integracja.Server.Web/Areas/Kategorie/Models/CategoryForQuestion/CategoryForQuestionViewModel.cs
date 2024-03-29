﻿using System.Collections.Generic;
using Integracja.Server.Web.Areas.Kategorie.Models.Shared;
using Integracja.Server.Web.Models.Shared.Alert;

namespace Integracja.Server.Web.Areas.Kategorie.Models.CategoryForQuestion
{
    public class CategoryForQuestionViewModel
    {
        public CategorySelectTableViewModel CategorySelectModel { get; set; } = new CategorySelectTableViewModel();

        public CategoryFormViewModel CategoryFormModel { get; set; } = new CategoryFormViewModel();

        public List<AlertModel> Alerts { get; set; }

        public CategoryForQuestionViewModel() : base()
        {
        }
    }
}
