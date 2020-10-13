using Pessoas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pessoas.ViewModels
{
    public class ItensPedidoViewModel
    {
        public class ItensPedido
        {
            public class Adicionaritens
            {
                public Pedido Pedido { get; set; }
                public IList<ItenPedido> ItensdoPedido { get; set; }
            }
        }
    }
}
