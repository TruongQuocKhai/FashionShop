using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ADO;

namespace FashionShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Slide = new SlideADO().GetListSlide();
            return View();
        }
        [ChildActionOnly] // Chỉ gọi PartialView ko gọi tạo thành trang
        public ActionResult _MainMenuPartial()
        {
            // parameter = 1: Main menu type
            var menu = new MainMenuADO().GetListMenuByMenuTypeId(1);
            return PartialView(menu);
        }
         [ChildActionOnly]
        public ActionResult _FooterPartial()
        {
            var footer = new FooterADO().GetFooter();
            return PartialView(footer);
        }
        [ChildActionOnly]
        public ActionResult _BrandsPartial()
        {
            return PartialView();
        }
    }
}