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
    public class CompanyController : Controller
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private static IWebHostEnvironment _environment;

        public CompanyController(ILogger<CompanyController> logger, IMapper mapper, ICompanyService companyService, IWebHostEnvironment environment)
        {
            _companyService = companyService;
            _environment = environment;
            _mapper = mapper;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllCompany(JqueryDatatableParam param)
        {
            var myParams = new DynamicParameters();
            myParams.Add("@skip", param.iDisplayStart);
            myParams.Add("@take", param.iDisplayLength);
            myParams.Add("@search_key", param.sSearch);
            var displayResult = await _companyService.GetAllForDisplayByQuery(AppData.usp_GetAllCompany, myParams).ConfigureAwait(true);
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
        public async Task<JsonResult> Create(CompanyCreateModel createModel)
        {
            if (ModelState.IsValid)
            {
                var company = _mapper.Map<Company>(createModel);
                company.ModifiedBy = User.Identity.Name;
                await _companyService.Add(company);
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
            var category = await _companyService.GetById(id);
            if (category != null)
            {
                var categoryView = _mapper.Map<CompanyCreateModel>(category);
                return PartialView("_Edit", categoryView);
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.FetchError });
        }

        [HttpPost]
        public async Task<JsonResult> Edit(CompanyCreateModel category)
        {
            if (ModelState.IsValid)
            {
                var stage = await _companyService.GetById(category.PID);
                stage.CompanyName = category.CompanyName;
                stage.ModifiedBy = User.Identity.Name;
                await _companyService.Update(stage);
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
            var category = await _companyService.GetById(id);
            if (category != null)
            {
                await _companyService.Delete(category);
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.DeletedSuccess });
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.DeleteError });
        }

        public async Task<IEnumerable<Company>> LoadCategory()
        {
            return await _companyService.GetAll();
        }
    }
}