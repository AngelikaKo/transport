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
    public class NaczepasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NaczepasController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Naczepas
        [Authorize(Roles = "Firma, Admin, Spedytor")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Naczepy.Include(n => n.Pracownicy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Naczepas/Details/5
        [Authorize(Roles = "Firma, Admin, Spedytor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naczepa = await _context.Naczepy
                .Include(n => n.Pracownicy)
                .SingleOrDefaultAsync(m => m.IdNaczepa == id);
            if (naczepa == null)
            {
                return NotFound();
            }

            return View(naczepa);
        }

        // GET: Naczepas/Create
        [Authorize(Roles = "Firma, Admin")]
        public IActionResult Create()
        {
            ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "IdPracownik");
            return View();
        }

        // POST: Naczepas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNaczepa,IdPracownik,Marka,Rodzaj,NrRejestr,DataProd,Wymiary,DataPrzegl,DataUbez,Wyposazenie,Aktywny")] Naczepa naczepa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(naczepa);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "IdPracownik", naczepa.IdPracownik);
            return View(naczepa);
        }

        // GET: Naczepas/Edit/5
        [Authorize(Roles = "Firma, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naczepa = await _context.Naczepy.SingleOrDefaultAsync(m => m.IdNaczepa == id);
            if (naczepa == null)
            {
                return NotFound();
            }
            ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "IdPracownik", naczepa.IdPracownik);
            return View(naczepa);
        }

        // POST: Naczepas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNaczepa,IdPracownik,Marka,Rodzaj,NrRejestr,DataProd,Wymiary,DataPrzegl,DataUbez,Wyposazenie,Aktywny")] Naczepa naczepa)
        {
            if (id != naczepa.IdNaczepa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(naczepa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NaczepaExists(naczepa.IdNaczepa))
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
            ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "IdPracownik", naczepa.IdPracownik);
            return View(naczepa);
        }

        // GET: Naczepas/Delete/5
        [Authorize(Roles = "Firma, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naczepa = await _context.Naczepy
                .Include(n => n.Pracownicy)
                .SingleOrDefaultAsync(m => m.IdNaczepa == id);
            if (naczepa == null)
            {
                return NotFound();
            }

            return View(naczepa);
        }

        // POST: Naczepas/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var naczepa = await _context.Naczepy.SingleOrDefaultAsync(m => m.IdNaczepa == id);
            _context.Naczepy.Remove(naczepa);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool NaczepaExists(int id)
        {
            return _context.Naczepy.Any(e => e.IdNaczepa == id);
        }
    }
}
