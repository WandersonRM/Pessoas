using Pessoas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pessoas.ViewModels
{
    public class ImagensProdutoViewModel
    {
        public class ListaImagensProduto
        {
            public Produto Produto { get; set; }
            public IList<ImagemProduto> ImagensProduto { get; set; }
        }
    }
}
