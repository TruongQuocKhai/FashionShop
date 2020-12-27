using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EF;

namespace FashionShop.Models
{
    public class CartModel
    {
        // có thể nối tiếp
        public product Product { get; set; }
        public int Quantity { get; set; }
    }
}