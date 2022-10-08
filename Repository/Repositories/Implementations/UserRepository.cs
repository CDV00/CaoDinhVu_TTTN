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
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        private DBContext _context;
        public UserRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public IUserQuery BuildQuery()
        {
            return new UserQuery(_context.AppUsers.AsQueryable(), _context);
        }
    }
}
