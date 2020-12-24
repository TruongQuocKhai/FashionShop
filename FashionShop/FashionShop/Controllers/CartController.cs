using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ADO;

namespace FashionShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
      //  private string _cartSession = "CartSession"

        public ActionResult _HeaderCartPartial()
        {
            return PartialView();
        }

        public ActionResult AddItems(int productId, int quantity)
        {
            // get product id
            var objProduct = new ProductADO().GetProductId(productId);
            var
        }
    }
}