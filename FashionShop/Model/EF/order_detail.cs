namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("order_detail")]
    public partial class order_detail
    {
        [Key]
        [Column(Order = 0)]
        public int order_id { get; set; }
        [Key]
        [Column(Order = 1)]
        public int product_id { get; set; }
        public int quantity { get; set; }
        public decimal? price { get; set; }
    }
}
