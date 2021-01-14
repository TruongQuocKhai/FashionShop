using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    [Table("user_group")]
    public partial class user_group
    {
        [Key]
        [StringLength(50)]
        public string user_group_id { get; set; }
        [StringLength(50)]
        public string name { get; set; }
    }
}
