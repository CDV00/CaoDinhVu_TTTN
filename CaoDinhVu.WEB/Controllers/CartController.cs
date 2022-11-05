using CaoDinhVu.BLL.Services;
using CaoDinhVu.WEB.Extensions;
using Entities.Constants;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Mvc;


namespace CaoDinhVu.WEB.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductColorService _productColorService;
        private readonly IProductOptionService _productOptionService;
        private readonly IProductSevice _productSevice;
        public CartController(IProductOptionService productOptionService, IProductColorService productColorService, IProductSevice productSevice)
        {
            _productColorService = productColorService;
            _productOptionService = productOptionService;
            _productSevice = productSevice;
        }
        //public const string CARTKEY = "cart";

        

        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("Cart");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }

        public IActionResult Index()
        {
            //List<CartItem> cart = new List<CartItem>();
            //var cart =  HttpContext.Session.GetString(CARTKEY).ToArray();
            //var cartsss = Carts;
            return View(Carts);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productid, Guid colorId, Guid optionId, int? Quantity)
        {
            if (Quantity == null)
                Quantity = 1;
            Guid productColorId =_productColorService.GetProductColorId(productid, colorId);
            var productOption =await _productOptionService.GetByProductColor(productColorId, optionId);
            //var product = await _productOptionService.GetCartById(productid, optionId, colorId);
            if (productOption == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = Carts;
            var cartitem = cart.Where(p => p.ProductOption.Option.Id == productOption.Option.Id 
                                           && p.ProductOption.Product.Id == productOption.Product.Id
                                           && p.ProductOption.ProductColor.Color.Id == productOption.ProductColor.Color.Id)
                                           //&& p.Product.ProductColors.Any(po=>po.ProductOptions.Any(p) == optionId))
                                           .FirstOrDefault();
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity += Quantity.Value;
                HttpContext.Session.Set("Cart", cart);
                //SaveCartSession(cart);
                //return Json(JsonConvert.SerializeObject( new { Message = "ThemSanPham" }));
                int CartCount = cart.Count;
                Cart.countCart = CartCount;
                Res res = new Res() { Mess = "ThemMoi", CartCount = CartCount };
                return Json(JsonConvert.SerializeObject(res));
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { quantity = Quantity.Value, ProductOption = productOption });

                HttpContext.Session.Set("Cart", cart);
                int CartCount = cart.Count;
                Cart.countCart = CartCount;
                Res res = new Res() { Mess = "ThemMoi", CartCount = CartCount };
                return Json(JsonConvert.SerializeObject(res));
            }

            // Lưu cart vào Session
            // Chuyển đến trang hiện thị Cart
            //return RedirectToAction(nameof(Cart));
        }
        public IActionResult Delete(Guid productOptionId)
        {
            var cart = Carts;
            var cartitem = cart.Where(x => x.ProductOption.Id == productOptionId).FirstOrDefault();

            string name = cartitem.ProductOption.Product.Name;
            //var cartItem = carts.RemoveAll(x => x.ProductOption.Id == productOptionId);

            cart.Remove(cartitem);

            HttpContext.Session.Set("Cart", cart);
            //Session["cart"] = sesstionCart;
            //Session["count"] = Int32.Parse(Session["count"].ToString()) - 1;
            int CartCount = cart.Count;
            Cart.countCart = CartCount;
            Res res = new Res() { Mess = "Xóa sản phẩm "+ name+"thành công!", CartCount = CartCount };
            return Json(JsonConvert.SerializeObject(res));

        }
        public IActionResult Update(int quantity, Guid productOptionId)
        {

            var cart = Carts;
            var cartitem = cart.Where(x => x.ProductOption.Id == productOptionId).FirstOrDefault();

            string name = cartitem.ProductOption.Product.Name;


            cartitem.quantity = quantity;

            HttpContext.Session.Set("Cart", cart);

            //Session["cart"] = sesstionCart;
            //Session["count"] = Int32.Parse(Session["count"].ToString()) - 1;
            Res res = new Res() { Mess = "cập nhập số lượng sản phẩm "+ name+" là "+quantity+" thành công!"};
            return Json(JsonConvert.SerializeObject(res));

        }

        /*List<CartItem> GetCartItems()
        {
            
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }*/

        // Xóa cart khỏi session
        /* void ClearCart()
         {
             var session = HttpContext.Session;
             session.Remove(CARTKEY);
         }*/

        // Lưu Cart (Danh sách CartItem) vào session
        /* void SaveCartSession(List<CartItem> ls)
         {
             try
             {
                 var session = HttpContext.Session;
                 string jsoncart = JsonConvert.SerializeObject(ls);
                 session.SetString(CARTKEY, jsoncart);
             }
             catch (Exception ex)
             {

                 Console.WriteLine(ex.Message);
             }

         }*/


    }
    public class Res
    {
        public string Mess { set; get; }
        public int? CartCount { set; get; }
    }
    /*public class CartItem
    {
        public int quantity { set; get; }
        public ProductDTO Product { set; get; }
    }*/
}
