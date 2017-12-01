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
    public class KontrahentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KontrahentsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Kontrahents
        [Authorize(Roles = "Firma, Spedytor, Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kontrahenci.ToListAsync());
        }

        // GET: Kontrahents/Details/5
        [Authorize(Roles = "Firma, Spedytor, Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kontrahent = await _context.Kontrahenci
                .SingleOrDefaultAsync(m => m.IdKontrahent == id);
            if (kontrahent == null)
            {
                return NotFound();
            }

            return View(kontrahent);
        }

        // GET: Kontrahents/Create
        [Authorize(Roles = "Firma, Spedytor, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kontrahents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Spedytor, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKontrahent,Nazwa,NIP,Regon,Wlasciciel,Ulica,Kod,Miasto,Telefon,EMail,Typ,Aktywny")] Kontrahent kontrahent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kontrahent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(kontrahent);
        }

        // GET: Kontrahents/Edit/5
        [Authorize(Roles = "Firma, Spedytor, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kontrahent = await _context.Kontrahenci.SingleOrDefaultAsync(m => m.IdKontrahent == id);
            if (kontrahent == null)
            {
                return NotFound();
            }
            return View(kontrahent);
        }

        // POST: Kontrahents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Spedytor, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKontrahent,Nazwa,NIP,Regon,Wlasciciel,Ulica,Kod,Miasto,Telefon,EMail,Typ,Aktywny")] Kontrahent kontrahent)
        {
            if (id != kontrahent.IdKontrahent)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kontrahent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KontrahentExists(kontrahent.IdKontrahent))
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
            return View(kontrahent);
        }

        // GET: Kontrahents/Delete/5
        [Authorize(Roles = "Firma, Spedytor, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kontrahent = await _context.Kontrahenci
                .SingleOrDefaultAsync(m => m.IdKontrahent == id);
            if (kontrahent == null)
            {
                return NotFound();
            }

            return View(kontrahent);
        }

        // POST: Kontrahents/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Firma, Spedytor, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kontrahent = await _context.Kontrahenci.SingleOrDefaultAsync(m => m.IdKontrahent == id);
            _context.Kontrahenci.Remove(kontrahent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool KontrahentExists(int id)
        {
            return _context.Kontrahenci.Any(e => e.IdKontrahent == id);
        }
    }
}
