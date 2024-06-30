using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealtyAgencyMLS.Admin.Models;
using RealtyAgencyMLS.Admin.Resource;
using RealtyAgencyMLS.BAL.EmailServices;
using RealtyAgencyMLS.BAL.Services.Interface;
using RealtyAgencyMLS.DAL.Data;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.Common;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Controllers
{
    //[Authorize(Roles = "SuperAdmin, Admin")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class UserManageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserManageController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IUserManageServices _userManageServices;

        private static IWebHostEnvironment _environment;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserManageController(IWebHostEnvironment hostEnvironment, IWebHostEnvironment environment, IUserManageServices userManageServices, ApplicationDbContext context, ILogger<UserManageController> logger, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IEmailSender emailSender)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _context = context;
            _userManageServices = userManageServices;
            _environment = environment;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }


        #region User Management

        public IActionResult GetUserList()
        {
            ViewBag.RoleList = _roleManager.Roles.ToList();
            return View();
        }
        public async Task<IActionResult> GetAllUsers(JqueryDatatableParam param)
        {
            try
            {
                var myParams = new DynamicParameters();
                myParams.Add("@skip", param.iDisplayStart);
                myParams.Add("@take", param.iDisplayLength);
                myParams.Add("@search_key", param.sSearch);
                myParams.Add("@userType", param.UserType);
                var displayResult = await _userManageServices.GetAllForDisplayByQuery(AppData.usp_GetAllUsers, myParams).ConfigureAwait(true);
                var totalRecords = displayResult.Any() ? displayResult.First().TotalRecords : 0;
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = displayResult
                });
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        public IActionResult CreateUser()
        {
            ViewBag.RoleList = _roleManager.Roles.ToList();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string password = GetTemporaryPassword();
                var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email).ConfigureAwait(false);

                IdentityRole role = await _roleManager.FindByIdAsync(model.Id).ConfigureAwait(false);
                model.Rolename = role.ToString();

                var rootPath = _hostEnvironment.WebRootPath;
                var folderName = Path.Combine("Resources", "Users");
                Guid obj = Guid.NewGuid();
                string imageDBPath = Path.Combine(folderName, obj.ToString() + "_.jpg");
                if (model.ImageFile != null)
                {

                    string filePath = Path.Combine(rootPath, imageDBPath);
                    model.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                if (existUser == null)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        CreatedDate = DateTime.Now,
                        AddressStreet = model.AddressStreet,
                        City = model.City,
                        State = model.State,
                        Country = model.CCountry,
                        Zip = model.Zip
                    };
                    user.IsFirstLogin = true;
                    user.ImagePath = imageDBPath;
                    user.Discriminator = "Application User";
                    IdentityResult result = _userManager.CreateAsync(user, password).Result;
                    if (result.Succeeded)
                        _userManager.AddToRoleAsync(user, model.Rolename).Wait();
                    if (model.Rolename.ToLower() == "agent")
                    {
                        var dto = _mapper.Map<RealtyAgents>(model);
                        dto.ImagePath = imageDBPath;
                        dto.AppUserID = user.AppUserID;
                        dto.IsMLSAgent = false;
                        dto.Country = model.CCountry;
                        dto.AgentID = 0;

                        await _userManageServices.Add(dto);
                        string fullName = user.FirstName + " " + user.LastName;
                        string message = "Dear Member <br/><br/> Realty Agency has registered you as admin. So you are requested to login into the system using below temperary credentials. You can reset your password when you will login first time.</br></br> Username: " + user.Email + "</br>Password: " + password;

                        string body = string.Format(fullName, message);
                        await _emailSender.SendEmal(EmailTemplates.EmailConfirmation, new MailRequest { Body = message, Subject = "RealtyAgency-New User Register", ToEmail = user.Email }).ConfigureAwait(false);
                    }
                    else
                    {
                        string fullName = user.FirstName + " " + user.LastName;
                        string message = "Dear Member <br/><br/> Realty Agency has registered you as admin. So you are requested to login into the system using below temperary credentials. You can reset your password when you will login first time.</br></br> Username: " + user.Email + "</br>Password: " + password;

                        string body = string.Format(fullName, message);
                        await _emailSender.SendEmal(EmailTemplates.EmailConfirmation, new MailRequest { Body = message, Subject = "RealtyAgency-New User Register", ToEmail = user.Email }).ConfigureAwait(false);
                    }

                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.CreatedSuccess });
            }
            else
            {
                return Json(new JsonResponse { IsSuccess = false, Message = AppData.ValidationFieldRequired });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            ViewBag.RoleList = _roleManager.Roles.ToList();

            var user = await _userManager.FindByIdAsync(id).ConfigureAwait(true);
            var roleName = await _userManager.GetRolesAsync(user).ConfigureAwait(true);
            var role = await _roleManager.FindByNameAsync(roleName[0].ToString()).ConfigureAwait(true);
            if (user != null)
            {
                // For Agent
                if (role.Name.ToLower() == "agent")
                {
                    var myParams = new DynamicParameters();
                    myParams.Add("@AppUserID", user.AppUserID);
                    var displayResult = await _userManageServices.GetSingleAgent(AppData.usp_GetAgentByAppUserID, myParams).ConfigureAwait(true);

                    var userAgnetView = _mapper.Map<UserViewModel>(displayResult);
                    userAgnetView.CCountry = user.Country;
                    userAgnetView.UserId = user.Id;
                    userAgnetView.Id = role.Id;
                    userAgnetView.Name = role.Name;
                    userAgnetView.ImagePath=user.ImagePath;
                    return PartialView("_EditUser", userAgnetView);
                }
                // For Admin
                return PartialView("_EditUser", new UserViewModel
                {
                    Name = role.Name,
                    Id = role.Id,
                    RoleId = role.Id,
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    AddressStreet = user.AddressStreet,
                    City = user.City,
                    State = user.State,
                    CCountry = user.Country,
                    Zip = user.Zip,
                    ImagePath = user.ImagePath
            }); ;
            }
            return Json(new JsonResponse
            {
                IsSuccess = false,
                Message = AppData.FetchError
            });
        }

        [HttpPost]
        public async Task<JsonResult> EditUser(UserViewModel userViewModel)
        {
            ViewBag.RoleList = _roleManager.Roles.ToList();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userViewModel.UserId).ConfigureAwait(false);
                var rootPath = _hostEnvironment.WebRootPath;
                var folderName = Path.Combine("Resources", "Users");
                Guid obj = Guid.NewGuid();
                string imageDBPath = Path.Combine(folderName, obj.ToString() + "_.jpg");
                if (userViewModel.ImageFile != null)
                {
                    string filePath = Path.Combine(rootPath, imageDBPath);
                    userViewModel.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                if (user != null)
                {
                    user.Email = userViewModel.Email;
                    user.FirstName = userViewModel.FirstName;
                    user.LastName = userViewModel.LastName;
                    user.PhoneNumber = userViewModel.PhoneNumber;
                    user.AddressStreet = userViewModel.AddressStreet;
                    user.City = userViewModel.City;
                    user.State = userViewModel.State;
                    user.Country = userViewModel.CCountry;
                    user.Zip = userViewModel.Zip;
                    user.ImagePath = imageDBPath;

                    if (userViewModel.Name.ToLower() == "agent")   //For Agent
                    {
                        var myParams = new DynamicParameters();
                        myParams.Add("@AppUserID", user.AppUserID);
                        var agentResult = await _userManageServices.GetSingleAgent(AppData.usp_GetAgentByAppUserID, myParams).ConfigureAwait(true);
                        if (agentResult != null)
                        {

                            agentResult.ImagePath = imageDBPath;
                            agentResult.Email = userViewModel.Email;
                            agentResult.FirstName = userViewModel.FirstName;
                            agentResult.LastName = userViewModel.LastName;
                            agentResult.PhoneNumber = userViewModel.PhoneNumber;
                            agentResult.AddressStreet = userViewModel.AddressStreet;
                            agentResult.City = userViewModel.City;
                            agentResult.State = userViewModel.State;
                            agentResult.Country = userViewModel.CCountry;
                            agentResult.Zip = userViewModel.Zip;
                            agentResult.OrgAddressStreet = userViewModel.OrgAddressStreet;
                            agentResult.OrgCity = userViewModel.OrgCity;
                            agentResult.OrgState = userViewModel.OrgState;
                            agentResult.OrgZip = userViewModel.OrgZip;
                            agentResult.Facebook = userViewModel.Facebook;
                            agentResult.Twitter = userViewModel.Twitter;
                            agentResult.Instagram = userViewModel.Instagram;
                            agentResult.AboutUs = userViewModel.AboutUs;

                            await _userManageServices.Update(agentResult);
                        }
                    }
                    await _userManager.UpdateAsync(user);
                    return Json(new JsonResponse { IsSuccess = true, Message = AppData.DataUpdated });
                }
                else
                {
                    return Json(new JsonResponse { IsSuccess = false, Message = AppData.FetchError });
                }
            }
            else
            {
                return Json(new JsonResponse { IsSuccess = false, Message = AppData.ValidationFieldRequired });
            }
        }


        [HttpPost]
        public async Task<JsonResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
            if (user != null)
            {
                RealtyAgents model = new RealtyAgents();
                model.AppUserID = user.AppUserID;
                model.Email = user.Email;
                //var realtyAgents = _mapper.Map<RealtyAgents>(userAgent);
                //realtyAgents.AppUserID = user.AppUserID;
                await _userManageServices.Delete(model);
                await _userManager.DeleteAsync(user).ConfigureAwait(false);
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.DeletedSuccess });
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.DeleteError });
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Method For Genrate temp password
        /// </summary>
        /// <returns></returns>
        private string GetTemporaryPassword()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<JsonResult> GetRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id).ConfigureAwait(false);

            return Json(role);
        }

        [HttpPost]
        public async Task<JsonResult> GetExistUserEmail(string email, string initialName)
        {
            if (email == initialName)
            {
                // Nothing has changed so signal its valid
                return Json(true);
            }
            var userEmail = await _userManager.FindByEmailAsync(email.Trim()).ConfigureAwait(true);

            return Json(userEmail == null);
        }

        #endregion
    }
}
