using System.Security.Claims;
using System.Threading.Tasks;
using Integracja.Server.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Integracja.Server.Web.Ulitities
{
    public class ProfilePicturesClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        private const string DefaultPicture = "https://as2.ftcdn.net/jpg/02/15/84/43/500_F_215844325_ttX9YiIIyeaR7Ne6EaLLjMAmy4GvPC69.jpg";

        public ProfilePicturesClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("ProfilePicture", user.ProfilePicture ?? DefaultPicture));
            identity.AddClaim(new Claim("ProfileThumbnail", user.ProfileThumbnail ?? DefaultPicture));

            return identity;
        }
    }
}
