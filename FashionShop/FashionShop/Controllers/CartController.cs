using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models;
using Model.ADO;
using System.Web.Script.Serialization;

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
            return View(listItems);
        }

        [HttpGet]
        public ActionResult _OrderPartial ()
        {
            var cart = Session[CART_SESSION];
            var listItems = new List<CartModel>();
            if (cart != null)
            {
                listItems = (List<CartModel>)cart;
            }
            return PartialView(listItems);
        }
         [HttpPost]
        public ActionResult _OrderPartial(string name, string email, string phone, string address)
        {
            var order = new Model.EF.order();
            order.order_name = name;
            order.order_email = email;
            order.order_phone = phone;
            order.order_address = address;


            return PartialView();
        }


        // Using Ajax Jquery update, delete Cart
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartModel>>(cartModel);
            var sessionCart = (List<CartModel>)Session[CART_SESSION];

            foreach (var item in sessionCart)
            {   // SingleOrDefault trả về phần từ duy nhất của chuỗi, or giá trị mặc định khi chuỗi trống.
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.product_id == item.Product.product_id);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CART_SESSION] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(int product_id)
        {
            var sessionCart = (List<CartModel>)Session[CART_SESSION];
            sessionCart.RemoveAll(x => x.Product.product_id == product_id);
            Session[CART_SESSION] = sessionCart;
            return Json(new
            {
                status = true
            });
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