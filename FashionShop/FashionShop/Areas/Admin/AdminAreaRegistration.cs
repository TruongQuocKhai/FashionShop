using System.Web.Mvc;

namespace FashionShop.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //    context.MapRoute(
            //    "Admin_default",
            //    "Admin/san-pham",
            //    new { controller = "Product", action = "Index", id = UrlParameter.Optional }
            //   namespaces: new[] { "FashionShop.Areas.Admin.Controllers" }
            //);

            //    context.MapRoute(
            //    "Admin_default",
            //    "Admin",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] {"FashionShop.Areas.Admin.Controllers"}
            //);

            context.MapRoute(
                "Product",
                "Admin/san-pham",
                new { controller = "Product", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_default",
                "Admin",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}