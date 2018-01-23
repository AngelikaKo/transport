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
    public class KontrahentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public KontrahentsController(
             UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;    
        }

        // GET: Kontrahents
        [Authorize(Roles = "Firma, Spedytor, Admin")]
        public async Task<IActionResult> Index(int? id, string sortOrder, string searchString)
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nazwa_desc" : "";
            ViewData["MiastoSortParm"] = String.IsNullOrEmpty(sortOrder) ? "miasto_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            if (firma != null)
            {
                var kontrahenci = from s in _context.Kontrahenci.Where(p => p.IdFirma == firma.IdFirma) select s;


                if (!String.IsNullOrEmpty(searchString))
                {
                    kontrahenci = kontrahenci.Where(s => s.Nazwa.Contains(searchString)
                    || s.Wlasciciel.Contains(searchString)
                    || s.Miasto.Contains(searchString)
                    || s.NIP.Contains(searchString)
                    || s.Typ.Contains(searchString)
                    || s.Telefon.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "nazwa_desc":
                        kontrahenci = kontrahenci.OrderByDescending(s => s.Nazwa);
                        break;
                    case "Miasto":
                        kontrahenci = kontrahenci.OrderBy(s => s.Nazwa);
                        break;
                    case "miasto_desc":
                        kontrahenci = kontrahenci.OrderByDescending(s => s.Miasto);
                        break;
                    default:
                        kontrahenci = kontrahenci.OrderBy(s => s.Nazwa);
                        break;
                }
                

            return View(await kontrahenci.ToListAsync());
               
            }   
            else
            {
                return View(await _context.Kontrahenci.ToListAsync());
            }
                       
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
        public async Task<IActionResult> Create([Bind("IdKontrahent,Firma,Nazwa,NIP,Regon,Wlasciciel,Ulica,Kod,Miasto,Telefon,EMail,Typ,Aktywny")] Kontrahent kontrahent)
        {
            if (ModelState.IsValid)
            {
                var currentuser = await _userManager.GetUserAsync(HttpContext.User);
                var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

                kontrahent.Firma = firma;

                _context.Add(kontrahent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
           
            return View(kontrahent);
                        
        }

        // GET: Kontrahents/Create
        [Authorize(Roles = "Firma, Spedytor, Admin")]
        public IActionResult CreateSpedytor()
        {
            return View();
        }

        // POST: Kontrahents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Spedytor, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSpedytor([Bind("IdKontrahent,IdFirma,Nazwa,NIP,Regon,Wlasciciel,Ulica,Kod,Miasto,Telefon,EMail,Typ,Aktywny")] Kontrahent kontrahent)
        {
            if (ModelState.IsValid)
            {
                var currentuser = await _userManager.GetUserAsync(HttpContext.User);
               // var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);
                
                var pracownik = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

                kontrahent.IdFirma = pracownik.FirmaId;

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
        public async Task<IActionResult> Edit(int id, [Bind("IdKontrahent,Firma,Nazwa,NIP,Regon,Wlasciciel,Ulica,Kod,Miasto,Telefon,EMail,Typ,Aktywny")] Kontrahent kontrahent)
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (id != kontrahent.IdKontrahent)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    kontrahent.Firma = firma;
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

        // GET: Kontrahents/Edit/5
        [Authorize(Roles = "Firma, Spedytor, Admin")]
        public async Task<IActionResult> EditSpedytor(int? id)
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
        public async Task<IActionResult> EditSpedytor(int id, [Bind("IdKontrahent,IdFirma,Nazwa,NIP,Regon,Wlasciciel,Ulica,Kod,Miasto,Telefon,EMail,Typ,Aktywny")] Kontrahent kontrahent)
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            //var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);
                       
            var pracownik = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

           // zlecenie.IdFirma = pracownik.FirmaId;
           // zlecenie.Pracownik = pracownik;

            if (id != kontrahent.IdKontrahent)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    kontrahent.IdFirma = pracownik.FirmaId;
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
