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
    public class BlogCategoryController : Controller
    {
        private readonly ILogger<BlogCategoryController> _logger;
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly IMapper _mapper;
        private static IWebHostEnvironment _environment;

        public BlogCategoryController(ILogger<BlogCategoryController> logger, IMapper mapper, IBlogCategoryService blogCategoryService, IWebHostEnvironment environment)
        {
            _blogCategoryService = blogCategoryService;
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
            var displayResult = await _blogCategoryService.GetAllForDisplayByQuery(AppData.usp_GetAllBlogCategory, myParams).ConfigureAwait(true);
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
        public async Task<JsonResult> Create(BlogCategoryCreateModel createModel)
        {
            if (ModelState.IsValid)
            {
                var blogCategory = _mapper.Map<BlogCategory>(createModel);
                blogCategory.ModifiedBy = User.Identity.Name;
                await _blogCategoryService.Add(blogCategory);
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
            var category = await _blogCategoryService.GetById(id);
            if (category != null)
            {
                var categoryView = _mapper.Map<BlogCategoryCreateModel>(category);
                return PartialView("_Edit", categoryView);
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.FetchError });
        }

        [HttpPost]
        public async Task<JsonResult> Edit(BlogCategoryCreateModel category)
        {
            if (ModelState.IsValid)
            {
                var stage = await _blogCategoryService.GetById(category.PID);
                stage.CategoryName = category.CategoryName;
                stage.ModifiedBy = User.Identity.Name;
                await _blogCategoryService.Update(stage);
                return Json(new JsonResponse  { IsSuccess = true, Message = AppData.DataUpdated });
            }
            else
            {
                return Json(new JsonResponse { IsSuccess = false, Message = AppData.ValidationFieldRequired });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var category = await _blogCategoryService.GetById(id);
            if (category != null)
            {
                await _blogCategoryService.Delete(category);
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.DeletedSuccess });
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.DeleteError });
        }

        public async Task<IEnumerable<BlogCategory>> LoadCategory()
        {
            return await _blogCategoryService.GetAll();
        }
    }
}
