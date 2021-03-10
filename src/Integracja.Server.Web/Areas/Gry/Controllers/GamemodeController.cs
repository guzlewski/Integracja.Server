﻿using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Areas.Gry.Models.Gamemode;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Enums;
using Integracja.Server.Web.Models.Shared.Gamemode;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Gry.Controllers
{
    [Area("Gry")]
    public class GamemodeController : ApplicationController, IGamemodeActions
    {
        public GamemodeViewModel Model { get; set; }
        public static new string Name { get => "Gamemode"; }

        public GamemodeController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public async Task<IActionResult> Index(int? id)
        {
            Model = new GamemodeViewModel();
            Model.Gamemodes = GamemodeModel.MapToList<GamemodeDto>( await GamemodeService.GetAll(UserId));
            Model.SelectedGamemode = id;
            return View("Gamemodes", Model);
        }

        public async Task<IActionResult> GamemodeCreate(GamemodeModel gamemode)
        {
            int gamemodeId = await GamemodeService.Add(gamemode.MapTo<CreateGamemodeDto>(), UserId);
            return RedirectToAction("Index", new { id = gamemodeId });
        }

        public async Task<IActionResult> GamemodeUpdate(GamemodeModel gamemode)
        {
            int gamemodeId = await GamemodeService.Update( gamemode.Id, gamemode.MapTo<EditGamemodeDto>(), UserId);
            return RedirectToAction("Index", new { id = gamemodeId });
        }
        public async Task<IActionResult> GamemodeRead(int? id)
        {
            return RedirectToAction("Index", new { id = id });
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
                GamemodeModel gamemode = (GamemodeModel)await GamemodeService.Get(id.Value, UserId);
                formModel.Gamemode = gamemode;
                return View("Gamemode", formModel);
            }
            else return RedirectToAction("Index");
        }

        public async Task<IActionResult> GamemodeCreateView()
        {
            var formModel = new GamemodeFormViewModel();
            formModel.ViewMode = ViewMode.Creating;
            return View("Gamemode", formModel);
        }

        public Task<IActionResult> GotoGameCreate()
        {
            throw new NotImplementedException();
        }
    }
}
