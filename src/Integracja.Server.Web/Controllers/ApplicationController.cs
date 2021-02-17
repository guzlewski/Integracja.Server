using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Mappers;
using Integracja.Server.Infrastructure.Repositories;
using Integracja.Server.Infrastructure.Services;
using Integracja.Server.Infrastructure.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Integracja.Server.Web.Controllers
{
    public class ApplicationController : Controller
    {
        // using dependency injection
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

        protected ICategoryService CategoryService { get =>
        new CategoryService(new CategoryRepository(DbContext), Mapper); }

        protected IQuestionService QuestionService { get =>
        new QuestionService(new QuestionRepository(DbContext), Mapper); }
    }
}
