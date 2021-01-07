using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models
{
    public class DistrictModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int ProvinceId { get; set; }
    }
}