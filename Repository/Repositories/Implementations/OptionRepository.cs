using CaoDinhVu.DAL.Data;
using Entities.Models;
using Query.Queries;
using Query.Queries.Implementations;
using System.Linq;

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

        public bool CheckExist(int RAM, int ROM)
        {
            return _context.Options.Any(o => o.RAM == RAM && o.ROM == ROM);
        }
    }
}
