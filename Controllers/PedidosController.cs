using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pessoas.Data;
using Pessoas.Models;
using static Pessoas.ViewModels.ItensPedidoViewModel;
using static Pessoas.ViewModels.ItensPedidoViewModel.ItensPedido;

namespace Pessoas.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pedidos.Include(p => p.Pessoa);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pedidoes/Details/5
        [Route("Pedidos/{id?}/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Pessoa)
                .FirstOrDefaultAsync(m => m.PedidoId == id);

            List<ItenPedido> listaItensPedido = new List<ItenPedido>();

            var listaItensPedidoPessoa = _context.itensPedido
               .Include(e => e.Pedido)
               .Where(e => e.Pedido.PedidoId == pedido.PedidoId)
               .ToList();

            /*Primeiro eu acesso o contexto isto é a tabela do BD, depois eu acesso a relação que eu mantenho com outra tabela e dps faço a clausula de excessão*/

            foreach (var item in listaItensPedidoPessoa)
            {
                ItenPedido itensPedido = new ItenPedido(); //ViewModel

                itensPedido.ItenPedidoId = item.ItenPedidoId;
                itensPedido.ProdutoId = item.ProdutoId;
                itensPedido.Descricao = item.Descricao;
                itensPedido.Quantidade = item.Quantidade;
                itensPedido.ValorUnitario = item.ValorUnitario;
                itensPedido.ValorTotal = item.ValorTotal;
                listaItensPedido.Add(itensPedido);
            }

            Adicionaritens exibir = new Adicionaritens();
            exibir.ItensdoPedido = (IList<ItenPedido>)listaItensPedido;
            exibir.Pedido = pedido;


            return View(exibir);

            //return View(pedido);
        }

        // GET: Pedidoes/Create
        public IActionResult Create()
        {
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "Id", "Nome");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoId,PessoaId,Rua,Numero,Complemento,Bairro,Cidade,Estado,Status,PedidoEnviado")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "Id", "Nome", pedido.PessoaId);
            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        [Route("Pedidos/{id?}/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "Id", "Nome", pedido.PessoaId);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoId,PessoaId,Rua,Numero,Complemento,Bairro,Cidade,Estado,Status,PedidoEnviado")] Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
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
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "Id", "Nome", pedido.PessoaId);
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        [Route("Pedidos/{id?}/Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Pessoa)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.PedidoId == id);
        }


    }
}
