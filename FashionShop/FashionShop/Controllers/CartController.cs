using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models;
using Model.ADO;

namespace FashionShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private const string CART_SESSION = "CART_SESSION";

        public ActionResult Index()
        {
            var cart = Session[CART_SESSION];
            var listItems = new List<CartModel>();
            if (cart != null)
            {
                listItems = (List<CartModel>)cart;
            }
            return View();
        }

        public ActionResult _HeaderCartPartial()
        {
            var cart = Session[CART_SESSION];
            var listItems = new List<CartModel>();
            if (cart != null)
            {
                listItems = (List<CartModel>)cart;
            }
            return PartialView(listItems);
        }

        public ActionResult AddItems(int productId, int quantity)
        {
            // get product id
            var product = new ProductADO().GetProductId(productId);
            var cart = Session[CART_SESSION];

            if (cart != null) // giỏ hàng đã tồn tại -> cộng số lượng 
            {
                var listItems = (List<CartModel>)cart;
                if (listItems.Exists(x => x.Product.product_id == productId))
                {
                    foreach (var item in listItems)
                    {
                        if (item.Product.product_id == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else // giỏ hàng đã tồn tại nhưng khác sản phẩm -> tạo mới
                {
                    var item = new CartModel();
                    item.Product = product;
                    item.Quantity = quantity;
                    listItems.Add(item);
                }
            }
            else // giỏ hàng chưa tồn tại -> tạo mới
            {
                var item = new CartModel();
                item.Product = product;
                item.Quantity = quantity;
                var listItems = new List<CartModel>();
                listItems.Add(item);
                Session[CART_SESSION] = listItems; // Assign to session
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

       
    }
}