using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ADO;

namespace FashionShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult _ListNewProductsPartial()
        {
            ViewBag.ListNewProducts = new ProductADO().GetListNewProduct(4);
            return PartialView();
        }

        public ActionResult _ListDiscountProductsPartial()
        {
            var model = new ProductADO().GetListDiscountProducts(8);
            return PartialView(model);
        }
    }
}