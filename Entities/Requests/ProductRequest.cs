using System;
using System.Collections.Generic;

namespace Entities.Requests
{
    public class ProductRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Thumbnails { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }=0;
        public int? Status { get; set; }
        public List<string> ListImage { get; set; }
        //public Guid ColorId { get; set; }
        //public Guid? OptionId { get; set; }
        //public decimal Prices { get; set; }
        //public int? Number { get; set; }
        //detail
        public string Screen { get; set; }
        public string Camera { get; set; }
        public string OperatingSystem { get; set; }
        public string CPU { get; set; }
        public string ROM { get; set; }
        public string RAM { get; set; }
        public string Connection { get; set; }
        public string Battery { get; set; }
        public string Charger { get; set; }
        public string GeneralInformation { get; set; }
    }
}
