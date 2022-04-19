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
    public class FuentesController : Controller
    {
        private readonly PROYECTO2TIContext _context;

        public FuentesController(PROYECTO2TIContext context)
        {
            _context = context;
        }

        // GET: Fuentes
        public async Task<IActionResult> Index()
        {
            var pROYECTO2TIContext = _context.Fuentes.Include(f => f.IdpaisNavigation);
            return View(await pROYECTO2TIContext.ToListAsync());
        }

        // GET: Fuentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuente = await _context.Fuentes
                .Include(f => f.IdpaisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fuente == null)
            {
                return NotFound();
            }

            return View(fuente);
        }

        // GET: Fuentes/Create
        public IActionResult Create()
        {
            ViewData["Idpais"] = new SelectList(_context.Pais, "Id", "Id");
            return View();
        }

        // POST: Fuentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Idpais")] Fuente fuente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fuente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idpais"] = new SelectList(_context.Pais, "Id", "Id", fuente.Idpais);
            return View(fuente);
        }

        // GET: Fuentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuente = await _context.Fuentes.FindAsync(id);
            if (fuente == null)
            {
                return NotFound();
            }
            ViewData["Idpais"] = new SelectList(_context.Pais, "Id", "Id", fuente.Idpais);
            return View(fuente);
        }

        // POST: Fuentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Idpais")] Fuente fuente)
        {
            if (id != fuente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fuente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuenteExists(fuente.Id))
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
            ViewData["Idpais"] = new SelectList(_context.Pais, "Id", "Id", fuente.Idpais);
            return View(fuente);
        }

        // GET: Fuentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuente = await _context.Fuentes
                .Include(f => f.IdpaisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fuente == null)
            {
                return NotFound();
            }

            return View(fuente);
        }

        // POST: Fuentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fuente = await _context.Fuentes.FindAsync(id);
            _context.Fuentes.Remove(fuente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuenteExists(int id)
        {
            return _context.Fuentes.Any(e => e.Id == id);
        }
    }
}
