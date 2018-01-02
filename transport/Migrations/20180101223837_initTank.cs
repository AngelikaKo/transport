using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace transport.Migrations
{
    public partial class initTank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "RegisterAdministratorViewModel",
                columns: table => new
                {
                    IdPracownik = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktywny = table.Column<bool>(nullable: false),
                    ConfirmPassword = table.Column<string>(nullable: true),
                    DataKonUmowy = table.Column<DateTime>(nullable: false),
                    DataUrodz = table.Column<DateTime>(nullable: false),
                    DataZatru = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    IdFirma = table.Column<int>(nullable: false),
                    Imie = table.Column<string>(nullable: true),
                    Kod = table.Column<string>(nullable: true),
                    Miasto = table.Column<string>(nullable: true),
                    Nazwisko = table.Column<string>(nullable: true),
                    NrDowoduOsob = table.Column<string>(nullable: true),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Telefon = table.Column<string>(nullable: true),
                    Ulica = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterAdministratorViewModel", x => x.IdPracownik);
                });

            migrationBuilder.CreateTable(
                name: "RegisterKierowcaViewModel",
                columns: table => new
                {
                    IdPracownik = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktywny = table.Column<bool>(nullable: false),
                    ConfirmPassword = table.Column<string>(nullable: true),
                    DataKarty = table.Column<DateTime>(nullable: false),
                    DataKonUmowy = table.Column<DateTime>(nullable: false),
                    DataOdczKart = table.Column<DateTime>(nullable: false),
                    DataUrodz = table.Column<DateTime>(nullable: false),
                    DataZatru = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Imie = table.Column<string>(nullable: true),
                    Kod = table.Column<string>(nullable: true),
                    Miasto = table.Column<string>(nullable: true),
                    Nazwisko = table.Column<string>(nullable: true),
                    NrDowoduOsob = table.Column<string>(nullable: true),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Telefon = table.Column<string>(nullable: true),
                    Ulica = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterKierowcaViewModel", x => x.IdPracownik);
                });

            migrationBuilder.CreateTable(
                name: "RegisterSpedytorViewModel",
                columns: table => new
                {
                    IdPracownik = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktywny = table.Column<bool>(nullable: false),
                    ConfirmPassword = table.Column<string>(nullable: true),
                    DataKonUmowy = table.Column<DateTime>(nullable: false),
                    DataUrodz = table.Column<DateTime>(nullable: false),
                    DataZatru = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Imie = table.Column<string>(nullable: true),
                    Kod = table.Column<string>(nullable: true),
                    Miasto = table.Column<string>(nullable: true),
                    Nazwisko = table.Column<string>(nullable: true),
                    NrDowoduOsob = table.Column<string>(nullable: true),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Telefon = table.Column<string>(nullable: true),
                    Ulica = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterSpedytorViewModel", x => x.IdPracownik);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Firmy",
                columns: table => new
                {
                    IdFirma = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AktywnyF = table.Column<bool>(nullable: false),
                    KodF = table.Column<string>(nullable: true),
                    MiastoF = table.Column<string>(nullable: true),
                    NIP = table.Column<string>(nullable: true),
                    Nazwa = table.Column<string>(nullable: true),
                    OCP = table.Column<DateTime>(nullable: false),
                    Regon = table.Column<string>(nullable: true),
                    TelefonF = table.Column<string>(nullable: true),
                    UlicaF = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Wlasciciel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmy", x => x.IdFirma);
                    table.ForeignKey(
                        name: "FK_Firmy_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kontrahenci",
                columns: table => new
                {
                    IdKontrahent = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktywny = table.Column<bool>(nullable: false),
                    EMail = table.Column<string>(nullable: true),
                    IdFirma = table.Column<int>(nullable: false),
                    Kod = table.Column<string>(nullable: true),
                    Miasto = table.Column<string>(nullable: true),
                    NIP = table.Column<string>(nullable: true),
                    Nazwa = table.Column<string>(nullable: true),
                    Regon = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Typ = table.Column<string>(nullable: true),
                    Ulica = table.Column<string>(nullable: true),
                    Wlasciciel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontrahenci", x => x.IdKontrahent);
                    table.ForeignKey(
                        name: "FK_Kontrahenci_Firmy_IdFirma",
                        column: x => x.IdFirma,
                        principalTable: "Firmy",
                        principalColumn: "IdFirma",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ogloszenia",
                columns: table => new
                {
                    IdOgloszenie = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktywny = table.Column<bool>(nullable: false),
                    DataDodania = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    FirmaId = table.Column<int>(nullable: false),
                    Tresc = table.Column<string>(nullable: true),
                    Typ = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogloszenia", x => x.IdOgloszenie);
                    table.ForeignKey(
                        name: "FK_Ogloszenia_Firmy_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firmy",
                        principalColumn: "IdFirma",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    PracownikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktywny = table.Column<bool>(nullable: false),
                    DataKarty = table.Column<DateTime>(nullable: false),
                    DataKonUmowy = table.Column<DateTime>(nullable: false),
                    DataOdczKart = table.Column<DateTime>(nullable: false),
                    DataUrodz = table.Column<DateTime>(nullable: false),
                    DataZatru = table.Column<DateTime>(nullable: false),
                    FirmaId = table.Column<int>(nullable: false),
                    Imie = table.Column<string>(nullable: true),
                    Kod = table.Column<string>(nullable: true),
                    Miasto = table.Column<string>(nullable: true),
                    Nazwisko = table.Column<string>(nullable: true),
                    NrDowoduOsob = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Ulica = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.PracownikId);
                    table.ForeignKey(
                        name: "FK_Pracownicy_Firmy_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firmy",
                        principalColumn: "IdFirma",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pracownicy_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Naczepy",
                columns: table => new
                {
                    IdNaczepa = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktywny = table.Column<bool>(nullable: false),
                    DataProd = table.Column<DateTime>(nullable: false),
                    DataPrzegl = table.Column<DateTime>(nullable: false),
                    DataUbez = table.Column<DateTime>(nullable: false),
                    IdFirma = table.Column<int>(nullable: false),
                    IdPracownik = table.Column<int>(nullable: false),
                    Marka = table.Column<string>(nullable: true),
                    NrRejestr = table.Column<string>(nullable: true),
                    Rodzaj = table.Column<string>(nullable: true),
                    Wymiary = table.Column<string>(nullable: true),
                    Wyposazenie = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Naczepy", x => x.IdNaczepa);
                    table.ForeignKey(
                        name: "FK_Naczepy_Firmy_IdFirma",
                        column: x => x.IdFirma,
                        principalTable: "Firmy",
                        principalColumn: "IdFirma",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Naczepy_Pracownicy_IdPracownik",
                        column: x => x.IdPracownik,
                        principalTable: "Pracownicy",
                        principalColumn: "PracownikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pojazdy",
                columns: table => new
                {
                    IdPojazd = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktywny = table.Column<bool>(nullable: false),
                    DataProd = table.Column<DateTime>(nullable: false),
                    DataPrzegl = table.Column<DateTime>(nullable: false),
                    DataUbez = table.Column<DateTime>(nullable: false),
                    EmisjaSpalin = table.Column<string>(nullable: true),
                    IdFirma = table.Column<int>(nullable: false),
                    IdPracownik = table.Column<int>(nullable: false),
                    Marka = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    NrRejestr = table.Column<string>(nullable: true),
                    PrzebiegAktu = table.Column<int>(nullable: false),
                    PrzebiegSerwis = table.Column<int>(nullable: false),
                    PrzebiegZakup = table.Column<int>(nullable: false),
                    Retarder = table.Column<bool>(nullable: false),
                    RodzajKabiny = table.Column<string>(nullable: true),
                    SpalanieSred = table.Column<decimal>(nullable: false),
                    TachoLegal = table.Column<DateTime>(nullable: false),
                    TachoOdczyt = table.Column<DateTime>(nullable: false),
                    VIN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pojazdy", x => x.IdPojazd);
                    table.ForeignKey(
                        name: "FK_Pojazdy_Firmy_IdFirma",
                        column: x => x.IdFirma,
                        principalTable: "Firmy",
                        principalColumn: "IdFirma",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pojazdy_Pracownicy_IdPracownik",
                        column: x => x.IdPracownik,
                        principalTable: "Pracownicy",
                        principalColumn: "PracownikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tankowania",
                columns: table => new
                {
                    IdTankowania = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktywny = table.Column<bool>(nullable: false),
                    DataTank = table.Column<DateTime>(nullable: false),
                    IdFirma = table.Column<int>(nullable: false),
                    IdPojazd = table.Column<int>(nullable: false),
                    IdPracownik = table.Column<int>(nullable: false),
                    IloscPaliwa = table.Column<decimal>(nullable: false),
                    PrzebiegTankow = table.Column<int>(nullable: false),
                    WartoscPaliwa = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tankowania", x => x.IdTankowania);
                    table.ForeignKey(
                        name: "FK_Tankowania_Firmy_IdFirma",
                        column: x => x.IdFirma,
                        principalTable: "Firmy",
                        principalColumn: "IdFirma",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tankowania_Pojazdy_IdPojazd",
                        column: x => x.IdPojazd,
                        principalTable: "Pojazdy",
                        principalColumn: "IdPojazd",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tankowania_Pracownicy_IdPracownik",
                        column: x => x.IdPracownik,
                        principalTable: "Pracownicy",
                        principalColumn: "PracownikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zlecenia",
                columns: table => new
                {
                    IdZlecenie = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdresDosta = table.Column<string>(nullable: true),
                    AdresOdbioru = table.Column<string>(nullable: true),
                    Aktywny = table.Column<bool>(nullable: false),
                    DaneTowar = table.Column<string>(nullable: true),
                    DataRozl = table.Column<DateTime>(nullable: false),
                    DataZalad = table.Column<DateTime>(nullable: false),
                    GodzRozl = table.Column<DateTime>(nullable: false),
                    GodzZalad = table.Column<DateTime>(nullable: false),
                    IdFirma = table.Column<int>(nullable: false),
                    IdKontrahent = table.Column<int>(nullable: false),
                    IdNaczepa = table.Column<int>(nullable: false),
                    IdPojazd = table.Column<int>(nullable: false),
                    IdPracownik = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Uwagi = table.Column<string>(nullable: true),
                    WagaTow = table.Column<string>(nullable: true),
                    Waluta = table.Column<string>(nullable: true),
                    WartoscNetto = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zlecenia", x => x.IdZlecenie);
                    table.ForeignKey(
                        name: "FK_Zlecenia_Firmy_IdFirma",
                        column: x => x.IdFirma,
                        principalTable: "Firmy",
                        principalColumn: "IdFirma",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zlecenia_Kontrahenci_IdKontrahent",
                        column: x => x.IdKontrahent,
                        principalTable: "Kontrahenci",
                        principalColumn: "IdKontrahent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zlecenia_Naczepy_IdNaczepa",
                        column: x => x.IdNaczepa,
                        principalTable: "Naczepy",
                        principalColumn: "IdNaczepa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zlecenia_Pojazdy_IdPojazd",
                        column: x => x.IdPojazd,
                        principalTable: "Pojazdy",
                        principalColumn: "IdPojazd",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zlecenia_Pracownicy_IdPracownik",
                        column: x => x.IdPracownik,
                        principalTable: "Pracownicy",
                        principalColumn: "PracownikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Kontrahenci_IdFirma",
                table: "Kontrahenci",
                column: "IdFirma");

            migrationBuilder.CreateIndex(
                name: "IX_Naczepy_IdFirma",
                table: "Naczepy",
                column: "IdFirma");

            migrationBuilder.CreateIndex(
                name: "IX_Naczepy_IdPracownik",
                table: "Naczepy",
                column: "IdPracownik");

            migrationBuilder.CreateIndex(
                name: "IX_Ogloszenia_FirmaId",
                table: "Ogloszenia",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pojazdy_IdFirma",
                table: "Pojazdy",
                column: "IdFirma");

            migrationBuilder.CreateIndex(
                name: "IX_Pojazdy_IdPracownik",
                table: "Pojazdy",
                column: "IdPracownik");

            migrationBuilder.CreateIndex(
                name: "IX_Tankowania_IdFirma",
                table: "Tankowania",
                column: "IdFirma");

            migrationBuilder.CreateIndex(
                name: "IX_Tankowania_IdPojazd",
                table: "Tankowania",
                column: "IdPojazd");

            migrationBuilder.CreateIndex(
                name: "IX_Tankowania_IdPracownik",
                table: "Tankowania",
                column: "IdPracownik");

            migrationBuilder.CreateIndex(
                name: "IX_Zlecenia_IdFirma",
                table: "Zlecenia",
                column: "IdFirma");

            migrationBuilder.CreateIndex(
                name: "IX_Zlecenia_IdKontrahent",
                table: "Zlecenia",
                column: "IdKontrahent");

            migrationBuilder.CreateIndex(
                name: "IX_Zlecenia_IdNaczepa",
                table: "Zlecenia",
                column: "IdNaczepa");

            migrationBuilder.CreateIndex(
                name: "IX_Zlecenia_IdPojazd",
                table: "Zlecenia",
                column: "IdPojazd");

            migrationBuilder.CreateIndex(
                name: "IX_Zlecenia_IdPracownik",
                table: "Zlecenia",
                column: "IdPracownik");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Firmy_UserId",
                table: "Firmy",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_FirmaId",
                table: "Pracownicy",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_UserId",
                table: "Pracownicy",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "RegisterAdministratorViewModel");

            migrationBuilder.DropTable(
                name: "RegisterKierowcaViewModel");

            migrationBuilder.DropTable(
                name: "RegisterSpedytorViewModel");

            migrationBuilder.DropTable(
                name: "Ogloszenia");

            migrationBuilder.DropTable(
                name: "Tankowania");

            migrationBuilder.DropTable(
                name: "Zlecenia");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Kontrahenci");

            migrationBuilder.DropTable(
                name: "Naczepy");

            migrationBuilder.DropTable(
                name: "Pojazdy");

            migrationBuilder.DropTable(
                name: "Pracownicy");

            migrationBuilder.DropTable(
                name: "Firmy");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
