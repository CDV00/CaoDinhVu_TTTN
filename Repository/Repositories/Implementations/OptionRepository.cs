using CaoDinhVu.DAL.Data;
using Entities.Models;
using Query.Queries;
using Query.Queries.Implementations;

namespace Repository.Repositories.Implementations
{
    public class OptionRepository : Repository<Option>, IOptionRepository
    {
        private readonly DBContext _context;
        public OptionRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public IOptionQuery BuildQuery()
        {
            return new OptionQuery(_context.Options.AsQueryable(), _context);
        }
    }
}
