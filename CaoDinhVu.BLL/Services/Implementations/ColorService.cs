using System.Collections.Generic;
using System.Threading.Tasks;
using Repository.Repositories;
using AutoMapper;
using Entities.DTOs;
using Entities.Responses;
using System;
using Entities.Models;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class ColorService : IColorService
    {
        private readonly IMapper _mapper;
        private readonly IColorRepository _colorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ColorService(IMapper mapper,IColorRepository colorRepository,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ColorDTO>> GetALL()
        {
            var colors = await _colorRepository.BuildQuery().ToListNoTrackingAsync(c => _mapper.Map<ColorDTO>(c));
            return colors;
        }
        public async Task<BaseResponse> AddAsync(ColorDTO colorRequest)
        {
            try
            {
                if(_colorRepository.CheckExist(colorRequest.Name))
                    return new BaseResponse(false, "Màu sắc đã tồn tại");
                var color = _mapper.Map<Color>(colorRequest);
                await _colorRepository.CreateAsync(color);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message);
                throw;
            }
        }
    }

}
