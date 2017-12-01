using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using transport.Data;
using transport.Models.ApplicationModels;
using Microsoft.AspNetCore.Authorization;

namespace transport.Controllers
{
    public class OgloszeniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OgloszeniesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Ogloszenies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ogloszenia.Include(o => o.Firmy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ogloszenies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenia
                .Include(o => o.Firmy)
                .SingleOrDefaultAsync(m => m.IdOgloszenie == id);
            if (ogloszenie == null)
            {
                return NotFound();
            }

            return View(ogloszenie);
        }

        // GET: Ogloszenies/Create
        [Authorize(Roles ="Firma, Admin")]
        public IActionResult Create()
        {
            ViewData["FirmaId"] = new SelectList(_context.Firmy, "IdFirma", "IdFirma");
            return View();
        }

        // POST: Ogloszenies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOgloszenie,FirmaId,DataDodania,Typ,Tresc,Aktywny")] Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ogloszenie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FirmaId"] = new SelectList(_context.Firmy, "IdFirma", "IdFirma", ogloszenie.FirmaId);
            return View(ogloszenie);
        }

        // GET: Ogloszenies/Edit/5
        [Authorize(Roles = "Firma, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenia.SingleOrDefaultAsync(m => m.IdOgloszenie == id);
            if (ogloszenie == null)
            {
                return NotFound();
            }
            ViewData["FirmaId"] = new SelectList(_context.Firmy, "IdFirma", "IdFirma", ogloszenie.FirmaId);
            return View(ogloszenie);
        }

        // POST: Ogloszenies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Firma, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOgloszenie,FirmaId,DataDodania,Typ,Tresc,Aktywny")] Ogloszenie ogloszenie)
        {
            if (id != ogloszenie.IdOgloszenie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ogloszenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgloszenieExists(ogloszenie.IdOgloszenie))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["FirmaId"] = new SelectList(_context.Firmy, "IdFirma", "IdFirma", ogloszenie.FirmaId);
            return View(ogloszenie);
        }

        // GET: Ogloszenies/Delete/5
        [Authorize(Roles = "Firma, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenia
                .Include(o => o.Firmy)
                .SingleOrDefaultAsync(m => m.IdOgloszenie == id);
            if (ogloszenie == null)
            {
                return NotFound();
            }

            return View(ogloszenie);
        }

        // POST: Ogloszenies/Delete/5
        [Authorize(Roles = "Firma, Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ogloszenie = await _context.Ogloszenia.SingleOrDefaultAsync(m => m.IdOgloszenie == id);
            _context.Ogloszenia.Remove(ogloszenie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OgloszenieExists(int id)
        {
            return _context.Ogloszenia.Any(e => e.IdOgloszenie == id);
        }
    }
}
