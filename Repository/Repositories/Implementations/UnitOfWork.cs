using CaoDinhVu.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementations
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly DBContext _context;
        public UnitOfWork(DBContext context)
        {
            _context = context;

            //Posts = postRepository;
            //Categories = categoryRepository;
        }

        #region IUnitOfWork Members
        /// <summary>
        /// Dispose
        /// </summary>
        //public void Dispose()
        //{
        //    GC.SuppressFinalize(this);
        //    _context.Dispose();
        //}

        public  int SaveChanges()
        {
            return _context.SaveChanges();
        }
        /// <summary>
        /// Save all changes async
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
