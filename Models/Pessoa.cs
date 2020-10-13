using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pessoas.Models
{
    [Table("Pessoa")]
    public class Pessoa
    {

        [Display(Description = "Id")]
        public int Id { get; set; }

        [Display(Description = "Nome completo")]
        public string Nome { get; set; }

        [Display(Description = "Sexo")]
        public int Sexo { get; set; }

        public virtual List<Endereco>  Endereco { get; set; }
    }
}
