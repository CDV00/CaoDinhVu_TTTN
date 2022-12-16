using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Constants
{
    public class Product
    {
        public static Guid ProductColorId { get; set; }
        public static Decimal Price { get; set; }
    }
    public class Cart
    {
        public static int countCart { get; set; } = 0;
    }
    public static class ActionChangeOrder
    {
        public static string UP = "UP";
        public static string DOWN = "DOWN";
    }
    public class Search
    {
        public static string KeyWork { get; set; }
    }
    public class FilterRequestConstan
    {
        public static Guid? Brand { get; set; }
        public static Guid? Category { get; set; }
        public static Guid? Color { get; set; }
        public static Guid? Option { get; set; }
        public static decimal? PriceMin { get; set; }
        public static decimal? PriceMax { get; set; }
        public static string KeyWork { get; set; }
        public static int? Page { get; set; }
        public static int? PageSize { get; set; }
    }
}
