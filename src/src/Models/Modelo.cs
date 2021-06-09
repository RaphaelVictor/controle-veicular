using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Modelo
    {
        public int modeloId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Modelo Descrição")]
        public string modeloDesc { get; set; }

        [Required]
        [Display(Name = "Marca")]
        public virtual int marcaid { get; set; }

        [ForeignKey("marcaid")]
        public Marca Marca { get; set; }
    }
}
