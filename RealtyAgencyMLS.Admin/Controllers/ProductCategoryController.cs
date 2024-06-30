using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
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
    public class ProductCategoryController : Controller
    {
        private readonly ILogger<ProductCategoryController> _logger;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IMapper _mapper;
        private static IWebHostEnvironment _environment;

        public ProductCategoryController(ILogger<ProductCategoryController> logger, IMapper mapper, IProductCategoryService productCategoryService, IWebHostEnvironment environment)
            {
                _productCategoryService = productCategoryService;
                _environment = environment;
                _mapper = mapper;
                _logger = logger;
            }
            public IActionResult Index()
            {
                return View();
            }

            public async Task<IActionResult> GetAllCategory(JqueryDatatableParam param)
            {
                var myParams = new DynamicParameters();
                myParams.Add("@skip", param.iDisplayStart);
                myParams.Add("@take", param.iDisplayLength);
                myParams.Add("@search_key", param.sSearch);
                var displayResult = await _productCategoryService.GetAllForDisplayByQuery(AppData.usp_GetAllProductCategory, myParams).ConfigureAwait(true);
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
            public async Task<JsonResult> Create(ProductCategoryCreateModel createModel)
            {
                if (ModelState.IsValid)
                {
                    var productCategory = _mapper.Map<ProductCategory>(createModel);
                    productCategory.ModifiedBy = User.Identity.Name;
                    await _productCategoryService.Add(productCategory);
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
                var category = await _productCategoryService.GetById(id);
                if (category != null)
                {
                    var categoryView = _mapper.Map<ProductCategoryCreateModel>(category);
                    return PartialView("_Edit", categoryView);
                }
                return Json(new JsonResponse { IsSuccess = false, Message = AppData.FetchError });
            }
            [HttpPost]
            public async Task<JsonResult> Edit(ProductCategoryCreateModel category)
            {
                if (ModelState.IsValid)
                {
                    var stage = await _productCategoryService.GetById(category.PID);
                    stage.CategoryName = category.CategoryName;
                    stage.ModifiedBy = User.Identity.Name;
                    await _productCategoryService.Update(stage);
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
                var category = await _productCategoryService.GetById(id);
                if (category != null)
                {
                    await _productCategoryService.Delete(category);
                    return Json(new JsonResponse { IsSuccess = true, Message = AppData.DeletedSuccess });
                }
                return Json(new JsonResponse { IsSuccess = false, Message = AppData.DeleteError });
            }

            public async Task<IEnumerable<ProductCategory>> LoadCategory()
            {
                return await _productCategoryService.GetAll();
            }
        }
    }
