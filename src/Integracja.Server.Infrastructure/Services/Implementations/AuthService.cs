using System;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, ApplicationDbContext context, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<DetailUserDto> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.NormalizedUserName == _userManager.NormalizeName(dto.Username) && !u.IsDeleted);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                throw new UnauthorizedException("Invalid username or password.");
            }

            var sessionGuid = Guid.NewGuid();

            user.SessionGuid = sessionGuid;
            await _context.SaveChangesAsync();

            var token = _tokenService.GenerateToken(user.Id, sessionGuid);

            return _mapper.Map<DetailUserDto>(user, opts => opts.Items["token"] = token);
        }

        public async Task Logout(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

            if (user == null)
            {
                throw new UnauthorizedException();
            }

            user.SessionGuid = null;
            await _context.SaveChangesAsync();
        }
    }
}
