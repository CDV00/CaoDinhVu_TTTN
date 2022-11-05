using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses
{
    public class PagingResponse<T> : BaseResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalProduct { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<T> Data { get; set; }
        public PagingResponse() {}
        public PagingResponse(Paging paging, IEnumerable<T> data)
        {
            Page = paging.page;
            PageSize = paging.pageSize;
            TotalProduct = paging.totalProduct;
            decimal totalpage = Convert.ToDecimal(paging.totalProduct)/ paging.pageSize;

            TotalPage = (int)Math.Ceiling(totalpage);
            //TotalPage =  Convert.ToInt32( Math.Ceiling(Convert.ToDecimal(paging.totalProduct / paging.pageSize)));


            Data = data;
        }
        public PagingResponse(IEnumerable<T> data)
        {
            Data = data;
        }
        public PagingResponse(bool isSuccess, string message) : base(isSuccess, message) { }
    }
    public class Paging{
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 2;
        public int totalProduct { get; set; }

        public Paging(int Page, int PageSize, int TotalProduct)
        {

            page = Page;
            pageSize = PageSize;
            totalProduct = TotalProduct;
        }
    }
}
