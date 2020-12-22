namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class menu_type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int menu_type_id { get; set; }

        [StringLength(50)]
        public string name { get; set; }
    }
}
