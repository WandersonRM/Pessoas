using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pessoas.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Display(Name = "Código")]
        [Column("PedidoId")]// id do pedido 
        [Key]
        public int PedidoId { get; set; }
        public virtual List<ItenPedido> PedidoItens { get; set; }//itens do pedido[lista de produtos]

        [Display(Name = "UsuarioId")]//id do cliente
        [ForeignKey("Pessoa")]
        [Column(Order = 1)]
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        //Endereço do pedido
        [Display(Description = "Rua")]
        public string Rua { get; set; }

        [Display(Description = "Número")]
        public int Numero { get; set; }

        [Display(Description = "Complemento")]
        public string Complemento { get; set; }

        [Display(Description = "Bairro")]
        public string Bairro { get; set; }

        [Display(Description = "Cidade")]
        public string Cidade { get; set; }

        [Display(Description = "Estado")]
        public string Estado { get; set; }

        //status pedido
        [Display(Name = "Status")]
        public int Status { get; set; }

        [Display(Name = "Data/Hora")] //data que foi feita
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime PedidoEnviado { get; set; }


    }
}
