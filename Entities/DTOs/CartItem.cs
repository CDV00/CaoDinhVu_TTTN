using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CartItem
    {
        public int quantity { set; get; }
        public ProductOptionCartItem ProductOption { set; get; }
        public decimal TotalPrice {get;set;}
    }
    public class ProductCartItem:BaseDTO
    {
        public string Name { get; set; }
        public string Thumbnails { get; set; }
        public string Slug { get; set; }
        public Guid CategoryId { get; set; }
        public virtual CategoryDTO Category { get; set; }
        public Guid BrandId { get; set; }
        public virtual BrandDTO Brand { get; set; }
    }
    public class ProductColorCartItem : BaseDTO
    {
        public virtual ColorDTO Color { get; set; }
    }
    public class ProductOptionCartItem : BaseDTO
    {
        public virtual ProductCartItem Product { get; set; }
        public virtual ProductColorCartItem ProductColor { get; set; }
        public virtual OptionDTO Option { get; set; }
        public decimal Price { get; set; }
    }




}
