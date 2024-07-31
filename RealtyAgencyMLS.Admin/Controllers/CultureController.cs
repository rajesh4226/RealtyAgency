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
    public class CultureController : Controller
    {
        private readonly ILogger<CultureController> _logger;
        private readonly ICultureService _cultureService;
        private readonly IMapper _mapper;
        private static IWebHostEnvironment _environment;

        public CultureController(ILogger<CultureController> logger, IMapper mapper, ICultureService cultureService, IWebHostEnvironment environment)
        {
            _cultureService = cultureService;
            _environment = environment;
            _mapper = mapper;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllCulture(JqueryDatatableParam param)
        {
            var myParams = new DynamicParameters();
            myParams.Add("@skip", param.iDisplayStart);
            myParams.Add("@take", param.iDisplayLength);
            myParams.Add("@search_key", param.sSearch);
            var displayResult = await _cultureService.GetAllForDisplayByQuery(AppData.usp_GetAllCulture, myParams).ConfigureAwait(true);
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
        public async Task<JsonResult> Create(CultureCreateModel createModel)
        {
            if (ModelState.IsValid)
            {
                var rootPath = _environment.WebRootPath;
                var folderName = Path.Combine("Resources", "Culture");
                var culture = _mapper.Map<Culture>(createModel);
                culture.ModifiedBy = User.Identity.Name;
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

                culture.ImagePath = imageDBPath;
                await _cultureService.Add(culture);

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
            var culture = await _cultureService.GetById(id);
            if (culture != null)
            {
                var cultureView = _mapper.Map<CultureCreateModel>(culture);
                return PartialView("_Edit", cultureView);
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.FetchError });
        }

        [HttpPost]
        public async Task<JsonResult> Edit(CultureCreateModel cultureCreate)
        {
            ModelState.Remove("ImageFile");
            if (ModelState.IsValid)
            {
                var rootPath = _environment.WebRootPath;
                var folderName = Path.Combine("Resources", "Culture");

                var culture = await _cultureService.GetById(cultureCreate.PID);
                if (culture != null)
                {
                    culture.Heading = cultureCreate.Heading;
                    culture.Description = cultureCreate.Description;
                    culture.YoutubeVideoLink = cultureCreate.YoutubeVideoLink;
                    culture.ModifiedBy = User.Identity.Name;

                    try
                    {
                        if (cultureCreate.ImageFile != null)
                        {
                            Guid obj = Guid.NewGuid();
                            string imageDBPath = Path.Combine(folderName, obj.ToString() + "_.jpg");
                            string filePath = Path.Combine(rootPath, imageDBPath);
                            culture.ImagePath = imageDBPath;
                            cultureCreate.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
                        }
                    }
                    catch (Exception) { }


                    await _cultureService.Update(culture);
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
            var category = await _cultureService.GetById(id);
            if (category != null)
            {
                await _cultureService.Delete(category);
                return Json(new JsonResponse { IsSuccess = true, Message = AppData.DeletedSuccess });
            }
            return Json(new JsonResponse { IsSuccess = false, Message = AppData.DeleteError });
        }
    }
}