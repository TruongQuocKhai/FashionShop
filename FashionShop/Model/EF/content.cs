namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("content")]
    public partial class content
    {
        [Key]
        public int content_id { get; set; }

        [StringLength(250)]
        public string content_name { get; set; }

        [StringLength(250)]
        public string alias { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [StringLength(250)]
        public string image { get; set; }

        [Column(TypeName = "ntext")]
        public string detail { get; set; }

        public DateTime? created_date { get; set; }

        [StringLength(250)]
        public string created_by { get; set; }

        public DateTime? edited_date { get; set; }

        [StringLength(250)]
        public string edited_by { get; set; }

        public bool? status { get; set; }

        public int? news_category_id { get; set; }

        [StringLength(500)]
        public string tag_id { get; set; }
    }
}
