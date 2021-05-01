using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Gry.Models.Game;
using Integracja.Server.Web.Areas.Gry.Models.Shared;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Game;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Gry.Controllers
{
    [Area("Gry")]
    public class GameController : ApplicationController, IGameActions, IGameSettingsValidation
    {
        public static new string Name { get => "Game"; }

        private const string GameSettingsStoreKey = "GameSettings";
        private const string QuestionPoolStoreKey = "QuestionPool";

        private const string QuestionPoolViewName = "QuestionPool";
        private const string SettingsViewName = "Settings";

        public GameController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        protected async Task<GameQuestionFormViewModel> GetGameQuestionFormModel()
        {
            var model = new GameQuestionFormViewModel();

            var questionPool = TryRetrieveFromTempData<List<QuestionModel>>(QuestionPoolStoreKey);

            if (questionPool != null)
                model.GameQuestions = questionPool;

            model.Questions = Mapper.Map<List<QuestionModel>>(await QuestionService.GetAll<QuestionModel>(UserId));

            return model;
        }

        public Task<IActionResult> Index(int? gamemodeId)
        {
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IGameActions.SettingsCreateView), new { gamemodeId = gamemodeId }));
        }

        public Task<IActionResult> SettingsCreateView(int gamemodeId)
        {
            GameSettingsFormViewModel model = new GameSettingsFormViewModel();

            model.Settings.Gamemode.Id = gamemodeId;

            return Task.FromResult<IActionResult>(View(SettingsViewName, model));
        }

        public Task<IActionResult> GameSettingsCreate(GameSettingsModel settings)
        {
            settings.SetTimeZone(TimeZoneConverter.TZConvert.GetTimeZoneInfo("Europe/Warsaw"));
            SaveToTempData(settings, GameSettingsStoreKey);
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IGameActions.QuestionPoolCreateView)));
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
            var question = await QuestionService.Get<QuestionModel>(id.Value, UserId);
            model.GameQuestions.Add(question);
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

        public Task<IActionResult> QuestionPoolCreate()
        {
            // powinno i tak już być zapisane można sprawdzić poprawność
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(IGameActions.GameCreate)));
        }

        public async Task<IActionResult> GameCreate()
        {
            // składanie
            GameModel game = new GameModel();
            game.Settings = TryRetrieveFromTempData<GameSettingsModel>(GameSettingsStoreKey);
            game.QuestionPool = TryRetrieveFromTempData<List<QuestionModel>>(QuestionPoolStoreKey);

            var createGameDto = Mapper.Map<CreateGameDto>(game);

            if (game.Settings.MaxPlayersCount == 0)
            {
                createGameDto.MaxPlayers = null;
            }

            await GameService.Add(createGameDto, UserId);

            return RedirectToAction("Index", HomeController.Name);
        }

        public async Task<IActionResult> GameDelete(int gameId)
        {
            await GameService.Delete(gameId, UserId);
            SetAlert(GameAlert.DeleteSuccess());
            return RedirectToAction("Index", HomeController.Name);
        }

        public async Task<IActionResult> GameRead(int gameId)
        {
            var model = new GameCardViewModel();

            GameModel game = await GameService.Get<GameModel>(gameId, UserId);

            model.Game = game;

            return View("GameCard", model);
        }


        public IActionResult VerifyDates(
            [Bind(Prefix = "Settings.StartDate")] string startDate,
            [Bind(Prefix = "Settings.StartTime")] string startTime,
            [Bind(Prefix = "Settings.EndDate")] string endDate,
            [Bind(Prefix = "Settings.EndTime")] string endTime)
        {
            if (startDate == null || startTime == null || endDate == null || endTime == null)
                return Json(true); // pominięcie walidacji jeśli się nie są podane wszystkie parametry

            if (!DateTimeOffset.TryParse(startDate + " " + startTime, out DateTimeOffset start) 
                || !DateTimeOffset.TryParse(endDate + " " + endTime, out DateTimeOffset end))
                return Json($"Podane czasy są w złym formacie");

            var now = DateTimeOffset.Now;

            if( start < now )
            {
                return Json($"Podany czas rozpoczęcia jest w przeszłości");
            }
            if( start >= end )
            {
                return Json($"Czas rozpoczęcia nie może być po czasie zakończenia");
            }
            if( end > now.AddYears(1) )
            {
                return Json($"Gra może się zakończyć najpóźniej za rok");
            }

            return Json(true);
        }
        
    }
}