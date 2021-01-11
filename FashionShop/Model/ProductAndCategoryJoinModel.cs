using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models
{
    public class ProductAndCategoryJoinModel
    {

        public int ProdId { set; get; }
        public string ProdImages { set; get; }
        public string ProdName { set; get; }
        public decimal? ProdPrice { set; get; }
        public string CateName { set; get; }
        public string CateAlias { set; get; }
        public string ProdAlias { set; get; }
        public bool? ProdStatus { set; get; }
        public DateTime? CreatedDate { set; get; }
    }
}