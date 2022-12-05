using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using AutoMapper;
using Entities.DTOs;
using Entities.Responses;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class OptionService : IOptionService
    {
        private readonly IMapper _mapper;
        private readonly IOptionRepository _optionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OptionService(IMapper mapper,IOptionRepository optionRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _optionRepository = optionRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<OptionDTO>> GetALL()
        {
            var options = await _optionRepository.BuildQuery().ToListNoTrackingAsync(o=> _mapper.Map<OptionDTO>(o));
            return options;
        }
        public Task<List<Option>> getbyProductcolorId(Guid id)
        {
            throw new NotImplementedException();
        }
        //public bool
        public async Task<BaseResponse> AddAsync(OptionDTO optionRequest)
        {
            try
            {
                if (_optionRepository.CheckExist(optionRequest.RAM, optionRequest.ROM))
                    return new BaseResponse(false, "Option đã tồn tại");
                var option = _mapper.Map<Option>(optionRequest);
                await _optionRepository.CreateAsync(option);
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
