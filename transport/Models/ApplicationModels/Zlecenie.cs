using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static transport.Data.ApplicationDbContext;

namespace transport.Models.ApplicationModels
{
    public class Zlecenie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdZlecenie { get; set; }

        public int IdKontrahent { get; set; }
        public virtual Kontrahent Kontrahent { get; set; }

        public int IdPracownik { get; set; }
        [ForeignKey("IdPracownik")]
        public virtual Pracownik Pracownik { get; set; }

        public int IdPojazd { get; set; }
        public virtual Pojazd Pojazd { get; set; }

        public int IdNaczepa { get; set; }
        public virtual Naczepa Naczepa { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name ="Adres załadunku")]
        public string AdresOdbioru { get; set; }

        [Display(Name = "Adres rozładunku")]
        [DataType(DataType.MultilineText)]
        public string AdresDosta { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data załadunku")]
        public DateTime DataZalad { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Godzina załadunku")]
        public DateTime GodzZalad { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data rozładunku")]
        public DateTime DataRozl { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Godzina rozładunku")]
        public DateTime GodzRozl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Uwagi { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Dane towaru")]
        public string DaneTowar { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Waga towaru")]
        public string WagaTow { get; set; }
                
        [Display(Name = "Cena netto")]
        public decimal WartoscNetto { get; set; }

        public string Waluta { get; set; }
    
        public string Status { get; set; }

        public bool Aktywny { get; set; }

        public int IdFirma { get; set; }
        [ForeignKey("IdFirma")]
        public virtual Firma Firma { get; set; }
    }
}
