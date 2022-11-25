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
        private readonly DBContext _context;
        public UserRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public IUserQuery BuildQuery()
        {
            return new UserQuery(_context.AppUsers.AsQueryable(), _context);
        }
        public int checkRole(Guid userId)
        {
            var roleId = _context.UserRoles.Where(m => m.UserId == userId).Select(r => r.RoleId).FirstOrDefault();
            var roleName = _context.Roles.Where(m => m.Id == roleId).Select(r => r.Name).FirstOrDefault();
            if (roleName == UserRoles.Admin)
                return 1;
            else
            {
                if (roleName == UserRoles.Customer)
                    return 2;
                return 3;
            }
        }
    }
}
