namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feedback")]
    public partial class feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int feedback_id { get; set; }

        [StringLength(50)]
        public string feedback_name { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(250)]
        public string subject { get; set; }

        [Column(TypeName = "ntext")]
        public string message { get; set; }
    }
}
