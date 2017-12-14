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
    public class TankowaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TankowaniesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Tankowanies
        [Authorize(Roles = "Firma")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tankowania.Include(t => t.Pojazd).Include(t => t.Pracownik);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tankowanies/Details/5
        [Authorize(Roles = "Firma")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tankowanie = await _context.Tankowania
                .Include(t => t.Pojazd)
                .Include(t => t.Pracownik)
                .SingleOrDefaultAsync(m => m.IdTankowania == id);
            if (tankowanie == null)
            {
                return NotFound();
            }

            return View(tankowanie);
        }

        // GET: Tankowanies/Create
        [Authorize(Roles = "Firma, Kierowca")]
        public IActionResult Create()
        {
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "IdPojazd");
            ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "IdPracownik");
            return View();
        }

        // POST: Tankowanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Kierowca")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTankowania,IdPracownik,IdPojazd,PrzebiegTankow,IloscPaliwa,WartoscPaliwa,DataTank,Aktywny")] Tankowanie tankowanie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tankowanie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "IdPojazd", tankowanie.IdPojazd);
            ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "IdPracownik", tankowanie.IdPracownik);
            return View(tankowanie);
        }

        // GET: Tankowanies/Edit/5
        [Authorize(Roles = "Firma")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tankowanie = await _context.Tankowania.SingleOrDefaultAsync(m => m.IdTankowania == id);
            if (tankowanie == null)
            {
                return NotFound();
            }
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "IdPojazd", tankowanie.IdPojazd);
            ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "IdPracownik", tankowanie.IdPracownik);
            return View(tankowanie);
        }

        // POST: Tankowanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTankowania,IdPracownik,IdPojazd,PrzebiegTankow,IloscPaliwa,WartoscPaliwa,DataTank,Aktywny")] Tankowanie tankowanie)
        {
            if (id != tankowanie.IdTankowania)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tankowanie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TankowanieExists(tankowanie.IdTankowania))
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
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "IdPojazd", tankowanie.IdPojazd);
            ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "IdPracownik", tankowanie.IdPracownik);
            return View(tankowanie);
        }

        // GET: Tankowanies/Delete/5
        [Authorize(Roles = "Firma")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tankowanie = await _context.Tankowania
                .Include(t => t.Pojazd)
                .Include(t => t.Pracownik)
                .SingleOrDefaultAsync(m => m.IdTankowania == id);
            if (tankowanie == null)
            {
                return NotFound();
            }

            return View(tankowanie);
        }

        // POST: Tankowanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Firma")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tankowanie = await _context.Tankowania.SingleOrDefaultAsync(m => m.IdTankowania == id);
            _context.Tankowania.Remove(tankowanie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TankowanieExists(int id)
        {
            return _context.Tankowania.Any(e => e.IdTankowania == id);
        }
    }
}
