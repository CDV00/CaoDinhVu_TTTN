using Entities.Models;
using Query.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        ICategoryQuery BuildQuery();
        void ChangeOrder(int order, int? orderOld = 0);
        bool CheckExists(Guid id);
    }
}
