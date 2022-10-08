using CaoDinhVu.DAL.Data;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Query.Queries.Implementations
{
    public class CategoryQuery:QueryBase<Category>,ICategoryQuery
    {
        private readonly DBContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public CategoryQuery(IQueryable<Category> categoryQuery, DBContext dbContext) : base(categoryQuery)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public ICategoryQuery FiterById(Guid id)
        {
            Query = Query.Where(type => type.Id == id);
            return this;
        }
    }
}
