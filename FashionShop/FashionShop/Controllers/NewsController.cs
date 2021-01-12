using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ADO;

namespace FashionShop.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            var model = new ContentADO().GetInforContent();
            return View(model);
        }

        public ActionResult ContentDetail(int id)
        {
            var model = new ContentADO().GetContentId(id);
            return View(model);
        }

    }
}