using Entities.Models;
using Query.Queries;

namespace Repository.Repositories
{
    public interface IColordRepository : IRepository<Color>
    {
        IColorQuery BuildQuery();
    }
}
