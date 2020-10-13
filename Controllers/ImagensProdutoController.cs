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
    public class ImagensProdutoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImagensProdutoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Thumbnail(string imageName)
        {
            // TODO: your authorization stuff comes here
            // Verify that the currently authenticated user has the required
            // permissions to access the requested image
            // It is also very important to properly sanitize the imageName
            // parameter to avoid requests such as imageName=../../../super_secret.png
            //var imagePath = @"C:\C#\Pessoas\wwwroot\Upload\Images\";
            var path = @"C:\C#\Pessoas\wwwroot\Upload\Images\" + imageName;

            return base.File(path, "image/png");
        }
        // GET: ImagensProduto
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ImagensProduto.Include(i => i.Produto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ImagensProduto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagemProduto = await _context.ImagensProduto
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.ImagemProdutoId == id);
            if (imagemProduto == null)
            {
                return NotFound();
            }

            return View(imagemProduto);
        }

        // GET: ImagensProduto/Create
        public IActionResult Create()
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImagemProdutoId,ProdutoId,ImagemUrl")] ImagemProduto imagemProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imagemProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId", imagemProduto.ProdutoId);
            return View(imagemProduto);
        }

        // GET: ImagensProduto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagemProduto = await _context.ImagensProduto.FindAsync(id);
            if (imagemProduto == null)
            {
                return NotFound();
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId", imagemProduto.ProdutoId);
            return View(imagemProduto);
        }

        // POST: ImagensProduto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImagemProdutoId,ProdutoId,ImagemUrl")] ImagemProduto imagemProduto)
        {
            if (id != imagemProduto.ImagemProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imagemProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagemProdutoExists(imagemProduto.ImagemProdutoId))
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
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId", imagemProduto.ProdutoId);
            return View(imagemProduto);
        }

        // GET: ImagensProduto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagemProduto = await _context.ImagensProduto
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.ImagemProdutoId == id);
            if (imagemProduto == null)
            {
                return NotFound();
            }

            return View(imagemProduto);
        }

        // POST: ImagensProduto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imagemProduto = await _context.ImagensProduto.FindAsync(id);
            _context.ImagensProduto.Remove(imagemProduto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagemProdutoExists(int id)
        {
            return _context.ImagensProduto.Any(e => e.ImagemProdutoId == id);
        }
    }
}
