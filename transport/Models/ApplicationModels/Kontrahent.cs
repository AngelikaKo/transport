using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace transport.Models.ApplicationModels
{
    public class Kontrahent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdKontrahent { get; set; }
                
        [Display(Name = "Nazwa firmy")]
        public string Nazwa { get; set; }

        public string NIP { get; set; }

        public string Regon { get; set; }

        [Display(Name = "Właściciel")]
        public string Wlasciciel { get; set; }

        public string Ulica { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "Kod pocztowy")]
        public string Kod { get; set; }

        public string Miasto { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Numer telefonu")]
        public string Telefon { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string EMail { get; set; }

        public string Typ { get; set; }

        public bool Aktywny { get; set; }

        public List<Zlecenie> Zlecenie { get; set; }

        public int IdFirma { get; set; }
        [ForeignKey("IdFirma")]
        public virtual Firma Firma { get; set; }
    }
}
