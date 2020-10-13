using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pessoas.Data;
using Pessoas.Models;

namespace Pessoas.Controllers
{
    public class ItenPedidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItenPedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItenPedidos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.itensPedido.Include(i => i.Produto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ItenPedidos/Details/5
        [Route("ItemPedido/{id?}/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itenPedido = await _context.itensPedido
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.ItenPedidoId == id);
            if (itenPedido == null)
            {
                return NotFound();
            }

            return View(itenPedido);
        }

        // GET: ItenPedidos/Create
        public IActionResult Create()
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId");
            return View();
        }

        // POST: ItenPedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItenPedidoId,ProdutoId,Descricao,Quantidade,ValorUnitario,ValorTotal")] ItenPedido itenPedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itenPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId", itenPedido.ProdutoId);
            return View(itenPedido);
        }

        // GET: ItenPedidos/Edit/5
        [Route("ItemPedido/{id?}/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itenPedido = await _context.itensPedido.FindAsync(id);
            if (itenPedido == null)
            {
                return NotFound();
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId", itenPedido.ProdutoId);
            return View(itenPedido);
        }

        // POST: ItenPedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItenPedidoId,ProdutoId,Descricao,Quantidade,ValorUnitario,ValorTotal")] ItenPedido itenPedido)
        {
            if (id != itenPedido.ItenPedidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itenPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItenPedidoExists(itenPedido.ItenPedidoId))
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
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId", itenPedido.ProdutoId);
            return View(itenPedido);
        }

        // GET: ItenPedidos/Delete/5
        [Route("ItemPedido/{id?}/Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itenPedido = await _context.itensPedido
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.ItenPedidoId == id);
            if (itenPedido == null)
            {
                return NotFound();
            }

            return View(itenPedido);
        }

        // POST: ItenPedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itenPedido = await _context.itensPedido.FindAsync(id);
            _context.itensPedido.Remove(itenPedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItenPedidoExists(int id)
        {
            return _context.itensPedido.Any(e => e.ItenPedidoId == id);
        }
    }
}
