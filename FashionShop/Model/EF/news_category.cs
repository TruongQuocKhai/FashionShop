namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class news_category
    {
        [Key]
        public int news_category_id { get; set; }

        [StringLength(250)]
        public string news_category_name { get; set; }

        [StringLength(250)]
        public string alias { get; set; }

        public int? parent_id { get; set; }

        public int? display_order { get; set; }

        public DateTime? created_date { get; set; }

        [StringLength(250)]
        public string created_by { get; set; }

        public DateTime? edited_date { get; set; }

        [StringLength(250)]
        public string edited_by { get; set; }

        public bool? status { get; set; }
    }
}
