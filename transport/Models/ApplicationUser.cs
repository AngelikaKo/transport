using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static transport.Data.ApplicationDbContext;
using transport.Models.ApplicationModels;

namespace transport.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual Firma Firma { get; set; }
        public virtual Pracownik Pracownik {get; set; }
    }

    public class Pracownik
    {

        public int PracownikId { get; set; }

        //public int FirmaId { get; set; }
        //public Firma Firma { get; set; }

        [Display(Name = "Imię")]
        public string Imie { get; set; }

        public string Nazwisko { get; set; }
                
        public string GetFullName
        { get
            {
                string fullname = String.Format("{0} {1}", Imie, Nazwisko);
                return fullname;
            }
        }

        public string Ulica { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string Kod { get; set; }

        public string Miasto { get; set; }

        [Display(Name = "Nr telefonu")]
        public string Telefon { get; set; }

        [Display(Name = "Data urodzenia")]
        public DateTime DataUrodz { get; set; }

        [Display(Name = "Data zatrudnienia")]
        public DateTime DataZatru { get; set; }

        [Display(Name = "Data końca umowy")]
        public DateTime DataKonUmowy { get; set; }

        [Display(Name = "Data ważności karty kierowcy")]
        public DateTime DataKarty { get; set; }

        [Display(Name = "Data ostatniego odczytu karty kierowcy")]
        public DateTime DataOdczKart { get; set; }

        [Display(Name = "Nr dowodu osobistego")]
        public string NrDowoduOsob { get; set; }

        public bool Aktywny { get; set; }

        public virtual List<Pojazd> Pojazdy { get; set; }

        public virtual List<Tankowanie> Tankowania { get; set; }

        public virtual List<Zlecenie> Zlecenia { get; set; }

        public virtual List<Naczepa> Naczepy { get; set; }
                
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public int FirmaId { get; set; }
        [ForeignKey("FirmaId")]
        public virtual Firma Firma { get; set; }

    }

    public class Firma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFirma { get; set; }

        [Display(Name = "Nazwa firmy")]
        public string Nazwa { get; set; }

        public string NIP { get; set; }

        public string Regon { get; set; }

        [Display(Name = "Właściciel firmy")]
        public string Wlasciciel { get; set; }

        [Display(Name = "Ulica")]
        public string UlicaF { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string KodF { get; set; }

        [Display(Name = "Miasto")]
        public string MiastoF { get; set; }

        [Display(Name = "Nr telefonu")]
        public string TelefonF { get; set; }

        [Display(Name = "Data OCP")]
        public DateTime OCP { get; set; }

        public bool AktywnyF { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Pracownik> Pracownicy { get; set; }
        public virtual ICollection<Pojazd> Pojazdy { get; set; }
        public virtual ICollection<Kontrahent> Kontrahenci { get; set; }

    }
}
