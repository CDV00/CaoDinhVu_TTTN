using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class UploadImage:IUploadImage
    {
        private readonly IConfiguration _configuration;
        public UploadImage(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<string> UploadImageToImgur(IFormFile files)
        {
            try
            {
                //Luu file tam vao trong root
                string imgUrl = "";
                if (files.Length > 0)
                {
                    imgUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\" + files.FileName);
                    using (var stream = new FileStream(imgUrl, FileMode.Create))
                    {
                        files.CopyTo(stream);
                    }
                }
                //kiem tra file ton tai
                if (imgUrl is null)
                    throw new("Something went wrong. ");
                //luu img len Imgus
                if (imgUrl == null)
                    imgUrl = @"D:\HK3_2022\MyWebASP\CaoDinhVu_2119110067\alistyle-html-v2\images\items\1.jpg";
                string ClientId = _configuration["ServerImage:ClientID"];
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Authorization", "Client-ID " + ClientId);
                System.Collections.Specialized.NameValueCollection Keys = new System.Collections.Specialized.NameValueCollection();
                Keys.Add("image", Convert.ToBase64String(File.ReadAllBytes(imgUrl)));
                byte[] responseArray = webClient.UploadValues("https://api.imgur.com/3/image", Keys);
                dynamic result = Encoding.ASCII.GetString(responseArray);
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("link\":\"(.*?)\"");
                Match match = reg.Match(result);
                string url = match.ToString().Replace("link\":\"", "").Replace("\"", "").Replace("\\/", "/");
                //Xoa file luu tam trong root
                if (File.Exists(imgUrl))
                    File.Delete(imgUrl);
                //
                return await Task.FromResult(url);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(String.Empty);
                throw new("Something went wrong. " + ex.Message);
            }
        }
    }
}
