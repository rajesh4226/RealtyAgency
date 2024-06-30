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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly IMapper _mapper;
        private static IWebHostEnvironment _environment;

        public BlogController(ILogger<BlogController> logger, IMapper mapper, IBlogService blogService, IBlogCategoryService blogCategoryService, IWebHostEnvironment environment)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
            _environment = environment;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CategoryList = await _blogCategoryService.GetAll().ConfigureAwait(true);
            return View();
        }

        public async Task<IActionResult> GetAllBlogs(JqueryDatatableParam param)
        {
            var myParams = new DynamicParameters();
            myParams.Add("@skip", param.iDisplayStart);
            myParams.Add("@take", param.iDisplayLength);
            myParams.Add("@search_key", param.sSearch);
            myParams.Add("@categoryName", string.Empty);
            var displayResult = await _blogService.GetAllForDisplayByQuery(AppData.usp_GetAllBlogs, myParams).ConfigureAwait(true);
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
            ViewBag.CategoryList = await _blogCategoryService.GetAll().ConfigureAwait(true);
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create(BlogCreateModel createModel)
        {
            ViewBag.CategoryList = await _blogCategoryService.GetAll().ConfigureAwait(true);
            if (ModelState.IsValid)
            {
                var rootPath = _environment.WebRootPath;
                var folderName = Path.Combine("Resources", "Blogs");
                var blog = _mapper.Map<Blog>(createModel);
                blog.ModifiedBy = User.Identity.Name;
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

                blog.ImagePath = imageDBPath;
                await _blogService.Add(blog);

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
            ViewBag.CategoryList = await _blogCategoryService.GetAll().ConfigureAwait(true);
            var blog = await _blogService.GetById(id);
            if (blog != null)
            {
                var blogView = _mapper.Map<BlogCreateModel>(blog);
                return PartialView("_Edit", blogView);
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.FetchError });
        }

        [HttpPost]
        public async Task<JsonResult> Edit(BlogCreateModel blogCreate)
        {
            ViewBag.CategoryList = await _blogCategoryService.GetAll().ConfigureAwait(true);
            ModelState.Remove("ImageFile");
            if (ModelState.IsValid)
            {
                var rootPath = _environment.WebRootPath;
                var folderName = Path.Combine("Resources", "Blogs");

                var blog = await _blogService.GetById(blogCreate.PID);
                if (blog != null)
                {
                    blog.Heading = blogCreate.Heading;
                    blog.Description = blogCreate.Description;
                    blog.ShortDescription = blogCreate.ShortDescription;
                    blog.Tags = blogCreate.Tags;
                    blog.CategoryID = blogCreate.CategoryID;
                    blog.ModifiedBy = User.Identity.Name;

                    try
                    {
                        if (blogCreate.ImageFile != null)
                        {
                            Guid obj = Guid.NewGuid();
                            string imageDBPath = Path.Combine(folderName, obj.ToString() + "_.jpg");
                            string filePath = Path.Combine(rootPath, imageDBPath);
                            blog.ImagePath = imageDBPath;
                            blogCreate.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
                        }
                    }
                    catch (Exception) { }


                    await _blogService.Update(blog);
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
            var category = await _blogService.GetById(id);
            if (category != null)
            {
                await _blogService.Delete(category);
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.DeletedSuccess });
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.DeleteError });
        }
    }
}
