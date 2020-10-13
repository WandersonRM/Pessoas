using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pessoas.Models
{
    [Table("ItensPedido")]
    public class ItenPedido
    {
        [Display(Name = "Código")]
        [Column("EnderecoId")]
        [Key]
        public int ItenPedidoId { get; set; }

        [Display(Name = "ProdutoId")]//id do Produto
        [ForeignKey("Produtos")]
        [Column(Order = 1)]
        public int ProdutoId { get; set; }
       

        [Display(Description = "Descrição do Produto")]//nome do produto
        public string Descricao { get; set; }

        [Display(Description = "Quantidade")]
        public int Quantidade { get; set; }

        [Display(Description = "Valor Unitário")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorUnitario { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
