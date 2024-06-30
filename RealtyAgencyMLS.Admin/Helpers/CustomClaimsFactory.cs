using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RealtyAgencyMLS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Helpers
{
    public class CustomClaimsFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public CustomClaimsFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
        }

        /// <summary>
        /// Generate Custom Claims
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user).ConfigureAwait(false);

            var roles = await UserManager.GetRolesAsync(user).ConfigureAwait(true);

            //var userFullName = string.Format(MessageResource.FullName, user.FirstName, user.LastName);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Uri, "/Home/Index"));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            identity.AddClaim(new Claim("FisrtName", user.FirstName));
            identity.AddClaim(new Claim("LastName", user.LastName));
            //identity.AddClaim(new Claim("Profile", user.ImagePath));

            identity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return identity;
        }
    }
}
