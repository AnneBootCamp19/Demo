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
    public class IstutuksetController : Controller
    {
        private readonly PuutarhaContext _context;

        public IstutuksetController(PuutarhaContext context)
        {
            _context = context;
        }

        // GET: Istutukset
        public async Task<IActionResult> Index()
        {
            var puutarhaContext = _context.Istutukset.Include(i => i.Kasvi);
            return View(await puutarhaContext.ToListAsync());
        }

        // GET: Istutukset/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var istutukset = await _context.Istutukset
                .Include(i => i.Kasvi)
                .FirstOrDefaultAsync(m => m.IstutusId == id);
            if (istutukset == null)
            {
                return NotFound();
            }

            return View(istutukset);
        }

        // GET: Istutukset/Create
        public IActionResult Create()
        {
            ViewData["KasviId"] = new SelectList(_context.Kasvit, "KasviId", "Nimi");
            return View();
        }

        // POST: Istutukset/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IstutusId,KasviId,IstutusPvm,Määrä,Yksikkö,Istutuspaikka,Lämpötila,Lisätieto,Poistettu,Poistopvm")] Istutukset istutukset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(istutukset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KasviId"] = new SelectList(_context.Kasvit, "KasviId", "Nimi", istutukset.KasviId);
            return View(istutukset);
        }

        // GET: Istutukset/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var istutukset = await _context.Istutukset.FindAsync(id);
            if (istutukset == null)
            {
                return NotFound();
            }
            ViewData["KasviId"] = new SelectList(_context.Kasvit, "KasviId", "Nimi", istutukset.KasviId);
            return View(istutukset);
        }

        // POST: Istutukset/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IstutusId,KasviId,IstutusPvm,Määrä,Yksikkö,Istutuspaikka,Lämpötila,Lisätieto,Poistettu,Poistopvm")] Istutukset istutukset)
        {
            if (id != istutukset.IstutusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(istutukset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IstutuksetExists(istutukset.IstutusId))
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
            ViewData["KasviId"] = new SelectList(_context.Kasvit, "KasviId", "Nimi", istutukset.KasviId);
            return View(istutukset);
        }

        // GET: Istutukset/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var istutukset = await _context.Istutukset
                .Include(i => i.Kasvi)
                .FirstOrDefaultAsync(m => m.IstutusId == id);
            if (istutukset == null)
            {
                return NotFound();
            }

            return View(istutukset);
        }

        // POST: Istutukset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var istutukset = await _context.Istutukset.FindAsync(id);
            _context.Istutukset.Remove(istutukset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IstutuksetExists(int id)
        {
            return _context.Istutukset.Any(e => e.IstutusId == id);
        }
    }
}
