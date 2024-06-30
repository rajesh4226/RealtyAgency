using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using RealtyAgencyMLS.Admin.Models;
using RealtyAgencyMLS.Admin.Resource;
using RealtyAgencyMLS.BAL.EmailServices;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(IEmailSender emailSender, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect(HttpContext.User.FindFirst(ClaimTypes.Uri).Value);
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult GetLoggedInUser()
        {
            if (_signInManager.IsSignedIn(User))
                return Json(new { IsLoggedIn = true, UserName = User.Identity.Name });
            else
                return Json(new { IsLoggedIn = false, UserName = string.Empty });
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false).ConfigureAwait(true);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    model.StatusMessage = $"Error : {AppData.InvalidLogin}";
                    return View(model);
                }
            }
            else
            {
                model.StatusMessage = $"Error : {AppData.InvalidLogin}";
                return View(model);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");

        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPassword.Email).ConfigureAwait(true);
                if (user != null)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(true);
                   // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Action(
                        "ResetPassword",
                        "Account",
                        values: new { code, forgotPassword.Email },
                        protocol: Request.Scheme);

                    var message = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                    await _emailSender.SendEmal(EmailTemplates.EmailConfirmation, new MailRequest { Body = message, Subject = "Reset Password", ToEmail = forgotPassword.Email }).ConfigureAwait(false);
                    forgotPassword.StatusMessage = "Please check your email to reset your password.";
                }
                else
                {
                    forgotPassword.StatusMessage = "Error : There is no account with this email address";
                }
            }
            return View(forgotPassword);
        }

        public IActionResult ResetPassword(string code, string email = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
               var resetPasswordModel = new ResetPasswordModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code)),
                    Email = email
               };
                return View(resetPasswordModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPassword)
        {
            if (!string.IsNullOrWhiteSpace(resetPassword.Email))
            {
                var user = await _userManager.FindByEmailAsync(resetPassword.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, resetPassword.Code, resetPassword.Password);
                    if (result.Succeeded)
                    {
                        resetPassword.StatusMessage = "Your password has been reset successfully!";
                    }
                    else
                    {
                        resetPassword.StatusMessage = "Error : " +  result.Errors.First().Description;
                    }
                }
                else
                {
                    resetPassword.StatusMessage = "Error : Invalid url!";
                }
            }
            else
            {
                resetPassword.StatusMessage = "Error : Invalid url!";
            }

            return View(resetPassword);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
