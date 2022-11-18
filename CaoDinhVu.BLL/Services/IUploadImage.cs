using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services
{
    public interface IUploadImage
    {
        Task<string> UploadImageToImgur(IFormFile files);
    }
}
