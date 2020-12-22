namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user")]
    public partial class user
    {
        [Key]
        public int user_id { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(200)]
        public string display_name { get; set; }

        [StringLength(32)]
        public string password { get; set; }

        [StringLength(200)]
        public string address { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        public DateTime? created_date { get; set; }

        [StringLength(250)]
        public string created_by { get; set; }

        public DateTime? edited_date { get; set; }

        [StringLength(250)]
        public string edited_by { get; set; }

        public bool? status { get; set; }
    }
}
