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
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IMapper _mapper;
        private static IWebHostEnvironment _environment;

        public ProductController(ILogger<ProductController> logger, IMapper mapper, IProductService productService, IProductCategoryService productCategoryService, IWebHostEnvironment environment)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _environment = environment;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CategoryList = await _productCategoryService.GetAll().ConfigureAwait(true);
            return View();
        }

        public async Task<IActionResult> GetAllProducts(JqueryDatatableParam param)
        {
            var myParams = new DynamicParameters();
            myParams.Add("@skip", param.iDisplayStart);
            myParams.Add("@take", param.iDisplayLength);
            myParams.Add("@search_key", param.sSearch);
            myParams.Add("@categoryName", string.Empty);
            var displayResult = await _productService.GetAllForDisplayByQuery(AppData.usp_GetAllProducts, myParams).ConfigureAwait(true);
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
            ViewBag.CategoryList = await _productCategoryService.GetAll().ConfigureAwait(true);
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create(ProductCreateModel createModel)
        {
            ViewBag.CategoryList = await _productCategoryService.GetAll().ConfigureAwait(true);
            if (ModelState.IsValid)
            {
                var rootPath = _environment.WebRootPath;
                var folderName = Path.Combine("Resources", "Products");
                var product = _mapper.Map<Product>(createModel);
                product.ModifiedBy = User.Identity.Name;
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

                product.ImagePath = imageDBPath;
                await _productService.Add(product);

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
            ViewBag.CategoryList = await _productCategoryService.GetAll().ConfigureAwait(true);
            var product = await _productService.GetById(id);
            if (product != null)
            {
                var productView = _mapper.Map<ProductCreateModel>(product);
                return PartialView("_Edit", productView);
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.FetchError });
        }

        [HttpPost]
        public async Task<JsonResult> Edit(ProductCreateModel productCreate)
        {
            ViewBag.CategoryList = await _productCategoryService.GetAll().ConfigureAwait(true);
            ModelState.Remove("ImageFile");
            if (ModelState.IsValid)
            {
                var rootPath = _environment.WebRootPath;
                var folderName = Path.Combine("Resources", "Products");

                var product = await _productService.GetById(productCreate.PID);
                if (product != null)
                {
                    product.CategoryID = productCreate.CategoryID;
                    product.ProductName = productCreate.ProductName;
                    product.Price = productCreate.Price;
                    product.MFG = productCreate.MFG;
                    product.EXP = productCreate.EXP;
                    product.Heading = productCreate.Heading;
                    product.ShortDescription = productCreate.ShortDescription;
                    product.Tags = productCreate.Tags;
                    product.ModifiedBy = User.Identity.Name;

                    try
                    {
                        if (productCreate.ImageFile != null)
                        {
                            Guid obj = Guid.NewGuid();
                            string imageDBPath = Path.Combine(folderName, obj.ToString() + "_.jpg");
                            string filePath = Path.Combine(rootPath, imageDBPath);
                            product.ImagePath = imageDBPath;
                            productCreate.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
                        }
                    }
                    catch (Exception) { }


                    await _productService.Update(product);
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
            var category = await _productService.GetById(id);
            if (category != null)
            {
                await _productService.Delete(category);
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.DeletedSuccess });
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.DeleteError });
        }
    }
}
