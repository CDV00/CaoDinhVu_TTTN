using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            //mapper category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryRequest>().ReverseMap();
            //mapper brand
            CreateMap<Brand, BrandDTO>().ReverseMap();
            CreateMap<Brand, BrandRequest>().ReverseMap();

            //slider
            CreateMap<Slider, SliderDTO>().ReverseMap();
            CreateMap<Slider, SliderRequest>().ReverseMap();
            CreateMap<SliderRequest, Slider>().ReverseMap();
            //product
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ListProductDTO>().ReverseMap();
            //cartItem
            CreateMap<Product, ProductCartItem>().ReverseMap();
            CreateMap<ProductColor, ProductColorCartItem>().ReverseMap();
            CreateMap<ProductOption, ProductOptionCartItem>().ReverseMap();
            //color
            CreateMap<Color, ColorDTO>().ReverseMap();
            //image
            CreateMap<Image, ImageDTO>().ReverseMap();
            //option
            CreateMap<Option, OptionDTO>().ReverseMap();
            //product detail
            CreateMap<Detail, DetailDTO>().ReverseMap();
            //product color
            CreateMap<ProductColor, ProductColorDTO>().ReverseMap();
            CreateMap<ProductColor, ProductColorRequest>().ReverseMap();
            //Product option
            CreateMap<ProductOption, ProductOptionDTO>().ReverseMap();
            CreateMap<ProductOption, ProductOptionRequest>().ReverseMap();
            CreateMap<ProductOptionRequest, ProductOption >().ReverseMap();
            //Appuser
            CreateMap<AppUser, UserDTO>().ReverseMap();
            
            //Register
            CreateMap<AppUser, RegisterRequest>().ReverseMap();
            //order
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, PaymentRequest>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            
        }
    }
}
