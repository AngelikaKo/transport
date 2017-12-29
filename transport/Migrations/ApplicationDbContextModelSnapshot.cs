using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using transport.Data;

namespace transport.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("transport.Models.AccountViewModels.RegisterAdministratorViewModel", b =>
                {
                    b.Property<int>("IdPracownik")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aktywny");

                    b.Property<string>("ConfirmPassword");

                    b.Property<DateTime>("DataKonUmowy");

                    b.Property<DateTime>("DataUrodz");

                    b.Property<DateTime>("DataZatru");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("IdFirma");

                    b.Property<string>("Imie");

                    b.Property<string>("Kod");

                    b.Property<string>("Miasto");

                    b.Property<string>("Nazwisko");

                    b.Property<string>("NrDowoduOsob");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Telefon");

                    b.Property<string>("Ulica");

                    b.HasKey("IdPracownik");

                    b.ToTable("RegisterAdministratorViewModel");
                });

            modelBuilder.Entity("transport.Models.AccountViewModels.RegisterKierowcaViewModel", b =>
                {
                    b.Property<int>("IdPracownik")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aktywny");

                    b.Property<string>("ConfirmPassword");

                    b.Property<DateTime>("DataKarty");

                    b.Property<DateTime>("DataKonUmowy");

                    b.Property<DateTime>("DataOdczKart");

                    b.Property<DateTime>("DataUrodz");

                    b.Property<DateTime>("DataZatru");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Imie");

                    b.Property<string>("Kod");

                    b.Property<string>("Miasto");

                    b.Property<string>("Nazwisko");

                    b.Property<string>("NrDowoduOsob");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Telefon");

                    b.Property<string>("Ulica");

                    b.HasKey("IdPracownik");

                    b.ToTable("RegisterKierowcaViewModel");
                });

            modelBuilder.Entity("transport.Models.AccountViewModels.RegisterSpedytorViewModel", b =>
                {
                    b.Property<int>("IdPracownik")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aktywny");

                    b.Property<string>("ConfirmPassword");

                    b.Property<DateTime>("DataKonUmowy");

                    b.Property<DateTime>("DataUrodz");

                    b.Property<DateTime>("DataZatru");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Imie");

                    b.Property<string>("Kod");

                    b.Property<string>("Miasto");

                    b.Property<string>("Nazwisko");

                    b.Property<string>("NrDowoduOsob");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Telefon");

                    b.Property<string>("Ulica");

                    b.HasKey("IdPracownik");

                    b.ToTable("RegisterSpedytorViewModel");
                });

            modelBuilder.Entity("transport.Models.ApplicationModels.Kontrahent", b =>
                {
                    b.Property<int>("IdKontrahent")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aktywny");

                    b.Property<string>("EMail");

                    b.Property<string>("Kod");

                    b.Property<string>("Miasto");

                    b.Property<string>("NIP");

                    b.Property<string>("Nazwa");

                    b.Property<string>("Regon");

                    b.Property<string>("Telefon");

                    b.Property<string>("Typ");

                    b.Property<string>("Ulica");

                    b.Property<string>("Wlasciciel");

                    b.HasKey("IdKontrahent");

                    b.ToTable("Kontrahenci");
                });

            modelBuilder.Entity("transport.Models.ApplicationModels.Naczepa", b =>
                {
                    b.Property<int>("IdNaczepa")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aktywny");

                    b.Property<DateTime>("DataProd");

                    b.Property<DateTime>("DataPrzegl");

                    b.Property<DateTime>("DataUbez");

                    b.Property<int>("IdFirma");

                    b.Property<int>("IdPracownik");

                    b.Property<string>("Marka");

                    b.Property<string>("NrRejestr");

                    b.Property<string>("Rodzaj");

                    b.Property<string>("Wymiary");

                    b.Property<string>("Wyposazenie");

                    b.HasKey("IdNaczepa");

                    b.HasIndex("IdFirma");

                    b.HasIndex("IdPracownik");

                    b.ToTable("Naczepy");
                });

            modelBuilder.Entity("transport.Models.ApplicationModels.Ogloszenie", b =>
                {
                    b.Property<int>("IdOgloszenie")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aktywny");

                    b.Property<DateTime>("DataDodania")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("FirmaId");

                    b.Property<string>("Tresc");

                    b.Property<string>("Typ");

                    b.HasKey("IdOgloszenie");

                    b.HasIndex("FirmaId");

                    b.ToTable("Ogloszenia");
                });

            modelBuilder.Entity("transport.Models.ApplicationModels.Pojazd", b =>
                {
                    b.Property<int>("IdPojazd")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aktywny");

                    b.Property<DateTime>("DataProd");

                    b.Property<DateTime>("DataPrzegl");

                    b.Property<DateTime>("DataUbez");

                    b.Property<string>("EmisjaSpalin");

                    b.Property<int>("IdFirma");

                    b.Property<int>("IdPracownik");

                    b.Property<string>("Marka");

                    b.Property<string>("Model");

                    b.Property<string>("NrRejestr");

                    b.Property<int>("PrzebiegAktu");

                    b.Property<int>("PrzebiegSerwis");

                    b.Property<int>("PrzebiegZakup");

                    b.Property<bool>("Retarder");

                    b.Property<string>("RodzajKabiny");

                    b.Property<decimal>("SpalanieSred");

                    b.Property<DateTime>("TachoLegal");

                    b.Property<DateTime>("TachoOdczyt");

                    b.Property<string>("VIN");

                    b.HasKey("IdPojazd");

                    b.HasIndex("IdFirma");

                    b.HasIndex("IdPracownik");

                    b.ToTable("Pojazdy");
                });

            modelBuilder.Entity("transport.Models.ApplicationModels.Tankowanie", b =>
                {
                    b.Property<int>("IdTankowania")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aktywny");

                    b.Property<DateTime>("DataTank");

                    b.Property<int>("IdPojazd");

                    b.Property<int>("IdPracownik");

                    b.Property<decimal>("IloscPaliwa");

                    b.Property<int?>("PracownikId");

                    b.Property<int>("PrzebiegTankow");

                    b.Property<decimal>("WartoscPaliwa");

                    b.HasKey("IdTankowania");

                    b.HasIndex("IdPojazd");

                    b.HasIndex("PracownikId");

                    b.ToTable("Tankowania");
                });

            modelBuilder.Entity("transport.Models.ApplicationModels.Zlecenie", b =>
                {
                    b.Property<int>("IdZlecenie")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdresDosta");

                    b.Property<string>("AdresOdbioru");

                    b.Property<bool>("Aktywny");

                    b.Property<string>("DaneTowar");

                    b.Property<DateTime>("DataRozl");

                    b.Property<DateTime>("DataZalad");

                    b.Property<DateTime>("GodzRozl");

                    b.Property<DateTime>("GodzZalad");

                    b.Property<int>("IdKontrahent");

                    b.Property<int>("IdNaczepa");

                    b.Property<int>("IdPojazd");

                    b.Property<int>("IdPracownik");

                    b.Property<int?>("PracownikId");

                    b.Property<string>("Status");

                    b.Property<string>("Uwagi");

                    b.Property<string>("WagaTow");

                    b.Property<string>("Waluta");

                    b.Property<decimal>("WartoscNetto");

                    b.HasKey("IdZlecenie");

                    b.HasIndex("IdKontrahent");

                    b.HasIndex("IdNaczepa");

                    b.HasIndex("IdPojazd");

                    b.HasIndex("PracownikId");

                    b.ToTable("Zlecenia");
                });

            modelBuilder.Entity("transport.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("transport.Models.Firma", b =>
                {
                    b.Property<int>("IdFirma")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AktywnyF");

                    b.Property<string>("KodF");

                    b.Property<string>("MiastoF");

                    b.Property<string>("NIP");

                    b.Property<string>("Nazwa");

                    b.Property<DateTime>("OCP");

                    b.Property<string>("Regon");

                    b.Property<string>("TelefonF");

                    b.Property<string>("UlicaF");

                    b.Property<string>("UserId");

                    b.Property<string>("Wlasciciel");

                    b.HasKey("IdFirma");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Firmy");
                });

            modelBuilder.Entity("transport.Models.Pracownik", b =>
                {
                    b.Property<int>("PracownikId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aktywny");

                    b.Property<DateTime>("DataKarty");

                    b.Property<DateTime>("DataKonUmowy");

                    b.Property<DateTime>("DataOdczKart");

                    b.Property<DateTime>("DataUrodz");

                    b.Property<DateTime>("DataZatru");

                    b.Property<int>("FirmaId");

                    b.Property<string>("Imie");

                    b.Property<string>("Kod");

                    b.Property<string>("Miasto");

                    b.Property<string>("Nazwisko");

                    b.Property<string>("NrDowoduOsob");

                    b.Property<string>("Telefon");

                    b.Property<string>("Ulica");

                    b.Property<string>("UserId");

                    b.HasKey("PracownikId");

                    b.HasIndex("FirmaId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Pracownicy");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("transport.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("transport.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("transport.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("transport.Models.ApplicationModels.Naczepa", b =>
                {
                    b.HasOne("transport.Models.Firma", "Firma")
                        .WithMany()
                        .HasForeignKey("IdFirma")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("transport.Models.Pracownik", "Pracownik")
                        .WithMany("Naczepy")
                        .HasForeignKey("IdPracownik")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("transport.Models.ApplicationModels.Ogloszenie", b =>
                {
                    b.HasOne("transport.Models.Firma", "Firma")
                        .WithMany()
                        .HasForeignKey("FirmaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("transport.Models.ApplicationModels.Pojazd", b =>
                {
                    b.HasOne("transport.Models.Firma", "Firma")
                        .WithMany("Pojazdy")
                        .HasForeignKey("IdFirma")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("transport.Models.Pracownik", "Pracownik")
                        .WithMany("Pojazdy")
                        .HasForeignKey("IdPracownik");
                });

            modelBuilder.Entity("transport.Models.ApplicationModels.Tankowanie", b =>
                {
                    b.HasOne("transport.Models.ApplicationModels.Pojazd", "Pojazd")
                        .WithMany("Tankowanie")
                        .HasForeignKey("IdPojazd")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("transport.Models.Pracownik", "Pracownik")
                        .WithMany("Tankowania")
                        .HasForeignKey("PracownikId");
                });

            modelBuilder.Entity("transport.Models.ApplicationModels.Zlecenie", b =>
                {
                    b.HasOne("transport.Models.ApplicationModels.Kontrahent", "Kontrahent")
                        .WithMany("Zlecenia")
                        .HasForeignKey("IdKontrahent")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("transport.Models.ApplicationModels.Naczepa", "Naczepa")
                        .WithMany("Zlecenia")
                        .HasForeignKey("IdNaczepa")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("transport.Models.ApplicationModels.Pojazd", "Pojazd")
                        .WithMany("Zlecenie")
                        .HasForeignKey("IdPojazd")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("transport.Models.Pracownik", "Pracownik")
                        .WithMany("Zlecenia")
                        .HasForeignKey("PracownikId");
                });

            modelBuilder.Entity("transport.Models.Firma", b =>
                {
                    b.HasOne("transport.Models.ApplicationUser", "User")
                        .WithOne("Firma")
                        .HasForeignKey("transport.Models.Firma", "UserId");
                });

            modelBuilder.Entity("transport.Models.Pracownik", b =>
                {
                    b.HasOne("transport.Models.Firma", "Firma")
                        .WithMany("Pracownicy")
                        .HasForeignKey("FirmaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("transport.Models.ApplicationUser", "User")
                        .WithOne("Pracownik")
                        .HasForeignKey("transport.Models.Pracownik", "UserId");
                });
        }
    }
}
