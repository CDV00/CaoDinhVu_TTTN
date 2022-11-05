using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using AutoMapper;
using Entities.DTOs;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class OptionService : IOptionService
    {
        private readonly IMapper _mapper;
        private readonly IOptionRepository _optionRepository;

        public OptionService(IMapper mapper,IOptionRepository optionRepository)
        {
            _mapper = mapper;
            _optionRepository = optionRepository;
        }
        public async Task<List<OptionDTO>> GetALL()
        {
            var options = await _optionRepository.BuildQuery().ToListAsync(o=> _mapper.Map<OptionDTO>(o));
            return options;
        }
        public Task<List<Option>> getbyProductcolorId(Guid id)
        {
            throw new NotImplementedException();
        }
    }

}
