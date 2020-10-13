using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pessoas.Models
{
    [Table("Enderecos")]
    public class Endereco
    {
        [Display(Name = "Código")]
        [Column("EnderecoId")]
        [Key]
        public int EnderecoId { get; set; }

        [Display(Name = "UsuarioId")]
        [ForeignKey("Pessoa")]
        [Column(Order = 1)]
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

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
    }
}
