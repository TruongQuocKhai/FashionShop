namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class product
    {
        [Key]
        public int product_id { get; set; }

        [StringLength(250)]
        public string product_name { get; set; }

        [StringLength(20)]
        public string prod_code { get; set; }

        [StringLength(250)]
        public string alias { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [StringLength(250)]
        public string image { get; set; }

        [Column(TypeName = "xml")]
        public string sub_image { get; set; }

        public decimal? price { get; set; }

        public decimal? discount { get; set; }

        public int quantity { get; set; }

        [Column(TypeName = "ntext")]
        public string detail { get; set; }

        public DateTime? created_date { get; set; }

        [StringLength(250)]
        public string created_by { get; set; }

        public DateTime? edited_date { get; set; }

        [StringLength(250)]
        public string edited_by { get; set; }

        public DateTime? top_hot { get; set; }

        public bool? status { get; set; }

        public int? prd_category_id { get; set; }
    }
}
