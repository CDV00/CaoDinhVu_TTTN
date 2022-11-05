using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests
{
    public class PagingRequest
    {
        public int? page { get; set; }
        public int? pageSize { get; set; }
        public Guid? id { get; set; }
        public PagingRequest(Guid Id)
        {
            page = 1;
            pageSize = 10;
            id = Id;
        }
        public PagingRequest(int? Page, int? PageSite)
        {
            page = Page;
            pageSize = PageSite;
        }
        public PagingRequest(Guid Id, int? Page,int? PageSite)
        {
            page = 1;
            pageSize = 10;
            id = Id;
        }
    }
}
