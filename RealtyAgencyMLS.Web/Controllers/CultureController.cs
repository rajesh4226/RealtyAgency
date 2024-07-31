using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RealtyAgencyMLS.BAL.Services.Interface;
using RealtyAgencyMLS.Web.Models;
using RealtyAgencyMLS.Model.Constants;
using RealtyAgencyMLS.Web.Resource;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Constants = RealtyAgencyMLS.Model.Constants.Constants;

namespace RealtyAgencyMLS.Web.Controllers
{
    public class CultureController : Controller
    {
        private readonly ICultureService _cultureService;
        private readonly IMapper _mapper;

        public CultureController(ICultureService cultureService, IMapper mapper)
        {
            _cultureService = cultureService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string currPage)
        {
            int page;
            if (string.IsNullOrWhiteSpace(currPage))
                page = 1;
            else
                page = int.Parse(currPage, CultureInfo.CurrentCulture);
            var itemsToSkip = (page - 1) * Constants.ReturnCount;
            var myParams = new DynamicParameters();
            myParams.Add("@skip", itemsToSkip);
            myParams.Add("@take", Constants.ReturnCount);
            myParams.Add("@search_key", string.Empty);
            var displayResult = await _cultureService.GetAllForDisplayByQuery(AppData.usp_GetAllCulture, myParams).ConfigureAwait(true);
          
            var cultureViewModel = new CultureViewModel
            {
                Culture = displayResult
            };
            return View(cultureViewModel);
        }
        [Route("Culture/Details/{id?}/{url?}")]
        public async Task<IActionResult> Details(int id)
        {
            var myParams = new DynamicParameters();
            myParams.Add("blogid", id);
            var singleCulture = await _cultureService.GetSingleCultureDetailsList(AppData.usp_GetSingleCultureDetails, myParams).ConfigureAwait(true);
            var cultureView = new SingleCultureDisplayModel
            {
                Culture = singleCulture.Culture
            };
            return View(cultureView);
        }
    }
}
