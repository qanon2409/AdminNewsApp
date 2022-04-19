#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminNewsApp.Models;

namespace AdminNewsApp.Controllers
{
    public class NoticiasController : Controller
    {
        private readonly PROYECTO2TIContext _context;

        public NoticiasController(PROYECTO2TIContext context)
        {
            _context = context;
        }

        // GET: Noticias
        public async Task<IActionResult> Index()
        {
            var pROYECTO2TIContext = _context.Noticias.Include(n => n.IdcatNavigation).Include(n => n.IdfuenteNavigation).Include(n => n.IdpaisNavigation);
            return View(await pROYECTO2TIContext.ToListAsync());
        }

        // GET: Noticias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = await _context.Noticias
                .Include(n => n.IdcatNavigation)
                .Include(n => n.IdfuenteNavigation)
                .Include(n => n.IdpaisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticia == null)
            {
                return NotFound();
            }

            return View(noticia);
        }

        // GET: Noticias/Create
        public IActionResult Create()
        {
            ViewData["Idcat"] = new SelectList(_context.Categoria, "Id", "Nombre");
            ViewData["Idfuente"] = new SelectList(_context.Fuentes, "Id", "Nombre");
            ViewData["Idpais"] = new SelectList(_context.Pais, "Id", "Nombre");
            return View();
        }

        // POST: Noticias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,Urlnoticia,Idcat,Idpais,Idfuente")] Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noticia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcat"] = new SelectList(_context.Categoria, "Id", "Nombre", noticia.Idcat);
            ViewData["Idfuente"] = new SelectList(_context.Fuentes, "Id", "Nombre", noticia.Idfuente);
            ViewData["Idpais"] = new SelectList(_context.Pais, "Id", "Nombre", noticia.Idpais);
            return View(noticia);
        }

        // GET: Noticias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = await _context.Noticias.FindAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }
            ViewData["Idcat"] = new SelectList(_context.Categoria, "Id", "Nombre", noticia.Idcat);
            ViewData["Idfuente"] = new SelectList(_context.Fuentes, "Id", "Nombre", noticia.Idfuente);
            ViewData["Idpais"] = new SelectList(_context.Pais, "Id", "Nombre", noticia.Idpais);
            return View(noticia);
        }

        // POST: Noticias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Noticia noticia)
        {
            if (id != noticia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noticia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticiaExists(noticia.Id))
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

            ViewData["Idcat"] = new SelectList(_context.Categoria, "Id", "Nombre", noticia.Idcat);
            ViewData["Idfuente"] = new SelectList(_context.Fuentes, "Id", "Nombre", noticia.Idfuente);
            ViewData["Idpais"] = new SelectList(_context.Pais, "Id", "Nombre", noticia.Idpais);
            return View(noticia);
        }

        // GET: Noticias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = await _context.Noticias
                .Include(n => n.IdcatNavigation)
                .Include(n => n.IdfuenteNavigation)
                .Include(n => n.IdpaisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticia == null)
            {
                return NotFound();
            }

            return View(noticia);
        }

        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);
            _context.Noticias.Remove(noticia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticiaExists(int id)
        {
            return _context.Noticias.Any(e => e.Id == id);
        }
        
       
    }
}
