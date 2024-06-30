using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealtyAgencyMLS.Admin.Models;
using RealtyAgencyMLS.Admin.Resource;
using RealtyAgencyMLS.BAL.Services.Interface;
using RealtyAgencyMLS.DAL.Data;
using RealtyAgencyMLS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Controllers
{

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RoleController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleController(ApplicationDbContext context, ILogger<RoleController> logger, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
        }

        #region Role Management

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult CreateRole()
        {
            return View(new ApplicationRole());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(ApplicationRole identityRole)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(identityRole).ConfigureAwait(false);
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.CreatedSuccess });
            }
            else
            {
                return Json(new JsonResponse { IsSuccess = false, Message = AppData.ValidationFieldRequired });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ApplicationRole role = await _roleManager.FindByIdAsync(id).ConfigureAwait(false);
            if (role != null)
            {
                return PartialView("_Edit", new RoleViewModel { Id = role.Id, Name = role.Name });
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.FetchError });
        }

        [HttpPut]
        public async Task<JsonResult> Edit(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = await _roleManager.FindByIdAsync(roleViewModel.Id).ConfigureAwait(false);
                role.Name = roleViewModel.Name;
                await _roleManager.UpdateAsync(role);
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.DataUpdated });
            }
            else
            {
                return Json(new JsonResponse { IsSuccess = false, Message = AppData.ValidationFieldRequired });
            }
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(string id)
        {
            ApplicationRole role = await _roleManager.FindByIdAsync(id).ConfigureAwait(false);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.DeletedSuccess });
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.DeleteError });
        }


        [HttpGet]
        public async Task<JsonResult> GetRoleName(string Id)
        {
            ApplicationRole role = await _roleManager.FindByIdAsync(Id).ConfigureAwait(false);

            return Json(role);
        }

        #endregion
    }
}
