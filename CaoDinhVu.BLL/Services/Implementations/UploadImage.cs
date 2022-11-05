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
        public async Task<string> UploadImageToImgur(string imgUrl)
        {
            if(imgUrl == null)
                imgUrl = @"D:\HK3_2022\MyWebASP\CaoDinhVu_2119110067\alistyle-html-v2\images\items\1.jpg";
            string ClientId =  _configuration["ServerImage:ClientID"];
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Authorization", "Client-ID " + ClientId);
            System.Collections.Specialized.NameValueCollection Keys = new System.Collections.Specialized.NameValueCollection();
            try
            {
                Keys.Add("image", Convert.ToBase64String(File.ReadAllBytes(imgUrl)));
                byte[] responseArray = webClient.UploadValues("https://api.imgur.com/3/image", Keys);
                dynamic result = Encoding.ASCII.GetString(responseArray);
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("link\":\"(.*?)\"");
                Match match = reg.Match(result);
                string url = match.ToString().Replace("link\":\"", "").Replace("\"", "").Replace("\\/", "/");
                return await Task.FromResult(url);
            }
            catch (Exception ex)
            {
                return await Task.FromResult("Failed!");
                throw new("Something went wrong. " + ex.Message);
            }
        }
    }
}
