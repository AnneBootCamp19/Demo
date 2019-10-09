using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Puutarha.Models;

namespace Puutarha.Controllers
{
    public class SatotiedotController : Controller
    {
        private readonly PuutarhaContext _context;

        public SatotiedotController(PuutarhaContext context)
        {
            _context = context;
        }

        // GET: Satotiedot
        public async Task<IActionResult> Index()
        {
            var puutarhaContext = _context.Satotiedot.Include(s => s.Kasvi);
            return View(await puutarhaContext.ToListAsync());
        }

        // GET: Satotiedot/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satotiedot = await _context.Satotiedot
                .Include(s => s.Kasvi)
                .FirstOrDefaultAsync(m => m.SatoId == id);
            if (satotiedot == null)
            {
                return NotFound();
            }

            return View(satotiedot);
        }

        // GET: Satotiedot/Create
        public IActionResult Create()
        {
            ViewData["KasviId"] = new SelectList(_context.Kasvit, "KasviId", "Nimi");
            return View();
        }

        // POST: Satotiedot/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SatoId,KasviId,SatoPvm,Lämpötila,Määrä,Yksikkö,Lisätieto")] Satotiedot satotiedot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(satotiedot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KasviId"] = new SelectList(_context.Kasvit, "KasviId", "Nimi", satotiedot.KasviId);
            return View(satotiedot);
        }

        // GET: Satotiedot/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satotiedot = await _context.Satotiedot.FindAsync(id);
            if (satotiedot == null)
            {
                return NotFound();
            }
            ViewData["KasviId"] = new SelectList(_context.Kasvit, "KasviId", "Nimi", satotiedot.KasviId);
            return View(satotiedot);
        }

        // POST: Satotiedot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SatoId,KasviId,SatoPvm,Lämpötila,Määrä,Yksikkö,Lisätieto")] Satotiedot satotiedot)
        {
            if (id != satotiedot.SatoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(satotiedot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SatotiedotExists(satotiedot.SatoId))
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
            ViewData["KasviId"] = new SelectList(_context.Kasvit, "KasviId", "Nimi", satotiedot.KasviId);
            return View(satotiedot);
        }

        // GET: Satotiedot/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satotiedot = await _context.Satotiedot
                .Include(s => s.Kasvi)
                .FirstOrDefaultAsync(m => m.SatoId == id);
            if (satotiedot == null)
            {
                return NotFound();
            }

            return View(satotiedot);
        }

        // POST: Satotiedot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var satotiedot = await _context.Satotiedot.FindAsync(id);
            _context.Satotiedot.Remove(satotiedot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SatotiedotExists(int id)
        {
            return _context.Satotiedot.Any(e => e.SatoId == id);
        }
    }
}
