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

namespace transport.Controllers
{
    public class ZleceniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZleceniesController(ApplicationDbContext context)
        {
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
            ViewData["IdKontrahent"] = new SelectList(_context.Kontrahenci, "IdKontrahent", "Nazwa", zlecenie.IdKontrahent);
            ViewData["IdNaczepa"] = new SelectList(_context.Naczepy, "IdNaczepa", "NrRejestr", zlecenie.IdNaczepa);
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "NrRejestr", zlecenie.IdPojazd);
            ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "Imie", zlecenie.IdPracownik);
            return View(zlecenie);
        }

        // POST: Zlecenies/zmiana statusu dla kierowcy
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                return RedirectToAction("Index");
            }
            ViewData["IdKontrahent"] = new SelectList(_context.Kontrahenci, "IdKontrahent", "Nazwa", zlecenie.IdKontrahent);
            ViewData["IdNaczepa"] = new SelectList(_context.Naczepy, "IdNaczepa", "NrRejestr", zlecenie.IdNaczepa);
            ViewData["IdPojazd"] = new SelectList(_context.Pojazdy, "IdPojazd", "NrRejestr", zlecenie.IdPojazd);
            ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "Imie", zlecenie.IdPracownik);
            return View(zlecenie);
        }

    

        // GET: Zlecenies sortowanie i wyszukiwanie
        [Authorize(Roles = "Firma, Kierowca, Spedytor")]
        public async Task<IActionResult> Index(int? id, string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date" ;
            ViewData["DateSortParm2"] = sortOrder == "Date2" ? "date_desc2" : "Date2";
            ViewData["CurrentFilter"] = searchString;

            var noweZlec = from s in _context.Zlecenia

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
            ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "Imie");
            return View();
        }

        // POST: Zlecenies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Spedytor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZlecenie,IdKontrahent,IdPracownik,IdPojazd,IdNaczepa,AdresOdbioru,AdresDosta,DataZalad,GodzZalad,DataRozl,GodzRozl,Uwagi,DaneTowar,WagaTow,WartoscNetto,Aktywny,Waluta,Status")] Zlecenie zlecenie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zlecenie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
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
        public async Task<IActionResult> Edit(int id, [Bind("IdZlecenie,IdKontrahent,IdPracownik,IdPojazd,IdNaczepa,AdresOdbioru,AdresDosta,DataZalad,GodzZalad,DataRozl,GodzRozl,Uwagi,DaneTowar,WagaTow,WartoscNetto,Aktywny,Waluta,Status")] Zlecenie zlecenie)
        {
            if (id != zlecenie.IdZlecenie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["IdPracownik"] = new SelectList(_context.Pracownicy, "IdPracownik", "Imie", zlecenie.IdPracownik);
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
    }
}
