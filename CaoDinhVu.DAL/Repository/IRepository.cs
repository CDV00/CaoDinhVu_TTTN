using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid Id);
        Task CreateAsync(T _object);
        Task CreateRangeAsync(List<T> _object);
        bool Update(T _object);
        void Remove(object _object, bool? permanent);
        void RemoveRange(List<T> _object);
    }
}
