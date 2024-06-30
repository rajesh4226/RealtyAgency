using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealtyAgencyMLS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Controllers
{
    [Authorize]
    public class ClaimsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ClaimsController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public ViewResult Index() => View(User?.Claims);

        public ViewResult Create() => View();

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post(string claimType, string claimValue)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            Claim claim = new Claim(claimType, claimValue, ClaimValueTypes.String);
            IdentityResult result = await _userManager.AddClaimAsync(user, claim);
            //var roleName = await _userManager.GetRolesAsync(user).ConfigureAwait(true);
            //var role = await _roleManager.FindByNameAsync(roleName[0].ToString()).ConfigureAwait(true);
            //await _roleManager.AddClaimAsync(role, claim);

            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string claimValues)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            string[] claimValuesArray = claimValues.Split(";");
            string claimType = claimValuesArray[0], claimValue = claimValuesArray[1], claimIssuer = claimValuesArray[2];

            Claim claim = User.Claims.Where(x => x.Type == claimType && x.Value == claimValue && x.Issuer == claimIssuer).FirstOrDefault();

            IdentityResult result = await _userManager.RemoveClaimAsync(user, claim);

            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);

            return View("Index");
        }
        void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
