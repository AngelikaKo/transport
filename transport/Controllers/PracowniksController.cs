using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using transport.Data;
using transport.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace transport.Controllers
{
    public class PracowniksController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;


        public PracowniksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        //***************
        // wyswietlanie kierowców
      // public async Task<IActionResult> Kierowcy()        {
            
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!?????????????????*************************
           
       //     return View(await _context.Pracownicy.ToListAsync());        }
       //*******************************


        // GET: Pracowniks
        public async Task<IActionResult> Index()
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (firma != null)
            {                
                var pracownicy = _context.Pracownicy.Where(p => p.FirmaId == firma.IdFirma);
                return View(await pracownicy.ToListAsync());
                //return View(firma.Pracownicy.ToList());
            }
            else
            {
                return View(await _context.Pracownicy.ToListAsync());
            }

        }

        // GET: Pracowniks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownik = await _context.Pracownicy
                .SingleOrDefaultAsync(m => m.PracownikId == id);
            if (pracownik == null)
            {
                return NotFound();
            }

            return View(pracownik);
        }

        // GET: Pracowniks/Create
        [Authorize(Roles = "Firma, Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pracowniks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPracownik,Imie,Nazwisko,Ulica,Kod,Miasto,Telefon,DataUrodz,DataZatru,DataKonUmowy,DataKarty,DataOdczKart,NrDowoduOsob,Aktywny")] Pracownik pracownik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pracownik);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pracownik);
        }

        // GET: Pracowniks/Edit/5
        [Authorize(Roles = "Firma, Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownik = await _context.Pracownicy.SingleOrDefaultAsync(m => m.PracownikId == id);
            if (pracownik == null)
            {
                return NotFound();
            }
            return View(pracownik);
        }

        // POST: Pracowniks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles ="Firma, Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPracownik,Imie,Nazwisko,Ulica,Kod,Miasto,Telefon,DataUrodz,DataZatru,DataKonUmowy,DataKarty,DataOdczKart,NrDowoduOsob,Aktywny,FirmaId")] Pracownik pracownik)
        {
            if (id != pracownik.PracownikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pracownik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PracownikExists(pracownik.PracownikId))
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
            return View(pracownik);
        }

        // GET: Pracowniks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownik = await _context.Pracownicy
                .SingleOrDefaultAsync(m => m.PracownikId == id);
            if (pracownik == null)
            {
                return NotFound();
            }

            return View(pracownik);
        }

        // POST: Pracowniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pracownik = await _context.Pracownicy.SingleOrDefaultAsync(m => m.PracownikId == id);
            _context.Pracownicy.Remove(pracownik);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PracownikExists(int id)
        {
            return _context.Pracownicy.Any(e => e.PracownikId == id);
        }
    }
}
