using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Marca
    {

        public int marcaId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Marca Descrição")]
        public string marcaDesc { get; set; }
    }
}
