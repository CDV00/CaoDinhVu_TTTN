using System.Collections.Generic;
using System.Threading.Tasks;
using Repository.Repositories;
using AutoMapper;
using Entities.DTOs;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class ColorService : IColorService
    {
        private readonly IMapper _mapper;
        private readonly IColorRepository _colorRepository;

        public ColorService(IMapper mapper,IColorRepository colorRepository)
        {
            _mapper = mapper;
            _colorRepository = colorRepository;
        }

        public async Task<List<ColorDTO>> GetALL()
        {
            var colors = await _colorRepository.BuildQuery().ToListAsync(c => _mapper.Map<ColorDTO>(c));
            return colors;
        }
    }

}
