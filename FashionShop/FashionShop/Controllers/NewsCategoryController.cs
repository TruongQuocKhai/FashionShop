using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ADO;

namespace FashionShop.Controllers
{
    public class NewsCategoryController : Controller
    {
        // GET: NewsCategory
        public ActionResult _NewsCategoryPartial()
        {
            var model = new NewsCategoryADO().GetListNewsCategory();
            return PartialView(model);
        }
    }
}