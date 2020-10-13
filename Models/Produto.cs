using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pessoas.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Display(Name = "Código")]
        [Column("ProdutoId")]
        [Key]
        public int ProdutoId { get; set; }

        [Display(Description = "Código de Barra")]
        public int CodigoBarra { get; set; }

        [Display(Description = "Nome do Produto")]
        public string nome { get; set; }

        [Display(Description = "Descrição Detalhada")]
        public string DescricaoDetalhada { get; set; }

        [Display(Description = "Estoque")]
        public int Estoque { get; set; }

        [Display(Description = "Preço")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }

    }
}
