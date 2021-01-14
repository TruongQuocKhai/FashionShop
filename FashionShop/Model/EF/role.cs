using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    [Table("role")]
    public partial class role
    {
        [Key]
        [StringLength(50)]
        public string role_id { get; set; }
        [StringLength(50)]
        public string name { get; set; }
    }
}
