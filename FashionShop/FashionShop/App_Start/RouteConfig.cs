using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FashionShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Product Detail",
               url: "chi-tiet/{alias}-{id}",
               defaults: new { controller = "Product", action = "ViewProductDetails", id = UrlParameter.Optional },
               namespaces: new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
               name: "Product Category",
               url: "san-pham/{alias}-{id}",
               defaults: new { controller = "Product", action = "ListProductsByCategory", id = UrlParameter.Optional },
               namespaces: new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
                name: "All Products",
                url: "tat-ca-san-pham",
                defaults: new { controller = "Product", action = "ListAllProducts", id = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
             );

            routes.MapRoute(
                 name: "Order",
                 url: "dat-hang",
                 defaults: new { controller = "Cart", action = "Order", id = UrlParameter.Optional },
                 namespaces: new[] { "FashionShop.Controllers" }
             );

            routes.MapRoute(
               name: "Add the item to cart",
               url: "them-vao-gio-hang",
               defaults: new { controller = "Cart", action = "AddItems", id = UrlParameter.Optional },
               namespaces: new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
              name: "Cart",
              url: "gio-hang",
              defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
               name: "Success notificaiton",
               url: "dat-hang-thanh-cong",
               defaults: new { controller = "Cart", action = "SuccessNotification", id = UrlParameter.Optional },
               namespaces: new[] { "FashionShop.Controllers" }
             );

            routes.MapRoute(
             name: "Failed notificaiton",
             url: "dat-hang-khong-thanh-cong",
             defaults: new { controller = "Cart", action = "FailedNotification", id = UrlParameter.Optional },
             namespaces: new[] { "FashionShop.Controllers" }
            );


            routes.MapRoute(
                 name: "Registration",
                 url: "dang-ky",
                 defaults: new { controller = "User", action = "Registration", id = UrlParameter.Optional },
                 namespaces: new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
                 name: "Login",
                 url: "dang-nhap",
                 defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
                 namespaces: new[] { "FashionShop.Controllers" }
             );

            routes.MapRoute(
                name: "Logout",
                url: "dang-xuat",
                defaults: new { controller = "User", action = "Logout", id = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
               name: "Search",
               url: "tim-kiem",
               defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
               namespaces: new[] { "FashionShop.Controllers" }
           );

            routes.MapRoute(
                name: "News",
                url: "tin-tuc",
                defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
                name: "News Detail",
                url: "chi-tiet-tin-tuc/{alias}-{id}",
                defaults: new { controller = "News", action = "ContentDetail", id = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
                name: "Introduce",
                url: "gioi-thieu",
                defaults: new { controller = "Introduce", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FashionShop.Controllers" }
            );

            routes.MapRoute(
               name: "Feedback",
               url: "lien-he",
               defaults: new { controller = "Feedback", action = "Index", id = UrlParameter.Optional } ,
               namespaces: new[] { "FashionShop.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } ,
                namespaces: new[] { "FashionShop.Controllers"}
            );
        }
    }
}
