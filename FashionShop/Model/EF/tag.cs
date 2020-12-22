namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tag")]
    public partial class tag
    {
        [Key]
        [StringLength(50)]
        public string tag_id { get; set; }

        [StringLength(50)]
        public string tag_name { get; set; }
    }
}
