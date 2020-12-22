namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("slide")]
    public partial class slide
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int slide_id { get; set; }

        [StringLength(250)]
        public string image { get; set; }

        public int? display_order { get; set; }

        [StringLength(250)]
        public string link { get; set; }

        [StringLength(100)]
        public string description { get; set; }

        public DateTime? created_date { get; set; }

        [StringLength(250)]
        public string created_by { get; set; }

        public DateTime? edited_date { get; set; }

        [StringLength(250)]
        public string edited_by { get; set; }

        public bool? status { get; set; }
    }
}
