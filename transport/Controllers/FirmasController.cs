using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using transport.Data;
using transport.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using transport.Models.ManageViewModels;
using transport.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace transport.Controllers
{
    public class FirmasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public FirmasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }
        
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Firmas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Firmy.ToListAsync());
        }

        // GET: Firmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firma = await _context.Firmy
                .SingleOrDefaultAsync(m => m.IdFirma == id);
            if (firma == null)
            {
                return NotFound();
            }

            return View(firma);
        }



        // GET: Firmas/Edit/5
        [Authorize(Roles ="Firma")]
        public async Task<IActionResult> Edit(int? id)
        {
            //  UserManager<ApplicationUser> _userManager;
            //_userManager.GetUserId(User);

            //  var user = HttpContext.User.GetUserId();

            // var user = await _userManager.GetUserAsync(HttpContext.User);

            // var user = await GetCurrentUserAsync();

           // id = await GetCurrentUserFirmaId();

            if (id == null)
            {
                return NotFound();
            }

            var firma = await _context.Firmy.SingleOrDefaultAsync(m => m.IdFirma == id);
          
            if (firma == null)
            {
                return NotFound();
            }
            return View(firma);
        }

        // POST: Firmas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Firma")]
        public async Task<IActionResult> Edit(int id, [Bind("IdFirma,Nazwa,NIP,Regon,Wlasciciel,UlicaF,KodF,MiastoF,TelefonF,OCP,AktywnyF")] Firma firma)
        {
            if (id != firma.IdFirma)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(firma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FirmaExists(firma.IdFirma))
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
            return View(firma);
        }            

        private bool FirmaExists(int id)
        {
            return _context.Firmy.Any(e => e.IdFirma == id);
        }
    }
}
