using AutoMapper;
using RealtyAgencyMLS.Model;
using RealtyAgencyMLS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<BlogCategoryDTO, BlogCategory>();
            CreateMap<BlogCategory, BlogCategoryDTO>();

            CreateMap<BlogCategoryCreateModel, BlogCategory>();
            CreateMap<BlogCategory, BlogCategoryCreateModel>();

            CreateMap<BlogDTO, Blog>();
            CreateMap<Blog, BlogDTO>();

            CreateMap<BlogCreateModel, Blog>();
            CreateMap<Blog, BlogCreateModel>();

            CreateMap<ProductCategoryCreateModel, ProductCategory>();
            CreateMap<ProductCategory, ProductCategoryCreateModel>();

            CreateMap<ProductCategoryDTO, ProductCategory>();
            CreateMap<ProductCategory, ProductCategoryDTO>();

            CreateMap<ProductCreateModel, Product>();
            CreateMap<Product, ProductCreateModel>();

            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>();

            CreateMap<VehicleTypeCreateModel, VehicleType>();
            CreateMap<VehicleType, VehicleTypeCreateModel>();

            CreateMap<VehicleTypeDTO, VehicleType>();
            CreateMap<VehicleType, VehicleTypeDTO>();

            CreateMap<CompanyCreateModel, Company>();
            CreateMap<Company, CompanyCreateModel>();

            CreateMap<CompanyDTO, Company>();
            CreateMap<Company, CompanyDTO>();


            CreateMap<VehicleCreateModel, Vehicle>();
            CreateMap<Vehicle, VehicleCreateModel>();

            CreateMap<VehicleDTO, Vehicle>();
            CreateMap<Vehicle, VehicleDTO>();

            CreateMap<CultureCreateModel, Culture>();
            CreateMap<Culture, CultureCreateModel>();

            CreateMap<CultureDTO, Culture>();
            CreateMap<Culture, CultureDTO>();

            CreateMap<UserViewModel, RealtyAgents>();
            CreateMap<RealtyAgents, UserViewModel>();
        }
    }
}