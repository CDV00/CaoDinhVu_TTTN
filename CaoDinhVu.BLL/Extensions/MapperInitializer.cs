using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Extensions
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Brand, BrandDTO>().ReverseMap();
            CreateMap<Slider, SliderDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ListProductDTO>().ReverseMap();
            CreateMap<Color, ColorDTO>().ReverseMap();
            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<Option, OptionDTO>().ReverseMap();
            CreateMap<ProductColor, ProductColorDTO>().ReverseMap();
            CreateMap<ProductOption, ProductOptionDTO>().ReverseMap();
            CreateMap<AppUser, UserDTO>().ReverseMap();
            //cartItem
            CreateMap<Product, ProductCartItem>().ReverseMap();
            CreateMap<ProductColor, ProductColorCartItem>().ReverseMap();
            CreateMap<ProductOption, ProductOptionCartItem>().ReverseMap();
            //Register
            CreateMap<AppUser, RegisterRequest>().ReverseMap();
            //
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, PaymentRequest>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();

        }
    }
}
