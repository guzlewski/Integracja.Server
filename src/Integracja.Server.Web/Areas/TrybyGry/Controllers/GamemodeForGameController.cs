﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Gry.Controllers;
using Integracja.Server.Web.Areas.TrybyGry.Models.GamemodeForGame;
using Integracja.Server.Web.Areas.TrybyGry.Models.Shared;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Alert;
using Integracja.Server.Web.Models.Shared.Enums;
using Integracja.Server.Web.Models.Shared.Gamemode;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.TrybyGry.Controllers
{
    [Area("TrybyGry")]
    public class GamemodeForGameController : ApplicationController, IGamemodeForGameActions
    {
        public GamemodeForGameViewModel Model { get; set; }
        public static new string Name { get => "GamemodeForGame"; }

        public GamemodeForGameController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public async Task<IActionResult> Index(int? id)
        {
            Model = new GamemodeForGameViewModel();
            Model.Gamemodes = (List<GamemodeModel>)await GamemodeService.GetAll<GamemodeModel>(UserId);
            Model.SelectedGamemode = id;
            Model.Alerts = GetAlerts();
            return View("GamemodeForGame", Model);
        }

        public async Task<IActionResult> GamemodeCreate(GamemodeModel gamemode)
        {
            int gamemodeId = await GamemodeService.Add(Mapper.Map<CreateGamemodeDto>(gamemode), UserId);
            return RedirectToAction("Index", new { id = gamemodeId });
        }

        public async Task<IActionResult> GamemodeUpdate(GamemodeModel gamemode)
        {
            int gamemodeId = await GamemodeService.Update(gamemode.Id, Mapper.Map<EditGamemodeDto>(gamemode), UserId);
            return RedirectToAction("Index", new { id = gamemodeId });
        }
        public Task<IActionResult> GamemodeRead(int? id)
        {
            return Task.FromResult<IActionResult>(RedirectToAction("Index", new { id = id }));
        }

        public async Task<IActionResult> GamemodeDelete(int? id)
        {
            if (id.HasValue)
            {
                await GamemodeService.Delete(id.Value, UserId);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GamemodeUpdateView(int? id)
        {
            if (id.HasValue)
            {
                var formModel = new GamemodeFormViewModel();
                formModel.ViewMode = ViewMode.Updating;
                GamemodeModel gamemode = await GamemodeService.Get<GamemodeModel>(id.Value, UserId);
                formModel.Gamemode = gamemode;
                return View("Gamemode", formModel);
            }
            else return RedirectToAction("Index");
        }

        public Task<IActionResult> GamemodeCreateView()
        {
            var formModel = new GamemodeFormViewModel();
            formModel.ViewMode = ViewMode.Creating;
            return Task.FromResult<IActionResult>(View("Gamemode", formModel));
        }

        public Task<IActionResult> GotoGameCreate(int? gamemodeId)
        {
            if (gamemodeId == null)
            {
                SetAlert(new AlertModel(AlertType.Warning, "Musisz wybrać lub utworzyć tryb gry."));
                return Task.FromResult<IActionResult>(RedirectToAction("Index"));
            }
            else return Task.FromResult<IActionResult>(RedirectToAction("Index", GameController.Name, new { area = "Gry", gamemodeId = gamemodeId }));

        }
    }
}
