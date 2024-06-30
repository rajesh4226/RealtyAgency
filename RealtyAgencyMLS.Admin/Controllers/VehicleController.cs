using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealtyAgencyMLS.Admin.Models;
using RealtyAgencyMLS.Admin.Resource;
using RealtyAgencyMLS.BAL.Services.Interface;
using RealtyAgencyMLS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleService _vehicleService;
        private readonly IVehicleTypeService _vehicleTypeService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private static IWebHostEnvironment _environment;

        public VehicleController(ILogger<VehicleController> logger,  IMapper mapper, IVehicleService vehicleService, IVehicleTypeService vehicleTypeService, ICompanyService companyService, IWebHostEnvironment environment)
        {
            _vehicleService = vehicleService;
            _vehicleTypeService = vehicleTypeService;
            _companyService = companyService;
            _environment = environment;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.VehicleTypeList = await _vehicleTypeService.GetAll().ConfigureAwait(true);
            ViewBag.CompanyList = await _companyService.GetAll().ConfigureAwait(true);
            return View();
        }

        public async Task<IActionResult> GetAllVehicle(JqueryDatatableParam param)
        {
            var myParams = new DynamicParameters();
            myParams.Add("@skip", param.iDisplayStart);
            myParams.Add("@take", param.iDisplayLength);
            myParams.Add("@search_key", param.sSearch);
            myParams.Add("@VehicleTypeName", string.Empty);
            var displayResult = await _vehicleService.GetAllForDisplayByQuery(AppData.usp_GetAllVehicle, myParams).ConfigureAwait(true);
            var totalRecords = displayResult.Any() ? displayResult.First().TotalRecords : 0;
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.VehicleTypeList = await _vehicleTypeService.GetAll().ConfigureAwait(true);
            ViewBag.CompanyList = await _companyService.GetAll().ConfigureAwait(true);
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create(VehicleCreateModel createModel)
        {
            ViewBag.VehicleTypeList = await _vehicleTypeService.GetAll().ConfigureAwait(true);
            if (ModelState.IsValid)
            {
                var rootPath = _environment.WebRootPath;
                var folderName = Path.Combine("Resources", "Vehicles");
                var vehicle = _mapper.Map<Vehicle>(createModel);
                vehicle.ModifiedBy = User.Identity.Name;
                Guid obj = Guid.NewGuid();
                string imageDBPath = Path.Combine(folderName, obj.ToString() + "_.jpg");
                try
                {
                    if (createModel.ImageFile != null)
                    {
                        string filePath = Path.Combine(rootPath, imageDBPath);
                        createModel.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }
                catch (Exception) { }
                vehicle.ImagePath = imageDBPath;
                await _vehicleService.Add(vehicle);
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.CreatedSuccess });
            }
            else
            {
                return Json(new JsonResponse { IsSuccess = false, Message = AppData.ValidationFieldRequired });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.VehicleTypeList = await _vehicleTypeService.GetAll().ConfigureAwait(true);
            ViewBag.CompanyList = await _companyService.GetAll().ConfigureAwait(true);
            var vehicle = await _vehicleService.GetById(id);
            if (vehicle != null)
            {
                var vehicleView = _mapper.Map<VehicleCreateModel>(vehicle);
                return PartialView("_Edit", vehicleView);
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.FetchError });
        }
        [HttpPost]
        public async Task<JsonResult> Edit(VehicleCreateModel vehicleCreate)
        {
            ViewBag.VehicleTypeList = await _vehicleTypeService.GetAll().ConfigureAwait(true);
            ViewBag.CompanyList = await _companyService.GetAll().ConfigureAwait(true);
            ModelState.Remove("ImageFile");
            if (ModelState.IsValid)
            {
                var rootPath = _environment.WebRootPath;
                var folderName = Path.Combine("Resources", "Vehicles");

                var vehicle = await _vehicleService.GetById(vehicleCreate.PID);
                if (vehicle != null)
                {
                    vehicle.PID = vehicleCreate.PID;
                    vehicle.VehicleName = vehicleCreate.VehicleName;
                    vehicle.Price = vehicleCreate.Price;
                    vehicle.ImagePath = vehicleCreate.ImagePath;
                    vehicle.Description = vehicleCreate.Description;
                    vehicle.CompanyID = vehicleCreate.CompanyID;
                    vehicle.VehicleTypeID = vehicleCreate.VehicleTypeID;
                    try
                    {
                        if (vehicleCreate.ImageFile != null)
                        {
                            Guid obj = Guid.NewGuid();
                            string imageDBPath = Path.Combine(folderName, obj.ToString() + "_.jpg");
                            string filePath = Path.Combine(rootPath, imageDBPath);
                            vehicle.ImagePath = imageDBPath;
                            vehicleCreate.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
                        }
                    }
                    catch (Exception) { }


                    await _vehicleService.Update(vehicle);
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
        public async Task<JsonResult> Delete(int id)
        {
            var category = await _vehicleService.GetById(id);
            if (category != null)
            {
                await _vehicleService.Delete(category);
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.DeletedSuccess });
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.DeleteError });
        }
    }
}