﻿using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Gry.Models.Game;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Game;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Gry.Controllers
{
    [Area("Gry")]
    public class GameController : ApplicationController, IGameActions
    {
        public static new string Name { get => "Game"; }

        private static string GameSettingsStoreKey = "GameSettings";
        private static string QuestionPoolStoreKey = "QuestionPool";

        private static string QuestionPoolViewName = "QuestionPool";
        private static string SettingsViewName = "Settings";

        public GameController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        protected async Task<GameQuestionFormViewModel> GetGameQuestionFormModel()
        {
            var model = new GameQuestionFormViewModel();

            var questionPool = TryRetrieveFromTempData<List<QuestionModel>>(QuestionPoolStoreKey);

            if( questionPool != null )
                model.GameQuestions = questionPool;

            model.Questions = QuestionModel.ConvertToList(await QuestionService.GetAll(UserId));

            return model;
        }

        public async Task<IActionResult> Index(int? gamemodeId) 
        {
            return RedirectToAction(nameof(IGameActions.SettingsCreateView), new { gamemodeId = gamemodeId });
        }

        public async Task<IActionResult> SettingsCreateView(int? gamemodeId)
        {
            GameSettingsViewModel model = new GameSettingsViewModel();

            model.Settings.GamemodeId = gamemodeId;

            return View(SettingsViewName, model);
        }

        public async Task<IActionResult> GameSettingsCreate(GameSettingsModel settings)
        {
            SaveToTempData(settings, GameSettingsStoreKey);
            return RedirectToAction(nameof(IGameActions.QuestionPoolCreateView));
        }

        public async Task<IActionResult> QuestionPoolCreateView()
        {
            return View(QuestionPoolViewName, await GetGameQuestionFormModel());
        }

        public async Task<IActionResult> GameQuestionCreate(int? id)
        {
            // bazowy model
            var model = await GetGameQuestionFormModel();
            // dodaję pytanie
            var question = await QuestionService.Get(id.Value, UserId);
            model.GameQuestions.Add((QuestionModel)question);
            // zapamiętuję dla następnego przekierowania
            SaveToTempData(model.GameQuestions, QuestionPoolStoreKey);

            return View(QuestionPoolViewName, model);
        }

        public async Task<IActionResult> GameQuestionDelete(int? id)
        {
            // bazowy model
            var model = await GetGameQuestionFormModel();
            // usuwam pytanie
            var q = new QuestionModel();
            q.Id = id;
            model.GameQuestions.Remove(q);
            // zapamiętuję dla następnego przekierowania
            SaveToTempData(model.GameQuestions, QuestionPoolStoreKey);

            return View(QuestionPoolViewName, model);
        }

        public async Task<IActionResult> QuestionPoolCreate()
        {
            // powinno i tak już być zapisane można sprawdzić poprawność
            return RedirectToAction(nameof(IGameActions.GameCreate));
        }

        public async Task<IActionResult> GameCreate()
        {
            // składanie
            GameModel game = new GameModel();
            game.Settings = TryRetrieveFromTempData<GameSettingsModel>(GameSettingsStoreKey);
            game.QuestionPool = TryRetrieveFromTempData<List<QuestionModel>>(QuestionPoolStoreKey);

            await GameService.Add(game.MapTo<CreateGameDto>(), UserId);

            return RedirectToAction("Index",HomeController.Name);
        }
    }
}