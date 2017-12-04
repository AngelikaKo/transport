﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using transport.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using transport.Models.AccountViewModels;
using System.ComponentModel;
using transport.Models.ApplicationModels;

namespace transport.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {  }

        public DbSet<transport.Models.ApplicationModels.Naczepa> Naczepy { get; set; }

        public DbSet<transport.Models.ApplicationModels.Tankowanie> Tankowania { get; set; }

        public DbSet<transport.Models.ApplicationModels.Zlecenie> Zlecenia { get; set; }

        public DbSet<transport.Models.ApplicationModels.Kontrahent> Kontrahenci { get; set; }

        public DbSet<transport.Models.ApplicationModels.Ogloszenie> Ogloszenia { get; set; }

        public DbSet<Firma> Firmy { get; set; }

        public DbSet<Pracownik> Pracownicy { get; set; }

        public DbSet<transport.Models.ApplicationModels.Pojazd> Pojazdy { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<transport.Models.ApplicationModels.Ogloszenie>()
                .Property(b => b.DataDodania)
                .HasDefaultValueSql("getdate()");

           // builder.Entity<transport.Models.ApplicationModels.Zlecenie>()
            //   .Property(b => b.Status)
            //   .HasDefaultValue("NOWE");


            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<transport.Models.AccountViewModels.RegisterAdministratorViewModel> RegisterAdministratorViewModel { get; set; }

        public DbSet<transport.Models.AccountViewModels.RegisterSpedytorViewModel> RegisterSpedytorViewModel { get; set; }

        public DbSet<transport.Models.AccountViewModels.RegisterKierowcaViewModel> RegisterKierowcaViewModel { get; set; }



    }
}