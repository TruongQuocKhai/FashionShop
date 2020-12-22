namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("contact")]
    public partial class contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int contact_id { get; set; }

        [Column(TypeName = "ntext")]
        public string content { get; set; }

        public bool? status { get; set; }
    }
}
