using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Anuncio
    {
        public int anuncioId { get; set; }
        [Required]
        [Display(Name = "Descrição do Anuncio")]
        public string anuncioDesc { get; set; }
        [Required]
        [Display(Name = "Modelo")]
        public int modeloId { get; set; }
        [ForeignKey("modeloId")]
        public Modelo Modelo { get; set; }
        [Required]
        [Display(Name = "Ano")]
        public int ano { get; set; }
        [Required]
        [Display(Name = "Valor de Compra")]
        public double valorCompra { get; set; }
        [Required]
        [Display(Name = "Valor de Venda")]
        public double valorVenda { get; set; }
        [Required]
        [Display(Name = "Cor")]
        public Cor cor { get; set; }
        [Required]
        [Display(Name = "Tipo de Combustivel")]
        public TipoCombusivel tipoCombustivel { get; set; }
        [Required]
        [Display(Name = "Data de Venda")]
        public DateTime dataVenda { get; set; }
    }

    public enum Cor
    {
        Branco = 1,
        Azul = 2,
        Vermelho = 3,
        Preto = 4,
        Cinza = 5
    }

    public enum TipoCombusivel : short
    {
       Gasolina = 1,
       Álcool = 2,
       Flex = 3,
       Diesel = 4
    }
}
