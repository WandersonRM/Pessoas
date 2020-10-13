using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pessoas.Models;

namespace Pessoas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<TipoUsuario> TipoUsuario { get; set; }
 
        public DbSet<PerfilUsuario> PerfilUsuario { get; set; }

        public DbSet<IdentityUser> Usuario { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<ItenPedido> itensPedido { get; set; }

        public DbSet<ImagemProduto> ImagensProduto { get; set; }

       
    }
}
