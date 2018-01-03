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
using Microsoft.AspNetCore.Identity;
using transport.Models;

namespace transport.Controllers
{
    public class PojazdsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public PojazdsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;    
        }

        // GET: Pojazds
        [Authorize(Roles ="Firma, Admin, Spedytor")]
        public async Task<IActionResult> Index()
        {

            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (firma != null)
            {
                var pojazdy = _context.Pojazdy.Where(p => p.IdFirma == firma.IdFirma);
                return View(await pojazdy.ToListAsync());
                //return View(firma.Pracownicy.ToList());
            }
            else
            {
                return View(await _context.Pojazdy.ToListAsync());
            }

        }

        // GET: Pojazds
        [Authorize(Roles = "Firma, Admin, Spedytor")]
        public async Task<IActionResult> IndexSpedytor()
        {

            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (firma != null)
            {
                var pojazdy = _context.Pojazdy.Where(p => p.IdFirma == firma.FirmaId);
                return View(await pojazdy.ToListAsync());
                //return View(firma.Pracownicy.ToList());
            }
            else
            {
                return View(await _context.Pojazdy.ToListAsync());
            }

        }


        // GET: Pojazds
        [Authorize(Roles = "Kierowca")]
        public async Task<IActionResult> IndexKierowca()
        {

            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (firma != null)
            {
                var pojazdy = _context.Pojazdy.Where(p => p.IdFirma == firma.FirmaId);
                return View(await pojazdy.Where(p => p.IdPracownik == firma.PracownikId).ToListAsync());
                //return View(firma.Pracownicy.ToList());
            }
            else
            {
                return View(await _context.Pojazdy.ToListAsync());
            }

        }

        // GET: Pojazds/Details/5
        [Authorize(Roles = "Firma, Admin, Spedytor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pojazd = await _context.Pojazdy
                .Include(p => p.Firma)
                .Include(p => p.Pracownik)
                .SingleOrDefaultAsync(m => m.IdPojazd == id);
            if (pojazd == null)
            {
                return NotFound();
            }

            return View(pojazd);
        }

        // GET: Pojazds/Create
        [Authorize(Roles = "Firma, Admin")]
        public IActionResult Create()
        {           
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId =s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko}), 
            "PracownikId", "FullName");
            return View();
        }

        // POST: Pojazds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPojazd,Firma,IdPracownik,Marka,Model,VIN,NrRejestr,DataProd,TachoOdczyt,TachoLegal,DataPrzegl,DataUbez,SpalanieSred,PrzebiegZakup,PrzebiegAktu,PrzebiegSerwis,RodzajKabiny,EmisjaSpalin,Retarder,Aktywny")] Pojazd pojazd)
        {
            if (ModelState.IsValid)
            {
                var currentuser = await _userManager.GetUserAsync(HttpContext.User);
                var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

                pojazd.Firma = firma;

                _context.Add(pojazd);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }           
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId =s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko}), 
            "PracownikId", "FullName", null);
            return View(pojazd);
        }

        // GET: Pojazds/Edit/5
        [Authorize(Roles = "Firma, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pojazd = await _context.Pojazdy.SingleOrDefaultAsync(m => m.IdPojazd == id);
            if (pojazd == null)
            {
                return NotFound();
            }
           // ViewData["IdFirma"] = new SelectList(_context.Firmy, "IdFirma", "IdFirma", pojazd.IdFirma);
           ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko }),
            "PracownikId", "FullName", null);
            return View(pojazd);
        }

        // POST: Pojazds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPojazd,Firma,IdPracownik,Marka,Model,VIN,NrRejestr,DataProd,TachoOdczyt,TachoLegal,DataPrzegl,DataUbez,SpalanieSred,PrzebiegZakup,PrzebiegAktu,PrzebiegSerwis,RodzajKabiny,EmisjaSpalin,Retarder,Aktywny")] Pojazd pojazd)
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (id != pojazd.IdPojazd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pojazd.Firma = firma;
                    _context.Update(pojazd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PojazdExists(pojazd.IdPojazd))
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
           // ViewData["IdFirma"] = new SelectList(_context.Firmy, "IdFirma", "IdFirma", pojazd.IdFirma);
           ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko }),
            "PracownikId", "FullName", null);
            return View(pojazd);
        }

        // GET: Pojazds/Delete/5
        [Authorize(Roles = "Firma, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pojazd = await _context.Pojazdy
                .Include(p => p.Firma)
                .Include(p => p.Pracownik)
                .SingleOrDefaultAsync(m => m.IdPojazd == id);
            if (pojazd == null)
            {
                return NotFound();
            }

            return View(pojazd);
        }

        // POST: Pojazds/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pojazd = await _context.Pojazdy.SingleOrDefaultAsync(m => m.IdPojazd == id);
            _context.Pojazdy.Remove(pojazd);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PojazdExists(int id)
        {
            return _context.Pojazdy.Any(e => e.IdPojazd == id);
        }
    }
}
