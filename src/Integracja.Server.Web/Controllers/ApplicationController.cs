using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Repositories;
using Integracja.Server.Infrastructure.Services.Implementations;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Integracja.Server.Web.Models.Shared.Alert;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Integracja.Server.Web.Controllers
{
    [Authorize]
    public class ApplicationController : Controller, IAlerts
    {
        public ApplicationController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper ) : base()
        { 
            _context = dbContext;
            _userManager = userManager;
            _userId = null;
            _mapper = mapper;
        }

        private ApplicationDbContext _context;
        protected ApplicationDbContext DbContext { get => _context; }

        private UserManager<User> _userManager;
        protected UserManager<User> UserManager { get => _userManager; }

        private IMapper _mapper;
        protected IMapper Mapper { get => _mapper; }

        private int? _userId;
        protected int UserId
        {
            get
            {
                if (!_userId.HasValue)
                    _userId = Int32.Parse(UserManager.GetUserId(User));
                return _userId.Value;
            }
        }

        public static string Name { get; private set; }

        protected ICategoryService CategoryService { get =>
        new CategoryService(new CategoryRepository(DbContext), Mapper, Mapper.ConfigurationProvider ); }

        protected IQuestionService QuestionService { get => 
        new QuestionService(new QuestionRepository(DbContext), Mapper, Mapper.ConfigurationProvider); }

        protected IGamemodeService GamemodeService { get => 
        new GamemodeService(new GamemodeRepository(DbContext), Mapper, Mapper.ConfigurationProvider); }

        protected IGameService GameService { get => 
        new GameService(new GameRepository(DbContext, new Random()), Mapper, Mapper.ConfigurationProvider); }

        protected IGameUserService GameUserService { get =>
        new GameUserService(new GameUserRepository(DbContext), Mapper.ConfigurationProvider); }
        

        private string DefaultTempDataKey<T>() => typeof(T).ToString();
        protected void SaveToTempData<T>(T form, string key )
        {
            string jsonString = JsonSerializer.Serialize<T>(form);
            TempData[key] = jsonString;
        }
        protected void SaveToTempData<T>(T form) => SaveToTempData<T>(form, DefaultTempDataKey<T>());
        protected T TryRetrieveFromTempData<T>(string key)
        {
            try
            {
                string jsonString = TempData[key] as string;
                if (jsonString == null)
                    return default(T);
                else return JsonSerializer.Deserialize<T>(jsonString);
            }
            catch (Exception)
            {
                return default(T);
            }
            finally
            {
                TempData.Remove(key);
            }
        }
        protected T TryRetrieveFromTempData<T>() => TryRetrieveFromTempData<T>(DefaultTempDataKey<T>());

        public void SetAlert(AlertModel alert)
        {
            List<AlertModel> alerts = new List<AlertModel>();
            alerts.Add(alert);
            SetAlerts(alerts);
        }
        public void SetAlerts(List<AlertModel> alerts)
        {
            SaveToTempData<List<AlertModel>>(alerts);
        }

        public List<AlertModel> GetAlerts()
        {
            return TryRetrieveFromTempData<List<AlertModel>>();
        }
    }
}
