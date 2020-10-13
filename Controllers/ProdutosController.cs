using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pessoas.Data;
using System.IO;
using Pessoas.Models;
using static Pessoas.ViewModels.ImagensProdutoViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Grpc.Core;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

namespace Pessoas.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ImagemProduto> _contextImagem;
        private IHostEnvironment _iHostingEnviroment;



        public ProdutosController(ApplicationDbContext context, IHostEnvironment iHostingEnviroment)
        {
            _context = context;

            _iHostingEnviroment = iHostingEnviroment;
        }

        [Route("Produtos/Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produtos.ToListAsync());
        }


        [HttpGet("Produtos/{id}/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);


            List<ImagemProduto> listaImagensProdutoEspecifico = new List<ImagemProduto>();

            var listaImagensProduto = _context.ImagensProduto
               .Include(e => e.Produto)
               .Where(e => e.Produto.ProdutoId == produto.ProdutoId)
               .ToList();

            /*Primeiro eu acesso o contexto isto é a tabela do BD, depois eu acesso a relação que eu mantenho com outra tabela e dps faço a clausula de excessão*/

            foreach (var item in listaImagensProduto)
            {
                ImagemProduto imagemProduto = new ImagemProduto(); //ViewModel

                imagemProduto.ImagemProdutoId = item.ImagemProdutoId;
                imagemProduto.ProdutoId = item.ProdutoId;
                imagemProduto.ImagemUrl = item.ImagemUrl;
                listaImagensProdutoEspecifico.Add(imagemProduto);
            }

            ListaImagensProduto exibirListaImagensProduto = new ListaImagensProduto();
            exibirListaImagensProduto.ImagensProduto = (IList<ImagemProduto>)listaImagensProdutoEspecifico;
            exibirListaImagensProduto.Produto = produto;


            return View(exibirListaImagensProduto);
        }

        [HttpGet("Produtos/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [ValidateAntiForgeryToken]
        [HttpPost("Produtos/Create")]
        public async Task<IActionResult> Create([Bind("ProdutoId,CodigoBarra,nome,DescricaoDetalhada,Estoque,Preco")] Produto produto, IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();

                var imagePath = @"C:\C#\Pessoas\wwwroot\Upload\Images\";
                string local = @"https://localhost:5001/";

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                foreach (IFormFile file in files)
                {
                    string uniqueFilename = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(imagePath, uniqueFilename);
                    file.CopyTo(new FileStream(filePath, FileMode.Create));
                    string caminho = local + @"Upload/Images/" + uniqueFilename;

                    ImagemProduto imagemProduto = new ImagemProduto()
                    {
                        ImagemUrl = caminho,
                        Produto = produto
                    };

                    _context.ImagensProduto.Add(imagemProduto);

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        [HttpGet("Produtos/{produtoId}/Edit")]
        public async Task<IActionResult> Edit(int? produtoId)
        {
            if (produtoId == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(produtoId);


            if (produto == null)
            {
                return NotFound();
            }

            List<ImagemProduto> listasImagens = new List<ImagemProduto>();

            var listaImagensProduto = _context.ImagensProduto.Where(p => p.ProdutoId == produtoId).ToList();

            foreach (var item in listaImagensProduto)
            {
                ImagemProduto imagemProduto = new ImagemProduto();
                imagemProduto.ImagemProdutoId = item.ImagemProdutoId;
                imagemProduto.ImagemUrl = item.ImagemUrl;
                imagemProduto.ImagemUrl = item.ImagemUrl;
                listasImagens.Add(imagemProduto);
            }

            ListaImagensProduto exibirProdutoEImagens = new ListaImagensProduto();
            exibirProdutoEImagens.ImagensProduto = (IList<ImagemProduto>)listasImagens;
            exibirProdutoEImagens.Produto = produto;

            return View(exibirProdutoEImagens);
            //return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        [HttpPost("Produtos/{id}/Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoId,CodigoBarra,nome,DescricaoDetalhada,Estoque,Preco")] Produto produto, IFormFile[] files)
        {
            if (id != produto.ProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();

                    var imagePath = @"C:\C#\Pessoas\wwwroot\Upload\Images\";
                    string local = @"https://localhost:5001/";
                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    foreach (IFormFile file in files)
                    {
                        string uniqueFilename = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string filePath = Path.Combine(imagePath, uniqueFilename);
                        file.CopyTo(new FileStream(filePath, FileMode.Create));
                        string caminho = local + @"Upload/Images/" + uniqueFilename;

                        ImagemProduto imagemProduto = new ImagemProduto()
                        {
                            ImagemUrl = caminho,
                            Produto = produto
                        };

                        _context.ImagensProduto.Add(imagemProduto);

                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }


                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.ProdutoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        [HttpGet("Produtos/{id}/Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Deletarproduto")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int produtoid)
        {
            var produto = await _context.Produtos.FindAsync(produtoid);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.ProdutoId == id);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(int produtoId, IFormFile[] files)
        {
            var imagePath = @"C:\C#\Pessoas\wwwroot\Upload\Images\";

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == produtoId);
            if (produto == null)
            {
                return NotFound();
            }
            else
            {

                foreach (IFormFile file in files)
                {
                    string uniqueFilename = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(imagePath, uniqueFilename);
                    file.CopyTo(new FileStream(filePath, FileMode.Create));
                    string caminho = @"Upload\Images\" + uniqueFilename;

                    ImagemProduto imagemProduto = new ImagemProduto()
                    {
                        ImagemUrl = caminho,
                        Produto = produto
                    };

                    _context.ImagensProduto.Add(imagemProduto);

                }
                await _context.SaveChangesAsync();

            }

            return RedirectToAction("Index", new { Message = "Sucesso ao salvar  arquivo" });

        }

        [Route("Produtos/{id}/Images")]
        public async Task<IActionResult> AdicionarImagens(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);


            List<ImagemProduto> listaImagensProdutoEspecifico = new List<ImagemProduto>();

            var listaImagensProduto = _context.ImagensProduto
               .Include(e => e.Produto)
               .Where(e => e.Produto.ProdutoId == produto.ProdutoId)
               .ToList();

            /*Primeiro eu acesso o contexto isto é a tabela do BD, depois eu acesso a relação que eu mantenho com outra tabela e dps faço a clausula de excessão*/

            foreach (var item in listaImagensProduto)
            {
                ImagemProduto imagemProduto = new ImagemProduto(); //ViewModel

                imagemProduto.ImagemProdutoId = item.ImagemProdutoId;
                imagemProduto.ProdutoId = item.ProdutoId;
                imagemProduto.ImagemUrl = item.ImagemUrl;
                listaImagensProdutoEspecifico.Add(imagemProduto);
            }

            ListaImagensProduto exibirListaImagensProduto = new ListaImagensProduto();
            exibirListaImagensProduto.ImagensProduto = (IList<ImagemProduto>)listaImagensProdutoEspecifico;
            exibirListaImagensProduto.Produto = produto;


            return View(exibirListaImagensProduto);
        }

        [Route("Produtos/{produtoId}/ListagemImagens")]
        public async Task<IActionResult> ListagemImagens(int produtoId)
        {
            if (produtoId == null)
            {
                return NotFound();
            }
            string local = @"https://localhost:5001/";

            var attachmentsList = new List<AttachmentsModel>();

            var listaImagensProduto = _context.ImagensProduto.Where(p => p.ProdutoId == produtoId).ToList();

            foreach (var item in listaImagensProduto)
            {
                AttachmentsModel imagemProduto = new AttachmentsModel();

                imagemProduto.AttachmentID = item.ImagemProdutoId;
                imagemProduto.FileName = item.ImagemUrl;
                imagemProduto.Path = item.ImagemUrl;
                attachmentsList.Add(imagemProduto);
            }

            
            return Json(new { Data = attachmentsList });
            //return (IActionResult)listaImagensProdutoEspecifico;
        }

        [HttpDelete("Produtos/{imagemId}/DeletarImagem")]
        public async Task<IActionResult> DeletarImagem(int imagemId)
        {
            
            var imagemProduto = await _context.ImagensProduto.FindAsync(imagemId);
            _context.ImagensProduto.Remove(imagemProduto);
            await _context.SaveChangesAsync();

            return Json(new { Data = "deletado com sucesso" });
        }
    }

}

