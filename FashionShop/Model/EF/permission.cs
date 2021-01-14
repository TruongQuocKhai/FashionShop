using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    [Table("permission")]
    public partial class permission
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string user_group_id { get; set; }
        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string role_id { get; set; }
    }
}
