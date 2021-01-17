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
            context.MapRoute(
              "Add logout",
              "Admin/dang-xuat",
              new { controller = "Login", action = "Logout", id = UrlParameter.Optional },
              namespaces: new[] { "FashionShop.Areas.Admin.Controllers" }
          );

            context.MapRoute(
               "Add user",
               "Admin/them-user",
               new { controller = "User", action = "AddUser", id = UrlParameter.Optional },
               namespaces: new[] { "FashionShop.Areas.Admin.Controllers" }
           );

            context.MapRoute(
                "User",
                "Admin/quan-ly-user",
                new { controller = "User", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Areas.Admin.Controllers" }
            );

            context.MapRoute(
               "Add Products",
               "Admin/them-san-pham",
               new { controller = "Product", action = "AddProduct", id = UrlParameter.Optional },
               namespaces: new[] { "FashionShop.Areas.Admin.Controllers" }
           );

            context.MapRoute(
                "Product",
                "Admin/quan-ly-san-pham",
                new { controller = "Product", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Dashboard",
                "Admin/dashboard",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin login",
                "Admin",
                new { controller = "Login", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Areas.Admin.Controllers" }
            );
        }
    }
}