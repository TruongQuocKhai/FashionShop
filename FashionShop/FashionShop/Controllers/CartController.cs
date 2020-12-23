using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart

        public ActionResult _HeaderCartPartial()
        {
            return PartialView();
        }
    }
}