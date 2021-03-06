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
        private readonly SignInManager<ApplicationUser> _signInManager;
        
        public PracowniksController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }               

        // GET: Pracowniks
        public async Task<IActionResult> Index()
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (firma != null)
            {                
                var pracownicy = _context.Pracownicy.Where(p => p.FirmaId == firma.IdFirma);
                return View(await pracownicy.ToListAsync());                
            }
            else
            {
                return View(await _context.Pracownicy.ToListAsync());
            }
        }

        // GET: Pracowniks
        public async Task<IActionResult> IndexSpedytor()
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (firma != null)
            {
                var pracownicy = _context.Pracownicy.Where(p => p.FirmaId == firma.FirmaId);
                //pracownicy.Where(r => r.Stanowisko == "Kierowca");
               // pracownicy.Where(r => r.User.Roles.Equals("2513098d-e515-4a10-9200-0f26de27fcff"));

                return View(await pracownicy.Where(r => r.Stanowisko == "Kierowca").ToListAsync());
                //return View(firma.Pracownicy.ToList());
            }
            else
            {
                return View(await _context.Pracownicy.ToListAsync());
            }

        }

        // GET: Pracowniks
        public async Task<IActionResult> IndexAdmin()
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (firma != null)
            {
                var pracownicy = _context.Pracownicy.Where(p => p.FirmaId == firma.FirmaId);
                //pracownicy.Where(r => r.Stanowisko == "Kierowca");
                // pracownicy.Where(r => r.User.Roles.Equals("2513098d-e515-4a10-9200-0f26de27fcff"));

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
        [Authorize(Roles = "Firma, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pracowniks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PracownikId,Imie,Nazwisko,Ulica,Kod,Miasto,Telefon,DataUrodz,DataZatru,DataKonUmowy,DataKarty,DataOdczKart,NrDowoduOsob,Aktywny")] Pracownik pracownik)
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
        [Authorize(Roles = "Firma, Admin")]
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
        [Authorize(Roles ="Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PracownikId,Imie,Nazwisko,Ulica,Kod,Miasto,Telefon,DataUrodz,DataZatru,DataKonUmowy,DataKarty,DataOdczKart,NrDowoduOsob,Aktywny,Firma")] Pracownik pracownik)
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (id != pracownik.PracownikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pracownik.Firma = firma;
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

        // GET: Pracowniks/Edit/5
        [Authorize(Roles = "Firma, Admin")]
        public async Task<IActionResult> EditFirma(int? id)
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
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFirma(int id, [Bind("PracownikId,Imie,Nazwisko,Ulica,Kod,Miasto,Telefon,DataUrodz,DataZatru,DataKonUmowy,DataKarty,DataOdczKart,NrDowoduOsob,Aktywny,Firma,Stanowisko")] Pracownik pracownik)
        {
            var userid = pracownik.UserId;
            var stanowisko = pracownik.Stanowisko;
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);            

            if (id != pracownik.PracownikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pracownik.Stanowisko = stanowisko;
                    pracownik.UserId = userid;
                    pracownik.Firma = firma;
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

        // GET: Pracowniks/Edit/5
        [Authorize(Roles = "Firma, Admin")]
        public async Task<IActionResult> EditFirmaKierowca(int? id)
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
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFirmaKierowca(int id, [Bind("PracownikId,Imie,Nazwisko,Ulica,Kod,Miasto,Telefon,DataUrodz,DataZatru,DataKonUmowy,DataKarty,DataOdczKart,NrDowoduOsob,Aktywny,Firma,Stanowisko,UserId")] Pracownik pracownik)
        {
            //var userid = pracownik.UserId;           
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (id != pracownik.PracownikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pracownik.Stanowisko = "Kierowca";
                   // pracownik.UserId = userid;
                    pracownik.Firma = firma;
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

        // GET: Pracowniks/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAdminKierowca(int? id)
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdminKierowca(int id, [Bind("PracownikId,Imie,Nazwisko,Ulica,Kod,Miasto,Telefon,DataUrodz,DataZatru,DataKonUmowy,DataKarty,DataOdczKart,NrDowoduOsob,Aktywny,FirmaId,Stanowisko,UserId")] Pracownik pracownik)
        {
            //var userid = pracownik.UserId;           
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (id != pracownik.PracownikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pracownik.Stanowisko = "Kierowca";
                     pracownik.FirmaId = firma.FirmaId;
                   

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
                return RedirectToAction("IndexAdmin");
            }
            return View(pracownik);
        }


        // GET: Pracowniks/Edit/5
        [Authorize(Roles = "Firma, Admin")]
        public async Task<IActionResult> EditFirmaSpedytor(int? id)
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
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFirmaSpedytor(int id, [Bind("PracownikId,Imie,Nazwisko,Ulica,Kod,Miasto,Telefon,DataUrodz,DataZatru,DataKonUmowy,DataKarty,DataOdczKart,NrDowoduOsob,Aktywny,Firma,Stanowisko,UserId")] Pracownik pracownik)
        {
            //var userid = pracownik.UserId;           
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (id != pracownik.PracownikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pracownik.Stanowisko = "Spedytor";
                    // pracownik.UserId = userid;
                    pracownik.Firma = firma;
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

        // GET: Pracowniks/Edit/5
        [Authorize(Roles = "Firma, Admin")]
        public async Task<IActionResult> EditAdminSpedytor(int? id)
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
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdminSpedytor(int id, [Bind("PracownikId,Imie,Nazwisko,Ulica,Kod,Miasto,Telefon,DataUrodz,DataZatru,DataKonUmowy,DataKarty,DataOdczKart,NrDowoduOsob,Aktywny,FirmaId,Stanowisko,UserId")] Pracownik pracownik)
        {
            //var userid = pracownik.UserId;           
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (id != pracownik.PracownikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pracownik.Stanowisko = "Spedytor";

                    pracownik.FirmaId = firma.FirmaId;
                    
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
                return RedirectToAction("IndexAdmin");
            }
            return View(pracownik);
        }

        // GET: Pracowniks/Edit/5
        [Authorize(Roles = "Firma, Admin")]
        public async Task<IActionResult> EditFirmaAdministrator(int? id)
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
        [Authorize(Roles = "Firma, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFirmaAdministrator(int id, [Bind("PracownikId,Imie,Nazwisko,Ulica,Kod,Miasto,Telefon,DataUrodz,DataZatru,DataKonUmowy,DataKarty,DataOdczKart,NrDowoduOsob,Aktywny,Firma,Stanowisko,UserId")] Pracownik pracownik)
        {
           // var userid = pracownik.UserId;           
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Firmy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (id != pracownik.PracownikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pracownik.Stanowisko = "Administrator";
                   // pracownik.UserId = userid;
                    pracownik.Firma = firma;
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

        // GET: Pracowniks/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAdmintAdministrator(int? id)
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdminAdministrator(int id, [Bind("PracownikId,Imie,Nazwisko,Ulica,Kod,Miasto,Telefon,DataUrodz,DataZatru,DataKonUmowy,DataKarty,DataOdczKart,NrDowoduOsob,Aktywny,FirmaId,Stanowisko,UserId")] Pracownik pracownik)
        {
            // var userid = pracownik.UserId;           
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);
            var firma = _context.Pracownicy.FirstOrDefault(f => f.UserId == currentuser.Id);

            if (id != pracownik.PracownikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pracownik.Stanowisko = "Administrator";
                    // pracownik.UserId = userid;
                    pracownik.FirmaId = firma.FirmaId;
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
                return RedirectToAction("IndexAdmin");
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
            if (User.IsInRole("Firma"))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("IndexAdmin");
            }
        }

        private bool PracownikExists(int id)
        {
            return _context.Pracownicy.Any(e => e.PracownikId == id);
        }
    }
}
