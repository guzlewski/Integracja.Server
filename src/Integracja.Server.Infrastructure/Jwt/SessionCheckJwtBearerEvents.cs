using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Jwt
{
    public class SessionCheckJwtBearerEvents : JwtBearerEvents
    {
        private readonly ApplicationDbContext _context;

        public SessionCheckJwtBearerEvents(ApplicationDbContext context)
        {
            _context = context;
        }

        public override async Task TokenValidated(TokenValidatedContext context)
        {
            var jwtSecurityToken = context.SecurityToken as JwtSecurityToken;

            if (jwtSecurityToken == null)
            {
                context.Fail("SecurityToken is invalid.");
            }

            var userId = int.Parse(jwtSecurityToken.Subject);
            var sessionGuid = context.SecurityToken.Id;

            var currentSessionGuid = await _context.Users
                .Where(u => u.Id == userId && !u.IsDeleted)
                .Select(u => u.SessionGuid)
                .FirstOrDefaultAsync();

            if (currentSessionGuid == null || currentSessionGuid.Value.ToString() != sessionGuid)
            {
                context.Fail("Session expired, user is logged on other device.");
            }
        }
    }
}
