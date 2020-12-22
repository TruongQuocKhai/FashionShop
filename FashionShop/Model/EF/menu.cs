namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("menu")]
    public partial class menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int menu_id { get; set; }

        [StringLength(50)]
        public string text { get; set; }

        [StringLength(250)]
        public string link { get; set; }

        public int? display_order { get; set; }

        public bool? status { get; set; }

        public int? menu_type_id { get; set; }
    }
}
