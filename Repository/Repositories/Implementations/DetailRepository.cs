using CaoDinhVu.DAL.Data;
using Entities.Models;

namespace Repository.Repositories.Implementations
{
    public class DetailRepository : Repository<Detail>, IDetailRepository
    {
        private readonly DBContext _context;
        public DetailRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        /*public IColorQuery BuildQuery()
        {
            return new DetailQuery(_context.Details.AsQueryable(), _context);
        }*/
    }
}
