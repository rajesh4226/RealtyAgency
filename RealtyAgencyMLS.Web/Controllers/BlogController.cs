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
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly IMapper _mapper;

        public BlogController(IBlogCategoryService blogCategoryService, IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string currPage, string categoryName)
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
            myParams.Add("@categoryName", categoryName);
            var displayResult = await _blogService.GetAllForDisplayByQuery(AppData.usp_GetAllBlogs, myParams).ConfigureAwait(true);
            var totalRecords = displayResult.Any() ? displayResult.First().TotalRecords : 0;
            var blogViewModel = new BlogViewModel
            {
                Blogs = displayResult,
                TotalPages = displayResult.Any() ? displayResult.First().TotalPages : 0,
                CurrentPage = page,
                Totalrecords = totalRecords,
            };
            return View(blogViewModel);
        }
        [Route("Blogs/Details/{id?}/{url?}")]
        public async Task<IActionResult> Details(int id)
        {
            var myParams = new DynamicParameters();
            myParams.Add("blogid", id);
            var singleBlog = await _blogService.GetSingleBlogDetailsList(AppData.usp_GetSingleBlogDetails, myParams).ConfigureAwait(true);
            var blogView = new SingleBlogDisplayModel
            {
                Blog = singleBlog.Blog,
                BlogCategory = singleBlog.BlogCategory,
                RecentBlogs = singleBlog.RecentBlogs,
                RelatedBlogs = singleBlog.RelatedBlogs
            };
            return View(blogView);
        }
    }
}
