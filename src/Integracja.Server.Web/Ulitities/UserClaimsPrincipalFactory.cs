using System.Security.Claims;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Integracja.Server.Web.Ulitities
{
    public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("ProfilePicture", user.ProfilePicture));

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roles)
            {
                identity.AddClaim(new Claim(Options.ClaimsIdentity.RoleClaimType, roleName));

                if (_roleManager.SupportsRoleClaims)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {
                        identity.AddClaims(await _roleManager.GetClaimsAsync(role));
                    }
                }
            }

            return identity;
        }
    }
}
