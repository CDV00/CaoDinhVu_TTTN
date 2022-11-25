using CaoDinhVu.DAL.Data;
using Entities.Models;
using Query.Queries;
using Query.Queries.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementations
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly DBContext _context;
        public SliderRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public ISliderQuery BuildQuery()
        {
            return new SliderQuery(_context.Sliders.AsQueryable(), _context);
        }
    }
}
