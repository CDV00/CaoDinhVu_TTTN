using CaoDinhVu.DAL.Data;
using Entities.Models;
using Query.Queries;
using Query.Queries.Implementations;using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementations
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        private DBContext _context;
        public ImageRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public IImageQuery BuildQuery()
        {
            return new ImageQuery(_context.Images.AsQueryable(), _context);
        }
    }
}
