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
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly ILogger<VehicleTypeController> _logger;
        private readonly IVehicleTypeService _vehicleTypeService;
        private readonly IMapper _mapper;
        private static IWebHostEnvironment _environment;

        public VehicleTypeController(ILogger<VehicleTypeController> logger, IMapper mapper, IVehicleTypeService vehicleTypeService, IWebHostEnvironment environment)
        {
            _vehicleTypeService = vehicleTypeService;
            _environment = environment;
            _mapper = mapper;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllVehicleType(JqueryDatatableParam param)
        {
            var myParams = new DynamicParameters();
            myParams.Add("@skip", param.iDisplayStart);
            myParams.Add("@take", param.iDisplayLength);
            myParams.Add("@search_key", param.sSearch);
            var displayResult = await _vehicleTypeService.GetAllForDisplayByQuery(AppData.usp_GetAllVehicleType, myParams).ConfigureAwait(true);
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create(VehicleTypeCreateModel createModel)
        {
            if (ModelState.IsValid)
            {
                var vehicleType = _mapper.Map<VehicleType>(createModel);
                vehicleType.ModifiedBy = User.Identity.Name;
                try
                {
                    await _vehicleTypeService.Add(vehicleType);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                
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
            var vehicleType = await _vehicleTypeService.GetById(id);
            if (vehicleType != null)
            {
                var vehicleTypeView = _mapper.Map<VehicleTypeCreateModel>(vehicleType);
                return PartialView("_Edit", vehicleTypeView);
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.FetchError });
        }
        [HttpPost]
        public async Task<JsonResult> Edit(VehicleTypeCreateModel category)
        {
            if (ModelState.IsValid)
            {
                var stage = await _vehicleTypeService.GetById(category.PID);
                stage.VehicleTypeName = category.VehicleTypeName;
                stage.ModifiedBy = User.Identity.Name;
                await _vehicleTypeService.Update(stage);
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.DataUpdated });
            }
            else
            {
                return Json(new JsonResponse { IsSuccess = false, Message = AppData.ValidationFieldRequired });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var category = await _vehicleTypeService.GetById(id);
            if (category != null)
            {
                await _vehicleTypeService.Delete(category);
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.DeletedSuccess });
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.DeleteError });
        }

        public async Task<IEnumerable<VehicleType>> LoadCategory()
        {
            return await _vehicleTypeService.GetAll();
        }
    }
}
