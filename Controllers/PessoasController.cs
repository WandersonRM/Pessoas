using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pessoas.Data;
using Pessoas.Models;
using Pessoas.ViewModels;
using static Pessoas.ViewModels.PessoaListViewModel;

namespace Pessoas.Controllers
{
    public class PessoasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PessoasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            CarregaTipoSexo();
            return View(await _context.Pessoas.ToListAsync());
        }

        // GET: Pessoas/Details/5
        [Route("Pessoas/{id?}/Enderecos/Index")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(m => m.Id == id);

            List<Endereco> listaEnderecos = new List<Endereco>();

            var listaEnderecosPessoa = _context.Enderecos
               .Include(e => e.Pessoa)
               .Where(e => e.Pessoa.Id == pessoa.Id)
               .ToList();

            /*Primeiro eu acesso o contexto isto é a tabela do BD, depois eu acesso a relação que eu mantenho com outra tabela e dps faço a clausula de excessão*/

            foreach (var item in listaEnderecosPessoa)
            {
                Endereco enderecosPessoa = new Endereco(); //ViewModel
                enderecosPessoa.EnderecoId = item.EnderecoId;
                enderecosPessoa.PessoaId = item.PessoaId;
                enderecosPessoa.Rua = item.Rua;
                enderecosPessoa.Numero = item.Numero;
                enderecosPessoa.Complemento = item.Complemento;
                enderecosPessoa.Bairro = item.Bairro;
                enderecosPessoa.Cidade = item.Cidade;
                enderecosPessoa.Estado = item.Estado;
                listaEnderecos.Add(enderecosPessoa);
            }

            PessoaEnderecos exibir = new PessoaEnderecos();
            exibir.Enderecos = (IList<Endereco>)listaEnderecos;
            exibir.Pessoa = pessoa;

            /*{
                Pessoa = pessoa,
                Enderecos = (IList<Enderecos>)listaEnderecos

            };*/

            CarregaTipoSexo();
            return View(exibir);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            CarregaTipoSexo();
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sexo")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CarregaTipoSexo();
            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        [HttpGet("Pessoas/{id?}/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            CarregaTipoSexo();
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [ValidateAntiForgeryToken]
        [HttpPost("Pessoas/{id?}/Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sexo")] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
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
            CarregaTipoSexo();
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        [Route("Pessoas/{id?}/Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }
            CarregaTipoSexo();
            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }

        public void CarregaTipoSexo()
        {
            var itensTipoSexo = new List<SelectListItem>
            {
                new SelectListItem{ Value = "1", Text = "Masculino"},
                new SelectListItem{ Value = "2", Text = "Feminino"},
                new SelectListItem{ Value = "3", Text = "Outros"}
            };

            ViewBag.TipoSexo = itensTipoSexo;
        }

        [Route("Pessoas/{id?}/Pedidos/Index")]
        public async Task<IActionResult> Pedidos(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(m => m.Id == id);

            List<Pedido> listaPedidos = new List<Pedido>();

            var listaPedidosPessoa = _context.Pedidos
               .Where(p => p.PessoaId == pessoa.Id)
               .ToList();

            foreach (var item in listaPedidosPessoa)
            {
                Pedido pedidosPessoa = new Pedido(); //ViewModel
                pedidosPessoa.PedidoId = item.PedidoId;
                pedidosPessoa.PessoaId = item.PessoaId;
                pedidosPessoa.Rua = item.Rua;
                pedidosPessoa.Numero = item.Numero;
                pedidosPessoa.Complemento = item.Complemento;
                pedidosPessoa.Bairro = item.Bairro;
                pedidosPessoa.Cidade = item.Cidade;
                pedidosPessoa.Estado = item.Estado;
                pedidosPessoa.Status = item.Status;
                pedidosPessoa.PedidoEnviado = item.PedidoEnviado;
                listaPedidos.Add(pedidosPessoa);
            }

            PessoaPedidos exibir = new PessoaPedidos();
            exibir.Pedidos = (IList<Pedido>)listaPedidos;
            exibir.Pessoa = pessoa;

            CarregaTipoSexo();
            return View(exibir);

        }

        public void CarregaTipoPedido()
        {
            var itensTipoStatusPedido = new List<SelectListItem>
            {
                new SelectListItem{ Value = "1", Text = "Feito"},
                new SelectListItem{ Value = "2", Text = "Enviado"},
                new SelectListItem{ Value = "3", Text = "Entregue"}
            };

            ViewBag.StatusPedido = itensTipoStatusPedido;
        }

        [Route("Pessoas/{pessoaId}/Pedidos/{pedidoId}/Edit")]
        public async Task<IActionResult> EditarPedido(int pessoaId, int pedidoId)
        {
            if (pedidoId == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(pedidoId);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);

        }

        // GET: Pedidoes/Details/5
        [Route("Pessoas/{pessoaId}/Pedidos/Details/{pedidoId}")]
        public async Task<IActionResult> DetalhesPedido(int pessoaId, int pedidoId)
        {
            if (pedidoId == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Pessoa)
                .FirstOrDefaultAsync(m => m.PedidoId == pedidoId);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }


        // GET: Pedidos/Create
        [Route("Pessoas/{pessoaId}/Pedidos/Create/")]
        public async Task<IActionResult> CriarPedido(int pessoaId)
        {
            if (pessoaId == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(m => m.Id == pessoaId);

            ViewData["PessoaId"] = pessoa.Id;
            return View();
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Pessoas/Pedidos/Create/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarPedido([Bind("PedidoId,PessoaId,Rua,Numero,Complemento,Bairro,Cidade,Estado,Status,PedidoEnviado")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PessoaId"] = pedido.PessoaId;
            return View(pedido);
        }



        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Pessoas/Pedidos/{id}/Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarEdicaoPedido(int id, [Bind("PedidoId,PessoaId,Rua,Numero,Complemento,Bairro,Cidade,Estado,Status,PedidoEnviado")] Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Pedidos.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.PedidoId))
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
            ViewData["PessoaId"] = pedido.PessoaId;
            return View(pedido);
        }

        private bool PedidoExists(int pedidoId)
        {
            throw new NotImplementedException();
        }

        // GET: Pedidoes/Delete/5
        [Route("Pessoas/{pessoaId}/Pedidos/{pedidoId}/Delete")]
        public async Task<IActionResult> DeletarPedido([FromRoute]int pessoaId, int pedidoId)
        {

            if (pedidoId == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Pessoa)
                .FirstOrDefaultAsync(m => m.PedidoId == pedidoId);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        //[Route("Pessoas/Pedidos/Delete/{id?}")]
        [HttpPost, ActionName("DeletarPedido")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarDeletacao(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }






    }
}
