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
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using transport.Models;

namespace transport.Controllers
{
    public class ZleceniesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public ZleceniesController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;    
        }

        //Get: trasa googleMaps
        public async Task<IActionResult> Trasa(int? id, string adres1, string adres2, string link)
        {
            if (id == null)
            {
                return NotFound();
            }
            var zlecenie = await _context.Zlecenia.SingleOrDefaultAsync(m => m.IdZlecenie == id);
            if (zlecenie == null)
            {
                return NotFound();
            }

            adres1 = zlecenie.AdresOdbioru;
            adres2 = zlecenie.AdresDosta;

            link = "https://www.google.pl/maps/dir/"+adres1+"/"+adres2;

            ViewData["trasa"] = link;

            ViewBag.MyUrl = link;

            return Redirect(link);
            
        }
        

        // GET: Zlecenies/zmiana statusu dla kierowcy
        [Authorize(Roles = "Kierowca")]
        public async Task<IActionResult> Akceptuje(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zlecenie = await _context.Zlecenia.SingleOrDefaultAsync(m => m.IdZlecenie == id); 
            if (zlecenie == null)
            {
                return NotFound();
            }       
            return View(zlecenie);
        }

        // POST: Zlecenies/zmiana statusu dla kierowcy        
        [HttpPost]
        [Authorize(Roles = "Kierowca")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Akceptuje(int id, [Bind("IdZlecenie,Status")] Zlecenie zlecenie)
        {
            if (id != zlecenie.IdZlecenie)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var z1 = _context.Zlecenia.FirstOrDefault(z => z.IdZlecenie == zlecenie.IdZlecenie);
                    z1.Status = zlecenie.Status;
                    _context.Update(z1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZlecenieExists(zlecenie.IdZlecenie))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexKierowca");
            }         
            return View(zlecenie);
        }

    

        // GET: Zlecenies sortowanie i wyszukiwanie
        [Authorize(Roles = "Firma, Kierowca, Spedytor")]
        public async Task<IActionResult> Index(int? id, string sortOrder, string searchString)
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date" ;
            ViewData["DateSortParm2"] = sortOrder == "Date2" ? "date_desc2" : "Date2";
            ViewData["CurrentFilter"] = searchString;

            if (firma != null)
            { 

                var noweZlec = from s in _context.Zlecenia.Where(p => p.IdFirma == firma.IdFirma)

                .Include(z => z.Kontrahent)
                .Include(z => z.Naczepa)
                .Include(z => z.Pojazd)
                .Include(z => z.Pracownik)
                           select s;
           
                
                
                if (!String.IsNullOrEmpty(searchString))
            {
                noweZlec = noweZlec.Where(s => s.Status.Contains(searchString)
                || s.WagaTow.Contains(searchString)
                || s.AdresOdbioru.Contains(searchString)
                || s.AdresDosta.Contains(searchString)
                || s.Kontrahent.Nazwa.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "status_desc":
                    noweZlec = noweZlec.OrderByDescending(s => s.Status);
                    break;
                case "Date":
                    noweZlec = noweZlec.OrderBy(s => s.DataZalad);
                    break;
                case "date_desc":
                    noweZlec = noweZlec.OrderByDescending(s => s.DataZalad);
                    break;
                case "Date2":
                    noweZlec = noweZlec.OrderBy(s => s.DataRozl);
                    break;
                case "date_desc2":
                    noweZlec = noweZlec.OrderByDescending(s => s.DataRozl);
                    break;
                default:
                   noweZlec = noweZlec.OrderBy(s => s.Status);
                    break;
            }


                return View(await noweZlec.ToListAsync());            
            }
            else
            {
                return View(await _context.Pojazdy.ToListAsync());
            }
        }

        // GET: Zlecenies sortowanie i wyszukiwanie
        [Authorize(Roles = "Firma, Kierowca, Spedytor")]
        public async Task<IActionResult> IndexSpedytor(int? id, string sortOrder, string searchString)
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            // var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);
            var firma = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

            ViewData["NamesSortParm"] = String.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
            ViewData["DatesSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["DatesSortParm2"] = sortOrder == "Date2" ? "date_desc2" : "Date2";
            ViewData["CurrentFilter"] = searchString;

            if (firma != null)
            {
                
                 var noweZlec = from s in _context.Zlecenia.Where(p => p.IdFirma == firma.FirmaId)

                .Include(z => z.Kontrahent)
                .Include(z => z.Naczepa)
                .Include(z => z.Pojazd)
                .Include(z => z.Pracownik)
                               select s;



                if (!String.IsNullOrEmpty(searchString))
                {
                    noweZlec = noweZlec.Where(s => s.Status.Contains(searchString)
                    || s.WagaTow.Contains(searchString)
                    || s.AdresOdbioru.Contains(searchString)
                    || s.AdresDosta.Contains(searchString)
                    || s.Kontrahent.Nazwa.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "status_desc":
                        noweZlec = noweZlec.OrderByDescending(s => s.Status);
                        break;
                    case "Date":
                        noweZlec = noweZlec.OrderBy(s => s.DataZalad);
                        break;
                    case "date_desc":
                        noweZlec = noweZlec.OrderByDescending(s => s.DataZalad);
                        break;
                    case "Date2":
                        noweZlec = noweZlec.OrderBy(s => s.DataRozl);
                        break;
                    case "date_desc2":
                        noweZlec = noweZlec.OrderByDescending(s => s.DataRozl);
                        break;
                    default:
                        noweZlec = noweZlec.OrderBy(s => s.Status);
                        break;
                }


                return View(await noweZlec.ToListAsync());
            }
            else
            {
                return View(await _context.Zlecenia.ToListAsync());
            }
        }

        // GET: Zlecenies sortowanie i wyszukiwanie
        [Authorize(Roles = "Firma, Kierowca, Spedytor")]
        public async Task<IActionResult> IndexKierowca(int? id, string sortOrder, string searchString)
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            // var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);
            var firma = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);
            
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["DateSortParm2"] = sortOrder == "Date2" ? "date_desc2" : "Date2";
            ViewData["CurrentFilter"] = searchString;

            if (firma != null)
            {

                var noweZlec = from s in _context.Zlecenia.Where(p => p.IdFirma == firma.FirmaId && p.IdPracownik == firma.PracownikId)

               .Include(z => z.Kontrahent)
               .Include(z => z.Naczepa)
               .Include(z => z.Pojazd)
               .Include(z => z.Pracownik)
                               select s;



                if (!String.IsNullOrEmpty(searchString))
                {
                    noweZlec = noweZlec.Where(s => s.Status.Contains(searchString)
                    || s.WagaTow.Contains(searchString)
                    || s.AdresOdbioru.Contains(searchString)
                    || s.AdresDosta.Contains(searchString)
                    || s.Kontrahent.Nazwa.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "status_desc":
                        noweZlec = noweZlec.OrderByDescending(s => s.Status);
                        break;
                    case "Date":
                        noweZlec = noweZlec.OrderBy(s => s.DataZalad);
                        break;
                    case "date_desc":
                        noweZlec = noweZlec.OrderByDescending(s => s.DataZalad);
                        break;
                    case "Date2":
                        noweZlec = noweZlec.OrderBy(s => s.DataRozl);
                        break;
                    case "date_desc2":
                        noweZlec = noweZlec.OrderByDescending(s => s.DataRozl);
                        break;
                    default:
                        noweZlec = noweZlec.OrderBy(s => s.Status);
                        break;
                }


                return View(await noweZlec.ToListAsync());
            }
            else
            {
                return View(await _context.Pojazdy.ToListAsync());
            }
        }

        // GET: Zlecenies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zlecenie = await _context.Zlecenia
                .Include(z => z.Kontrahent)
                .Include(z => z.Naczepa)
                .Include(z => z.Pojazd)
                .Include(z => z.Pracownik)
                .SingleOrDefaultAsync(m => m.IdZlecenie == id);
            if (zlecenie == null)
            {
                return NotFound();
            }

            return View(zlecenie);
        }

        // GET: Zlecenies/Create
        [Authorize(Roles = "Firma, Spedytor")]
        public IActionResult Create()
        {
            ViewData["IdKontrahent"] = new SelectList(_context.Kontrahenci, "IdKontrahent", "Nazwa");
            ViewData["IdNaczepa"] = new SelectList(_context.Naczepy, "IdNaczepa", "NrRejestr");
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "NrRejestr");
           // ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "Imie");
           ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
             PracownikId = s.PracownikId,
             FullName = s.Imie + " " + s.Nazwisko}),
             "PracownikId", "FullName", null);
            return View();
        }

        // POST: Zlecenies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Spedytor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZlecenie,Firma,IdKontrahent,IdPracownik,IdPojazd,IdNaczepa,AdresOdbioru,AdresDosta,DataZalad,GodzZalad,DataRozl,GodzRozl,Uwagi,DaneTowar,WagaTow,WartoscNetto,Aktywny,Waluta,Status")] Zlecenie zlecenie)
        {
            if (ModelState.IsValid)
            {
                var currentuser = await _userManager.GetUserAsync(HttpContext.User);
                var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);
                               
                 zlecenie.Firma = firma;

                _context.Add(zlecenie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["IdKontrahent"] = new SelectList(_context.Kontrahenci, "IdKontrahent", "Nazwa", zlecenie.IdKontrahent);
            ViewData["IdNaczepa"] = new SelectList(_context.Naczepy, "IdNaczepa", "NrRejestr", zlecenie.IdNaczepa);
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "NrRejestr", zlecenie.IdPojazd); 
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko }),
            "PracownikId", "FullName", null);
            
           // ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "Imie", zlecenie.IdPracownik);
            return View(zlecenie);
        }

        // GET: Zlecenies/Create
        [Authorize(Roles = "Firma, Spedytor")]
        public IActionResult CreateSpedytor()
        {
            ViewData["IdKontrahent"] = new SelectList(_context.Kontrahenci, "IdKontrahent", "Nazwa");
            ViewData["IdNaczepa"] = new SelectList(_context.Naczepy, "IdNaczepa", "NrRejestr");
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "NrRejestr");
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko }),
            "PracownikId", "FullName", null);
            return View();
        }

        // POST: Zlecenies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Spedytor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSpedytor([Bind("IdZlecenie,IdFirma,Pracownik,IdKontrahent,IdPojazd,IdNaczepa,AdresOdbioru,AdresDosta,DataZalad,GodzZalad,DataRozl,GodzRozl,Uwagi,DaneTowar,WagaTow,WartoscNetto,Aktywny,Waluta,Status")] Zlecenie zlecenie)
        {
            if (ModelState.IsValid)
            {
                var currentuser = await _userManager.GetUserAsync(HttpContext.User);
                // var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);
                var pracownik = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

                zlecenie.IdFirma = pracownik.FirmaId;
                zlecenie.Pracownik = pracownik;
                

                _context.Add(zlecenie);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexSpedytor");
            }
            ViewData["IdKontrahent"] = new SelectList(_context.Kontrahenci, "IdKontrahent", "Nazwa", zlecenie.IdKontrahent);
            ViewData["IdNaczepa"] = new SelectList(_context.Naczepy, "IdNaczepa", "NrRejestr", zlecenie.IdNaczepa);
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "NrRejestr", zlecenie.IdPojazd);
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko }),
            "PracownikId", "FullName", null);
            return View(zlecenie);
        }

        // GET: Zlecenies/Edit/5
        [Authorize(Roles = "Firma, Spedytor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zlecenie = await _context.Zlecenia.SingleOrDefaultAsync(m => m.IdZlecenie == id);
            if (zlecenie == null)
            {
                return NotFound();
            }
            ViewData["IdKontrahent"] = new SelectList(_context.Kontrahenci, "IdKontrahent", "Nazwa", zlecenie.IdKontrahent);
            ViewData["IdNaczepa"] = new SelectList(_context.Naczepy, "IdNaczepa", "NrRejestr", zlecenie.IdNaczepa);
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "NrRejestr", zlecenie.IdPojazd);
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko}),
            "PracownikId", "FullName", null);
            return View(zlecenie);
        }

        // POST: Zlecenies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Spedytor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdZlecenie,Firma,IdKontrahent,IdPracownik,IdPojazd,IdNaczepa,AdresOdbioru,AdresDosta,DataZalad,GodzZalad,DataRozl,GodzRozl,Uwagi,DaneTowar,WagaTow,WartoscNetto,Aktywny,Waluta,Status")] Zlecenie zlecenie)
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (id != zlecenie.IdZlecenie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    zlecenie.Firma = firma;
                    _context.Update(zlecenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZlecenieExists(zlecenie.IdZlecenie))
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
            ViewData["IdKontrahent"] = new SelectList(_context.Kontrahenci, "IdKontrahent", "Nazwa", zlecenie.IdKontrahent);
            ViewData["IdNaczepa"] = new SelectList(_context.Naczepy, "IdNaczepa", "NrRejestr", zlecenie.IdNaczepa);
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "NrRejestr", zlecenie.IdPojazd);
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko }),
            "PracownikId", "FullName", null);
            return View(zlecenie);
        }

        // GET: Zlecenies/Edit/5
        [Authorize(Roles = "Firma, Spedytor")]
        public async Task<IActionResult> EditSpedytor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zlecenie = await _context.Zlecenia.SingleOrDefaultAsync(m => m.IdZlecenie == id);
            if (zlecenie == null)
            {
                return NotFound();
            }
            ViewData["IdKontrahent"] = new SelectList(_context.Kontrahenci, "IdKontrahent", "Nazwa", zlecenie.IdKontrahent);
            ViewData["IdNaczepa"] = new SelectList(_context.Naczepy, "IdNaczepa", "NrRejestr", zlecenie.IdNaczepa);
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "NrRejestr", zlecenie.IdPojazd);
            ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko }),
            "PracownikId", "FullName", null);
            return View(zlecenie);
        }

        // POST: Zlecenies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Spedytor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSpedytor(int id, [Bind("IdZlecenie,IdFirma,IdKontrahent,IdPracownik,IdPojazd,IdNaczepa,AdresOdbioru,AdresDosta,DataZalad,GodzZalad,DataRozl,GodzRozl,Uwagi,DaneTowar,WagaTow,WartoscNetto,Aktywny,Waluta,Status")] Zlecenie zlecenie)
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var pracownik = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (id != zlecenie.IdZlecenie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    zlecenie.IdFirma = pracownik.FirmaId;
                    _context.Update(zlecenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZlecenieExists(zlecenie.IdZlecenie))
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
            ViewData["IdKontrahent"] = new SelectList(_context.Kontrahenci, "IdKontrahent", "Nazwa", zlecenie.IdKontrahent);
            ViewData["IdNaczepa"] = new SelectList(_context.Naczepy, "IdNaczepa", "NrRejestr", zlecenie.IdNaczepa);
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "NrRejestr", zlecenie.IdPojazd);
             ViewData["FullNamee"] = new SelectList((from s in _context.Pracownicy.ToList() select new {
            PracownikId = s.PracownikId,
            FullName = s.Imie + " " + s.Nazwisko }),
            "PracownikId", "FullName", null);
            return View(zlecenie);
        }

        // GET: Zlecenies/Delete/5
        [Authorize(Roles = "Firma, Spedytor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zlecenie = await _context.Zlecenia
                .Include(z => z.Kontrahent)
                .Include(z => z.Naczepa)
                .Include(z => z.Pojazd)
                .Include(z => z.Pracownik)
                .SingleOrDefaultAsync(m => m.IdZlecenie == id);
            if (zlecenie == null)
            {
                return NotFound();
            }

            return View(zlecenie);
        }

        // POST: Zlecenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Firma, Spedytor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zlecenie = await _context.Zlecenia.SingleOrDefaultAsync(m => m.IdZlecenie == id);
            _context.Zlecenia.Remove(zlecenie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ZlecenieExists(int id)
        {
            return _context.Zlecenia.Any(e => e.IdZlecenie == id);
        }

        // GET: Zlecenies/Delete/5
        [Authorize(Roles = "Firma, Spedytor")]
        public async Task<IActionResult> DeleteSpedytor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zlecenie = await _context.Zlecenia
                .Include(z => z.Kontrahent)
                .Include(z => z.Naczepa)
                .Include(z => z.Pojazd)
                .Include(z => z.Pracownik)
                .SingleOrDefaultAsync(m => m.IdZlecenie == id);
            if (zlecenie == null)
            {
                return NotFound();
            }

            return View(zlecenie);
        }

        // POST: Zlecenies/Delete/5
        [HttpPost, ActionName("DeleteSpedytor")]
        [Authorize(Roles = "Firma, Spedytor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedSpedytor(int id)
        {
            var zlecenie = await _context.Zlecenia.SingleOrDefaultAsync(m => m.IdZlecenie == id);
            _context.Zlecenia.Remove(zlecenie);
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexSpedytor");
        }

       
    }
}
