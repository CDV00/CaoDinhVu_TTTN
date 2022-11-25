using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DTOs;
using Repository.Repositories;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class SliderService: ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IMapper _mapper;

        public SliderService(ISliderRepository sliderRepository, IMapper mapper)
        {
            _sliderRepository = sliderRepository;
            _mapper = mapper;
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
    }
}
