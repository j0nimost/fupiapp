using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fupi_KeyGenService.Models
{
    public class KeyGenModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        public string Proxy { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsUtilized { get; set; }
    }
}
