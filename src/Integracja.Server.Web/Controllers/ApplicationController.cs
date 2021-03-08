using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Mappers;
using Integracja.Server.Infrastructure.Repositories;
using Integracja.Server.Infrastructure.Services;
using Integracja.Server.Infrastructure.Services.Implementations;
using Integracja.Server.Web.Models.Shared.Alert;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace Integracja.Server.Web.Controllers
{
    [Authorize]
    public class ApplicationController : Controller, IAlert
    {
        public ApplicationController(UserManager<User> userManager, ApplicationDbContext dbContext) : base()
        {
            _context = dbContext;
            _userManager = userManager;
            _userId = null;
            _mapper = AutoMapperConfig.Initialize();
        }

        private ApplicationDbContext _context;
        protected ApplicationDbContext DbContext { get => _context; }

        private UserManager<User> _userManager;
        protected UserManager<User> UserManager { get => _userManager; }

        private AutoMapper.IMapper _mapper;
        protected AutoMapper.IMapper Mapper { get => _mapper; }

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
        new CategoryService(new CategoryRepository(DbContext), Mapper); }

        protected IQuestionService QuestionService { get =>
        new QuestionService(new QuestionRepository(DbContext), Mapper); }

        protected void SaveToTempData<T>(T form)
        {
            string jsonString = JsonSerializer.Serialize<T>(form);
            TempData[typeof(T).ToString()] = jsonString;
        }


        protected T TryRetrieveFromTempData<T>()
        {
            try
            {
                string jsonString = TempData[typeof(T).ToString()] as string;
                if (jsonString == null)
                    return default(T);
                else return JsonSerializer.Deserialize<T>(jsonString);
            }
            catch (Exception e)
            {
                return default(T);
            }
            finally
            {
                TempData.Remove(typeof(T).ToString());
            }
        }
    }
}
