using System.Security.Claims;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Integracja.Server.Web.Ulitities
{
    public class ProfilePicturesClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        public ProfilePicturesClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("ProfilePicture", user.ProfilePicture));
            identity.AddClaim(new Claim("ProfileThumbnail", user.ProfileThumbnail));

            return identity;
        }
    }
}
