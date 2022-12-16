using Entities.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Repository.Repositories;
using AutoMapper;
using Entities.Responses;
using Entities.Requests;
using Entities.Models;
using Repository.Repositories.Implementations;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class ProductSevice : IProductSevice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        public IMapper _mapper { get; }

        private readonly IProductColorService _productColorService;
        private readonly IProductOptionService _productOptionService;
        private readonly IDetailRepository _detailRepository;
        private readonly IImageRepository _imageRepository;

        public ProductSevice(IUnitOfWork unitOfWork,IProductRepository productRepository, IMapper mapper,
            IProductColorService productColorService,
            IProductOptionService productOptionService,
            IDetailRepository detailRepository,
            IImageRepository imageRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _mapper = mapper;
            _productColorService = productColorService;
            _productOptionService = productOptionService;
            _detailRepository = detailRepository;
            _imageRepository = imageRepository;
        }
        public async Task<List<ListProductDTO>> GetAll(int? status = 1)
        {
            try
            {

                var listProducts = await _productRepository.BuildQuery()
                                                           .FilterStatus(status.Value)
                                                           .IncludeBrand()
                                                           .IncludeCategory()
                                                           .ToListAsync(p => _mapper.Map<ListProductDTO>(p));

                return listProducts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }
        public async Task<PagingResponse<ListProductDTO>> GetAllNoTracking(PagingRequest pagingRequest, int? status = 1)
        {
            try
            {
                var listProducts = await _productRepository.BuildQuery()
                                                           .FilterStatus(status.Value)
                                                           .Skip((pagingRequest.page - 1) * pagingRequest.pageSize)
                                                           .Take(pagingRequest.pageSize)
                                                           .ToListNoTrackingAsync(p => _mapper.Map<ListProductDTO>(p));

                int totalProducts = await _productRepository.BuildQuery()
                                                           .CountAsync();
                return new PagingResponse<ListProductDTO>(new Paging(pagingRequest.page.Value, pagingRequest.pageSize.Value, totalProducts), listProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PagingResponse<ListProductDTO>(false, "Something went wrong. " + ex.Message);

            }
        }
        public async Task<PagingResponse<ListProductDTO>> GetAll(PagingRequest pagingRequest, int? status = 1)
        {
            try
            {
                var listProducts = await _productRepository.BuildQuery()
                                                           .FilterStatus(status.Value)
                                                           .Skip((pagingRequest.page - 1) * pagingRequest.pageSize)
                                                           .Take(pagingRequest.pageSize)
                                                           .ToListAsync(p => _mapper.Map<ListProductDTO>(p));

                int totalProducts = await _productRepository.BuildQuery()
                                                           .CountAsync();

                /*var builquery = await _productRepository.BuildQuery()
                                                           .IncludeBrand()
                                                           .IncludeCategory()
                                                           .ToListAsync(p => _mapper.Map<ListProductDTO>(p));*/
                //var nobuilquery = await _productRepository.GetAllTest();





                return new PagingResponse<ListProductDTO>(new Paging(pagingRequest.page.Value, pagingRequest.pageSize.Value, totalProducts), listProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PagingResponse<ListProductDTO>(false, "Something went wrong. " + ex.Message);

            }
        }

        public async Task<ProductDTO> GetById(Guid id, int? status = 2)
        {
            try
            {
                var listProducts = await _productRepository.BuildQuery()
                                                           .FilterProductId(id)
                                                           .IncludeDetail()
                                                           .IncludeBrand()
                                                           .IncludeCategory()
                                                           .IncludeImage()
                                                           .IncludeProductColor(status.Value)
                                                           .IncludeColor()
                                                           .IncludeProductOption(status.Value)
                                                           .IncludeOption()
                                                           .AsSelectorAsync(p =>_mapper.Map<ProductDTO>(p));
                return listProducts;
            }
            catch (Exception ex)
            {

                return null;
                throw new("Something went wrong. " + ex.Message);
            }
        }

        public async Task<ProductCartItem> GetCartById(Guid productId, Guid optionId, Guid colorId)
        {
            try
            {
                var listProducts = await _productRepository.BuildQuery()
                                                           .FilterProductId(productId)
                                                           .FilterByColorId(colorId)
                                                           //.FilterByOptionId(colorId)
                                                           .IncludeProductColor(1)
                                                           .IncludeColor()
                                                           .IncludeProductOption(1)
                                                           .IncludeOption()
                                                           .AsSelectorAsync(p => _mapper.Map<ProductCartItem>(p));
                return listProducts;
            }
            catch (Exception ex)
            {

                return null;
                throw new("Something went wrong. " + ex.Message);
            }
        }


        public async Task<PagingResponse<ListProductDTO>> GetByCategoryId(PagingRequest pagingRequest)
        {
            try
            {
                var listProducts = await _productRepository.BuildQuery()
                                                           .FilterCategoryId(pagingRequest.id.Value)
                                                           .Skip((pagingRequest.page - 1) * pagingRequest.pageSize)
                                                           .Take(pagingRequest.pageSize)
                                                           .ToListNoTrackingAsync(p => _mapper.Map<ListProductDTO>(p));
                int totalProducts = await _productRepository.BuildQuery()
                                                           .FilterCategoryId(pagingRequest.id.Value)
                                                           .CountAsync();


                //int totalPage = Math.Floor( totalProducts/pagingRequest.pageSize);
                return new PagingResponse<ListProductDTO>(new Paging(pagingRequest.page.Value, pagingRequest.pageSize.Value, totalProducts), listProducts);
            }
            catch (Exception ex)
            {

                return new PagingResponse<ListProductDTO>(false, "Something went wrong. " + ex.Message);
            }
        }
        public async Task<PagingResponse<ListProductDTO>> GetByBrandId(PagingRequest pagingRequest)
        {
            try
            {
                var listProducts = await _productRepository.BuildQuery()
                                                           .FilterBrandId(pagingRequest.id.Value)
                                                           .Skip((pagingRequest.page - 1) * pagingRequest.pageSize)
                                                           .Take(pagingRequest.pageSize)
                                                           .ToListNoTrackingAsync(p => _mapper.Map<ListProductDTO>(p));

                int totalProducts = await _productRepository.BuildQuery()
                                                           .FilterBrandId(pagingRequest.id.Value)
                                                           .CountAsync();
                return new PagingResponse<ListProductDTO>(new Paging(pagingRequest.page.Value, pagingRequest.pageSize.Value, totalProducts), listProducts);
            }
            catch (Exception ex)
            {
                return new PagingResponse<ListProductDTO>(false, "Something went wrong. " + ex.Message);
            }
        }
        public async Task<PagingResponse<ListProductDTO>>GetByKeyword( PagingRequest pagingRequest ,string keyWork)
        {
            try 
	        {
                var listProducts = await _productRepository.BuildQuery()
                                                           .FilterByKeyword(keyWork)
                                                           .Skip((pagingRequest.page - 1) * pagingRequest.pageSize)
                                                           .Take(pagingRequest.pageSize)
                                                           .ToListNoTrackingAsync(p => _mapper.Map<ListProductDTO>(p));

                int totalProducts = await _productRepository.BuildQuery()
                                                           .FilterByKeyword(keyWork)
                                                           .CountAsync();


                //int totalPage = Math.Floor( totalProducts/pagingRequest.pageSize);
                return new PagingResponse<ListProductDTO>( new Paging(pagingRequest.page.Value, pagingRequest.pageSize.Value, totalProducts),listProducts);
            }
	        catch (Exception ex)
	        {

                return new PagingResponse<ListProductDTO>(false, "Something went wrong. " + ex.Message);
            }
        }
        public async Task<PagingResponse<ListProductDTO>> Filter(FilterRequest filterRequest)
        {
            try
            {
                var listProducts = await _productRepository.BuildQuery()
                                                           .FilterStatus(1)
                                                           .FilterByKeyword(filterRequest.KeyWork)
                                                           .FilterBrandId(filterRequest.BrandId)
                                                           .FilterCategoryId(filterRequest.CategoryId)
                                                           .FilterByColorId(filterRequest.ColorId)
                                                           .FilterByOptionId(filterRequest.OptionId)
                                                           .FilterByPriceMin(filterRequest.PriceMin)
                                                           .FilterByPriceMax(filterRequest.PriceMax)
                                                           .Skip((filterRequest.Page - 1) * filterRequest.PageSize)
                                                           .Take(filterRequest.PageSize)
                                                           .ToListAsync(p => _mapper.Map<ListProductDTO>(p));

                int totalProducts = await _productRepository.BuildQuery()
                                                           .FilterStatus(1)
                                                           .FilterByKeyword(filterRequest.KeyWork)
                                                           .FilterBrandId(filterRequest.BrandId)
                                                           .FilterCategoryId(filterRequest.CategoryId)
                                                           .FilterByColorId(filterRequest.ColorId)
                                                           .FilterByOptionId(filterRequest.OptionId)
                                                           .FilterByPriceMin(filterRequest.PriceMin)
                                                           .FilterByPriceMax(filterRequest.PriceMax)
                                                           .CountAsync();


                //int totalPage = Math.Floor( totalProducts/pagingRequest.pageSize);
                return new PagingResponse<ListProductDTO>(new Paging(filterRequest.Page.Value, filterRequest.PageSize.Value, totalProducts), listProducts);
            }
            catch (Exception ex)
            {

                return new PagingResponse<ListProductDTO>(false, "Something went wrong. " + ex.Message);
            }
        }

        public async Task<BaseResponse> AddAsync(ProductRequest productRequest)
        {
            try
            {
                //var product = _mapper.Map<Product>(productRequest);

                var product = new Product()
                {
                    Name = productRequest.Name,
                    Thumbnails = productRequest.Thumbnails,
                    Slug = productRequest.Slug,
                    Title = productRequest.Title,
                    CategoryId = productRequest.CategoryId,
                    BrandId = productRequest.BrandId,
                    Description = productRequest.Description,
                    Price = 0,
                    Status = 0,
                    CreateAt = DateTime.UtcNow
                };
                await _productRepository.CreateAsync(product);

                var detail = new Detail()
                {
                    Screen = productRequest.Screen,
                    Camera = productRequest.Camera,
                    OperatingSystem = productRequest.OperatingSystem,
                    CPU = productRequest.CPU,
                    ROM = productRequest.ROM,
                    RAM = productRequest.RAM,
                    Connection = productRequest.Connection,
                    Battery = productRequest.Battery,
                    Charger = productRequest.Charger,
                    GeneralInformation = productRequest.GeneralInformation,
                    //ProductId = product.Id,
                };
                foreach (var item in productRequest.ListImage)
                {
                    var img = new Image()
                    {
                        Imglink = item.ToString(),
                        ProductId = product.Id
                    };
                    await _imageRepository.CreateAsync(img);
                }
                await _detailRepository.CreateAsync(detail);

                product.DetailId = detail.Id;
        await _unitOfWork.SaveChangesAsync();

                
                return new BaseResponse(true, product.Id.ToString());
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "Thêm sản phẩm thất bại " + ex);
            }
        }
        //change image Thumbnails
        public async Task<BaseResponse> ChangeThumbnails(Guid id, string Thumbnails)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                product.Thumbnails = Thumbnails;
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "thay đổi ảnh thành công thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "thay đổi ảnh thành công thất bại" + ex);
            }
        }
        //Add image Item
        public async Task<Response<ImageDTO>> AddImageItem(Guid id, string Thumbnails)
        {
            try
            {
                var img = new Image()
                {
                    Imglink = Thumbnails,
                    ProductId = id
                };
                await _imageRepository.CreateAsync(img);
                await _unitOfWork.SaveChangesAsync();
                return new Response<ImageDTO>(true, "Thêm ảnh thành công thành công",_mapper.Map<ImageDTO>(img));
            }
            catch (Exception ex)
            {
                return new Response<ImageDTO>(false, "Thêm ảnh thành công thất bại" + ex);
            }
        }
        //Add image Item
        public async Task<BaseResponse> DeleteImageItem(Guid id)
        {
            try
            {
                var img = await _imageRepository.GetByIdAsync(id);
                img.IsDelete = true;
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "xóa ảnh thành công thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "xóa ảnh thành công thất bại" + ex);
            }
        }
        //
        public async Task<BaseResponse> Update(ProductRequest productRequest)
        {
            try
            {

                var product = await _productRepository.GetByIdAsync(productRequest.Id.Value);
                var detail = await _detailRepository.GetByIdAsync(product.DetailId.Value);

                detail.Screen = productRequest.Screen;
                detail.Camera = productRequest.Camera;
                detail.OperatingSystem = productRequest.OperatingSystem;
                detail.CPU = productRequest.CPU;
                detail.ROM = productRequest.ROM;
                detail.RAM = productRequest.RAM;
                detail.Connection = productRequest.Connection;
                detail.Battery = productRequest.Battery;
                detail.Charger = productRequest.Charger;
                detail.GeneralInformation = productRequest.GeneralInformation;

                await _detailRepository.Update(detail);

                product.Name = productRequest.Name;
                product.Status = productRequest.Status;
                product.Title = productRequest.Title;
                product.CategoryId = productRequest.CategoryId;
                product.BrandId = productRequest.BrandId;
                product.Description = productRequest.Description;
                product.UpdateAt = DateTime.UtcNow;
                if(productRequest.Thumbnails != null)
                {
                    product.Thumbnails = productRequest.Thumbnails;
                }

                await _productRepository.Update(product);
                if(productRequest.ListImage.Count > 0)
                {
                    foreach (var item in productRequest.ListImage)
                    {
                        var img = new Image()
                        {
                            Imglink = item.ToString(),
                            ProductId = product.Id,
                            CreateAt = DateTime.UtcNow,
                            CreateBy = new Guid()
                            
                        };
                        await _imageRepository.CreateAsync(img);
                    }
                }
                
                /*var product = _mapper.Map<Product>(productRequest);
                var update =await _productRepository.Update(product);*/
                /*if (!update)
                    return new BaseResponse(true, "Update sản phẩm thất bại");*/
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Update thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "Update sản phẩm thất bại" + ex);
            }
        }
        /*public async Task<BaseResponse> Delete(Guid id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                


                product.IsDelete = true;
                var update =await _productRepository.Update(product);
                if (!update)
                    return new BaseResponse(true, "Delete sản phẩm thất bại");
                await _unitOfWork.SaveChangesAsync(); 
                //var productColor = _productColorService.GetIdByProductId(id);

                *//*using(var products = _productColorService.GetIdByProductId(id))
                {
                    foreach (var productColor in products)
                    {
                        var delete = await _productColorService.Delete(productColor);
                        if (!delete.IsSuccess)
                            return new BaseResponse(true, delete.Message);
                    }
                }*//*

                foreach (var productColor in _productColorService.GetIdByProductId(id))
                {
                   var delete = await _productColorService.Delete(productColor);
                   if(!delete.IsSuccess)
                        return new BaseResponse(true, delete.Message);
                }
                foreach (var productOption in _productOptionService.GetIdByProductId(id))
                {
                    var delete = await _productOptionService.Delete(productOption);
                    if (!delete.IsSuccess)
                        return new BaseResponse(true, delete.Message);
                }

                
                return new BaseResponse(true, "Delete thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "Delete sản phẩm thất bại" + ex);
            }
        }*/

        //
        public async Task<BaseResponse> ChangeStatus(Guid id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product.Status == 1)
                    product.Status = 2;
                else
                    product.Status = 1;

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, product.Status.ToString());
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "thay đổi trạng thái thất bại" + ex);
            }
        }
        /*public async Task<BaseResponse> DeleteSoft(Guid id)
        {
            try
            {
                var category = await _productRepository.GetByIdAsync(id);
                category.Status = 0;

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "xóa mềm thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "xóa mềm thất bại" + ex);
            }
        }*/

        public async Task<BaseResponse> DeleteSoft(Guid id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product.Status == 0)
                    product.Status = 2;
                else
                    product.Status = 0;

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, product.Id.ToString());
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "xóa mềm thất bại" + ex);
            }
        }
        public async Task<BaseResponse> Delete(Guid id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                product.IsDelete = true;
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, product.Id.ToString());
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "xóa thất bại" + ex);
            }
        }


    }
}
