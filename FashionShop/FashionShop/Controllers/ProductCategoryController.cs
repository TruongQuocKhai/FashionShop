using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ADO;

namespace FashionShop.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        [ChildActionOnly]
        public ActionResult _ProductCategoryPartial()
        {
            var model = new ProductCategoryADO().GetListProductCategory();
            return PartialView(model);
        }
    }
}