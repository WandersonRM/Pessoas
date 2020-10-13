using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pessoas.Models
{
    [Table("ImagensProduto")]
    public class ImagemProduto
    {
        [Display(Name = "Código")]
        [Column("ImagemProdutoId")]
        [Key]
        public int ImagemProdutoId { get; set; }

        [Display(Name = "ProdutoId")]
        [ForeignKey("Produtos")]
        [Column(Order = 1)]
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }

        [Display(Name = "Caminho Imagem")]
        public string ImagemUrl { get; set; }
    }
}
