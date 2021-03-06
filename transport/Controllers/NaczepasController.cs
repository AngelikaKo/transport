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
    public class NaczepasController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public NaczepasController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)            
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;    
        }

        // GET: Naczepas
        [Authorize(Roles = "Firma, Admin, Spedytor")]
        public async Task<IActionResult> Index()
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (firma != null)
            {
                var naczepy = _context.Naczepy.Where(p => p.IdFirma == firma.IdFirma);
                return View(await naczepy.ToListAsync());
                //return View(firma.Pracownicy.ToList());
            }
            else
            {
                return View(await _context.Naczepy.ToListAsync());
            }
           // var applicationDbContext = _context.Naczepy.Include(n => n.Pracownik);
           // return View(await applicationDbContext.ToListAsync());
        }

        // GET: Naczepas
        [Authorize(Roles = "Firma, Admin, Spedytor")]
        public async Task<IActionResult> IndexSpedytor()
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (firma != null)
            {
                var naczepy = _context.Naczepy.Where(p => p.IdFirma == firma.FirmaId);
                return View(await naczepy.ToListAsync());
                //return View(firma.Pracownicy.ToList());
            }
            else
            {
                return View(await _context.Naczepy.ToListAsync());
            }
            // var applicationDbContext = _context.Naczepy.Include(n => n.Pracownik);
            // return View(await applicationDbContext.ToListAsync());
        }

        // GET: Naczepas
        [Authorize(Roles = "Kierowca")]
        public async Task<IActionResult> IndexKierowca()
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (firma != null)
            {
                var naczepy = _context.Naczepy.Where(p => p.IdFirma == firma.FirmaId);
                return View(await naczepy.Where(p => p.IdPracownik == firma.PracownikId).ToListAsync());
                //return View(firma.Pracownicy.ToList());
            }
            else
            {
                return View(await _context.Naczepy.ToListAsync());
            }
            // var applicationDbContext = _context.Naczepy.Include(n => n.Pracownik);
            // return View(await applicationDbContext.ToListAsync());
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
                .Include(n => n.Pracownik)
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
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko}),
            "PracownikId", "FullName");           
            return View();
        }

        // POST: Naczepas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNaczepa,Firma,IdPracownik,Marka,Rodzaj,NrRejestr,DataProd,Wymiary,DataPrzegl,DataUbez,Wyposazenie,Aktywny")] Naczepa naczepa)
        {
            if (ModelState.IsValid)
            {
                var currentuser = await _userManager.GetUserAsync(HttpContext.User);
                var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

                naczepa.Firma = firma;

                _context.Add(naczepa);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko}),
            "PracownikId", "FullName", null);
            return View(naczepa);

            
        }

         // GET: Naczepas/Create
        [Authorize(Roles = "Firma, Admin")]
        public IActionResult CreateAdmin()
        {
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko}),
            "PracownikId", "FullName");           
            return View();
        }

        // POST: Naczepas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin([Bind("IdNaczepa,IdFirma,Pracownik,Marka,Rodzaj,NrRejestr,DataProd,Wymiary,DataPrzegl,DataUbez,Wyposazenie,Aktywny")] Naczepa naczepa)
        {
            if (ModelState.IsValid)
            {
                var currentuser = await _userManager.GetUserAsync(HttpContext.User);
                var firma = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

                naczepa.IdFirma = firma.FirmaId;
                naczepa.Pracownik = firma;
                _context.Add(naczepa);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexSpedytor");
            }
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko}),
            "PracownikId", "FullName", null);
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
           ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko }),
            "PracownikId", "FullName", null);
            return View(naczepa);
        }

        // POST: Naczepas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNaczepa,Firma,IdPracownik,Marka,Rodzaj,NrRejestr,DataProd,Wymiary,DataPrzegl,DataUbez,Wyposazenie,Aktywny")] Naczepa naczepa)
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (id != naczepa.IdNaczepa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    naczepa.Firma = firma;
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
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko }),
            "PracownikId", "FullName", null);
            return View(naczepa);
        }

        // GET: Naczepas/Edit/5
        [Authorize(Roles = "Firma, Admin")]
        public async Task<IActionResult> EditAdmin(int? id)
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
           ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko }),
            "PracownikId", "FullName", null);
            return View(naczepa);
        }

        // POST: Naczepas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(int id, [Bind("IdNaczepa,IdFirma,Pracownik,Marka,Rodzaj,NrRejestr,DataProd,Wymiary,DataPrzegl,DataUbez,Wyposazenie,Aktywny")] Naczepa naczepa)
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (id != naczepa.IdNaczepa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    naczepa.IdFirma = firma.FirmaId;
                    naczepa.Pracownik = firma;
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
                return RedirectToAction("IndexSpedytor");
            }
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko }),
            "PracownikId", "FullName", null);
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
                .Include(n => n.Pracownik)
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
            if (User.IsInRole("Firma"))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("IndexSpedytor");
            }
        }

        private bool NaczepaExists(int id)
        {
            return _context.Naczepy.Any(e => e.IdNaczepa == id);
        }
    }
}
