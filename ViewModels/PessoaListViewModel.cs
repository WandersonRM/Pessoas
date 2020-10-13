using Pessoas.Data;
using Pessoas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pessoas.ViewModels
{
    public class PessoaListViewModel
    {
        public class PessoaEnderecos
        {
            public Pessoa Pessoa { get; set; }
            public IList<Endereco> Enderecos { get; set; }
        }

        public class PessoaPedidos
        {
            public Pessoa Pessoa { get; set; }
            public IList<Pedido> Pedidos { get; set; }
        }
    
    } 
}
