﻿using CaoDinhVu.DAL.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Query.Queries.Implementations
{
    public class ProductQuery : QueryBase<Product>, IProductQuery
    {
        private readonly DBContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public ProductQuery(IQueryable<Product> productQuery, DBContext dbContext) : base(productQuery)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public IProductQuery FilterProductId(Guid? productId)
        {
            Query = Query.Where(type => type.Id == productId);
            return this;
        }
        public IProductQuery FilterBrandId(Guid? id)
        {
            if (id.Equals(Guid.Empty))
                return this;
            Query = Query.Where(type => type.BrandId == id);
            return this;
        } 
        public IProductQuery FilterCategoryId(Guid? id)
        {
            if (id.Equals(Guid.Empty))
                return this;
            Query = Query.Where(type => type.CategoryId == id);
            return this;
        }
        public IProductQuery FilterProductColor(Guid productId)
        {
            Query = Query.Where(type => type.ProductColors.Any(pc => pc.Product.Id == productId));
            return this;
        }
        public IProductQuery IncludeProductColor(int status)
        {
            if(status == 1)
                Query.Include(p => p.ProductColors.Where(c=>c.Status == status)).Load();
            else
                Query.Include(p => p.ProductColors).Load();
            return this;
        }
        public IProductQuery IncludeColor()
        {
            Query.Include(p => p.ProductColors).ThenInclude(c=>c.Color).Load();
            return this;
        }
        public IProductQuery IncludeDetail()
        {
            Query.Include(p => p.Detail).Load();
            
            return this;
        }
        public IProductQuery IncludeOption()
        {
            Query.Include(p => p.ProductOptions).ThenInclude(po=>po.Option).Load();
            return this;
        }
        public IProductQuery IncludeProductOption(int status)
        {
            if (status == 1)
                Query.Include(p => p.ProductOptions.Where(c => c.Status == status)).Load();
            else
                Query.Include(pc=>pc.ProductOptions).Load();
            return this;
        }
        public IProductQuery IncludeCategory()
        {
            Query.Include(p => p.Category).Load();
            return this;
        }
        public IProductQuery IncludeBrand()
        {
            Query.Include(p => p.Brand).Load();
            return this;
        }
        public IProductQuery IncludeImage()
        {
            Query.Include(p => p.Images).Load();
            return this;
        }
        public IProductQuery FilterByKeyword(string? keyWork = null)
        {
            if (keyWork == null)
                return this;
            string KeyWork = keyWork.ToUpper();
            Query = Query.Where(type => type.Name.ToUpper().Contains(KeyWork) ||
                                        type.Brand.Name.ToUpper().Contains(KeyWork) ||
                                        type.Category.Name.ToUpper().Contains(KeyWork)
                                );
            return this;
        }
        public IProductQuery FilterStatus(int status)
        {
            if(status == 0)
            {
                Query = Query.Where(p => p.Status == 0);
            }
            else if(status == 1)
            {
                Query = Query.Where(p => p.Status == 1);
            }
            else
            {
                Query = Query.Where(p => p.Status == 1 || p.Status ==2);
            }
            return this;
        }

        public IProductQuery FilterByOptionId(Guid? optionId)
        {
            if(optionId.Equals(Guid.Empty))
            {
                return this;
            }
            Query.Include(p => p.ProductOptions).ThenInclude(po => po.Option).Load();
            Query = Query.Where(type => type.ProductOptions.Any(po=>po.Option.Id == optionId));
            return this;
        }
        public IProductQuery FilterByColorId(Guid? colorId)
        {
            if (colorId.Equals(Guid.Empty))
            {
                return this;
            }
            //Query.Include(p=>p.ProductColors).ThenInclude(c => c.Color).Load();
            Query = Query.Where(type => type.ProductColors.Any(pc => pc.Color.Id == colorId ));
            return this;
        }

        public IProductQuery FilterByPriceMin(decimal? price)
        {
            if (price == 0)
            {
                return this;
            }
            //Query.Include(p=>p.ProductColors).ThenInclude(c => c.Color).Load();
            Query = Query.Where(type => type.Price >= price);
            return this;
        }
        public IProductQuery FilterByPriceMax(decimal? price)
        {
            if (price == 0)
            {
                return this;
            }
            //Query.Include(p=>p.ProductColors).ThenInclude(c => c.Color).Load();
            Query = Query.Where(type => type.Price <= price);
            return this;
        }
    }
}
