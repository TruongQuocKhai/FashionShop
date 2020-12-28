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
           name: "Order",
           url: "dat-hang",
           defaults: new { controller = "Cart", action = "_OrderPartial", id = UrlParameter.Optional }
       );

            routes.MapRoute(
           name: "Add the item to cart",
           url: "them-vao-gio-hang",
           defaults: new { controller = "Cart", action = "AddItems", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Cart",
              url: "gio-hang",
              defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
               name: "Product Detail",
               url: "chi-tiet/{alias}-{id}",
               defaults: new { controller = "Product", action = "ViewProductDetails", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Product Category",
               url: "san-pham/{alias}-{id}",
               defaults: new { controller = "Product", action = "ListProductsByCategory", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "All Products",
                url: "tat-ca-san-pham",
                defaults: new { controller = "Product", action = "ListAllProducts", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
