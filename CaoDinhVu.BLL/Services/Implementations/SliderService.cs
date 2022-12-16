using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Constants;
using Entities.DTOs;
using Entities.Models;
using Entities.Requests;
using Entities.Responses;
using Repository.Repositories;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class SliderService: ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SliderService(ISliderRepository sliderRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _sliderRepository = sliderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<SliderDTO>> getAll()
        {
            try
            {
                var listSliders = await _sliderRepository.BuildQuery().ToListNoTrackingAsync(s => _mapper.Map<SliderDTO>(s));
                return listSliders;
            }
            catch (Exception ex)
            {

                return null;
                throw new("Something went wrong. " + ex.Message);
            }
        }
        public async Task<SliderDTO> GetById(Guid id)
        {
            try
            {
                var listSlider = await _sliderRepository.GetByIdAsync(id);
                var result = _mapper.Map<SliderDTO>(listSlider);
                return result;
            }
            catch (Exception ex)
            {
                return null;
                throw new("Something went wrong. " + ex.Message);
            }
        }
        //
        public async Task<BaseResponse> Create(SliderRequest sliderRequest)
        {
            try
            {
                var slider = _mapper.Map<Slider>(sliderRequest);
                slider.CreateAt = DateTime.UtcNow;
                slider.CreateBy = new Guid();
                slider.Orders = _sliderRepository.MaxOrder()+1;
                await _sliderRepository.CreateAsync(slider);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Thêm thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "Thêm thất bại" + ex);
                throw new Exception(ex.Message);
            }
        }
        public async Task<BaseResponse> Update(Slider sliderRequest)
        {
            try
            {
                var slider = await _sliderRepository.GetByIdAsync(sliderRequest.Id);
                if(sliderRequest.Img != null)
                {
                    slider.Img = sliderRequest.Img;
                }
                slider.Status = sliderRequest.Status;
                if(sliderRequest.Orders != slider.Orders)
                {
                    _sliderRepository.ChangeOrder(slider,sliderRequest.Orders.Value);
                }

                slider.UpdateAt = DateTime.UtcNow;
                slider.UpdateBy = new Guid();
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Thêm thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "Thêm thất bại" + ex);
                throw new Exception(ex.Message);
            }
        }
        public async Task<BaseResponse> ChangeOrder(Guid id, string actionChange)
        {
            try
            {
                if(actionChange == ActionChangeOrder.UP)
                {
                    var slider = await _sliderRepository.GetByIdAsync(id);
                    _sliderRepository.ChangeOrder(slider, slider.Orders.Value - 1);
                }
                else
                {
                    var slider = await _sliderRepository.GetByIdAsync(id);
                    _sliderRepository.ChangeOrder(slider, slider.Orders.Value + 1);
                }

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, "Thành công");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "thay đổi trạng thái thất bại" + ex);
            }
        }

        public async Task<BaseResponse> ChangeStatus(Guid id)
        {
            try
            {
                var slider = await _sliderRepository.GetByIdAsync(id);
                if (slider.Status == 1)
                    slider.Status = 2;
                else
                    slider.Status = 1;

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, slider.Status.ToString());
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "thay đổi trạng thái thất bại" + ex);
            }
        }
        public  async Task<BaseResponse> DeleteSoft(Guid id)
        {
            try
            {
                var slider = await _sliderRepository.GetByIdAsync(id);
                if (slider.Status == 0)
                    slider.Status = 2;
                else
                    slider.Status = 0;

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, slider.Id.ToString());
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
                var slider = await _sliderRepository.GetByIdAsync(id);
                slider.IsDelete = true;

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, slider.Id.ToString());
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, "xóa thất bại" + ex);
            }
        }
    }
}
