using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ADO;

namespace FashionShop.Controllers
{
    public class IntroduceController : Controller
    {
        // GET: Introduce
        public ActionResult Index()
        {
            var model = new IntroduceADO().GetIntroduceInfo();
            return View(model);
        }
    }
}