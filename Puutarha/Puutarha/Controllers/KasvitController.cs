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
    public class KasvitController : Controller
    {
        private readonly PuutarhaContext _context;

        public KasvitController(PuutarhaContext context)
        {
            _context = context;
        }

        // GET: Kasvit
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kasvit.ToListAsync());
        }

        // GET: Kasvit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kasvit = await _context.Kasvit
                .FirstOrDefaultAsync(m => m.KasviId == id);
            if (kasvit == null)
            {
                return NotFound();
            }

            return View(kasvit);
        }

        // GET: Kasvit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kasvit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KasviId,Nimi,Lajike,TieteellinenNimi,Kuva,Monivuotinen,Hyötykasvi,Poistettu")] Kasvit kasvit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kasvit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kasvit);
        }

        // GET: Kasvit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kasvit = await _context.Kasvit.FindAsync(id);
            if (kasvit == null)
            {
                return NotFound();
            }
            return View(kasvit);
        }

        // POST: Kasvit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KasviId,Nimi,Lajike,TieteellinenNimi,Kuva,Monivuotinen,Hyötykasvi,Poistettu")] Kasvit kasvit)
        {
            if (id != kasvit.KasviId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kasvit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KasvitExists(kasvit.KasviId))
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
            return View(kasvit);
        }

        // GET: Kasvit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kasvit = await _context.Kasvit
                .FirstOrDefaultAsync(m => m.KasviId == id);
            if (kasvit == null)
            {
                return NotFound();
            }

            return View(kasvit);
        }

        // POST: Kasvit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kasvit = await _context.Kasvit.FindAsync(id);
            _context.Kasvit.Remove(kasvit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KasvitExists(int id)
        {
            return _context.Kasvit.Any(e => e.KasviId == id);
        }
    }
}
