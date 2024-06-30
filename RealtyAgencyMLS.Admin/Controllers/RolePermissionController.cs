using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RealtyAgencyMLS.Admin.Models;
using RealtyAgencyMLS.Admin.Resource;
using RealtyAgencyMLS.BAL.Services.Interface;
using RealtyAgencyMLS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Controllers
{
    public class RolePermissionController : Controller
    {
        private readonly IMvcControllerDiscovery _mvcControllerDiscovery;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RolePermissionController(IMvcControllerDiscovery mvcControllerDiscovery, RoleManager<ApplicationRole> roleManager)
        {
            _mvcControllerDiscovery = mvcControllerDiscovery;
            _roleManager = roleManager;
        }

        // GET: Role
        [DisplayName("Role List")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            ViewData["Controllers"] = _mvcControllerDiscovery.GetControllers();

            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleCreateModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Controllers"] = _mvcControllerDiscovery.GetControllers();
                return View(viewModel);
            }

            var role = new ApplicationRole { Name = viewModel.Name };
            if (viewModel.SelectedControllers != null && viewModel.SelectedControllers.Any())
            {
                foreach (var controller in viewModel.SelectedControllers)
                    foreach (var action in controller.Actions)
                        action.ControllerId = controller.Id;

                var accessJson = JsonConvert.SerializeObject(viewModel.SelectedControllers);
                role.Access = accessJson;
            }

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                ViewData["Controllers"] = _mvcControllerDiscovery.GetControllers();
                return View(viewModel);
            }
            //if (result.Succeeded)
            return RedirectToAction(nameof(Index));
        }

        [DisplayName("Edit Role")]
        public async Task<ActionResult> Edit(string id)
        {
            ViewData["Controllers"] = _mvcControllerDiscovery.GetControllers();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            var accessList = JsonConvert.DeserializeObject<IEnumerable<MvcControllerInfo>>(role.Access).ToList();

            RoleCreateModel viewModel = new RoleCreateModel();
            viewModel.Name = role.Name;
            foreach (var item in accessList)
            {
                viewModel.SelectedControllers.Add(new MvcControllerInfo { Actions = item.Actions, AreaName = item.AreaName, DisplayName = item.DisplayName, Name = item.Name });
            }
            return View(viewModel);
        }

        // POST: Role/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, RoleCreateModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Controllers"] = _mvcControllerDiscovery.GetControllers();
                return View(viewModel);
            }

            // Check role exit
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ModelState.AddModelError("", "Role not found");
                ViewData["Controllers"] = _mvcControllerDiscovery.GetControllers();
                return View();
            }
            // Update role access list
            if (viewModel.SelectedControllers != null && viewModel.SelectedControllers.Any())
            {
                foreach (var controller in viewModel.SelectedControllers)
                    foreach (var action in controller.Actions)
                        action.ControllerId = controller.Id;

                var accessJson = JsonConvert.SerializeObject(viewModel.SelectedControllers);
                role.Access = accessJson;
                role.Name = viewModel.Name;
            }
            // Update role if role's name is changed
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                    ViewData["Controllers"] = _mvcControllerDiscovery.GetControllers();
                    return View(viewModel);
                }
            return RedirectToAction(nameof(Index));
        }

        // POST: Role/Delete/5
        [HttpPost]
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

        public  IActionResult AccessDenied()
        {
            return View();
        }
    }
}
