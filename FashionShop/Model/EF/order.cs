namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("order")]
    public partial class order
    {
        [Key]
        public int order_id { get; set; }
        public int user_id { get; set; }
        public string order_name { get; set; }
        public string order_phone { get; set; }
        public string order_email { get; set; }
        public string order_address { get; set; }
        public int order_province_id { get; set; }
        public int order_district_id { get; set; }
        public DateTime order_date { get; set; }
        public bool? status { get; set; }
    }
}
