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
    public class EnderecosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnderecosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Enderecos
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.Enderecos.Include(e => e.Pessoa);
            //return View(await applicationDbContext.ToListAsync());
            
                return RedirectToAction("index", "Pessoas");
          
        }
        

        // GET: Enderecos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enderecos = await _context.Enderecos
                .Include(e => e.Pessoa)
                .FirstOrDefaultAsync(m => m.EnderecoId == id);
            if (enderecos == null)
            {
                return NotFound();
            }

            return View(enderecos);
        }

        // GET: Enderecos/Create
        public async Task<IActionResult> Create(int? id)
        {

            if ((id == null)|| (id == 0))
            {
                ViewData["PessoaId"] = new SelectList(_context.Pessoas, "Id", "Nome");
                return View();
            }
            else
            {
                var pessoa = await _context.Pessoas.FindAsync(id);
                ViewData["PessoaId"] = pessoa.Id;
            }

            return View();
        }

        // POST: Enderecos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnderecoId,PessoaId,Rua,Numero,Complemento,Bairro,Cidade,Estado")] Endereco enderecos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enderecos);
                await _context.SaveChangesAsync();

                //return RedirectToAction(nameof(Index), enderecos.PessoaId);
                //return RedirectToRoute("enderecos.PessoaId", "Pessoas/Details/");

                return RedirectToRoute(new
                {
                    controller = "Pessoas",
                    action = "Details",
                    id = enderecos.PessoaId,
                    
                });
                //string routeName, object routeValues
                //route values
                //route name
            }
            ViewData["PessoaId"] = enderecos.PessoaId;
            return View(enderecos);
        }

        // GET: Enderecos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enderecos = await _context.Enderecos.FindAsync(id);
            if (enderecos == null)
            {
                return NotFound();
            }
            ViewData["PessoaId"] =  enderecos.PessoaId;
            return View(enderecos);
        }

        // POST: Enderecos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnderecoId,PessoaId,Rua,Numero,Complemento,Bairro,Cidade,Estado")] Endereco enderecos)
        {
            if (id != enderecos.EnderecoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enderecos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecosExists(enderecos.EnderecoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToRoute(new
                {
                    controller = "Pessoas",
                    action = "Details",
                    id = enderecos.PessoaId,

                });
                //return RedirectToAction(nameof(Index));
            }
            ViewData["PessoaId"] = enderecos.PessoaId;
            return View(enderecos);
        }

        // GET: Enderecos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enderecos = await _context.Enderecos
                .Include(e => e.Pessoa)
                .FirstOrDefaultAsync(m => m.EnderecoId == id);
            if (enderecos == null)
            {
                return NotFound();
            }

            return View(enderecos);
        }

        // POST: Enderecos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enderecos = await _context.Enderecos.FindAsync(id);

            var recebeID = enderecos.PessoaId;
            _context.Enderecos.Remove(enderecos);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new
            {
                controller = "Pessoas",
                action = "Details",
                id = recebeID,

            });
            //return RedirectToAction(nameof(Index));
        }

        private bool EnderecosExists(int id)
        {
            return _context.Enderecos.Any(e => e.EnderecoId == id);
        }
    }
}
