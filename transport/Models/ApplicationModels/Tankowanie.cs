using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace transport.Models.ApplicationModels
{
    public class Tankowanie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTankowania { get; set; }

        public int IdPracownik { get; set; }
        [ForeignKey("IdPracownik")]
        public virtual Pracownik Pracownik { get; set; }

        public int IdPojazd { get; set; }
        public virtual Pojazd Pojazd { get; set; }

        [Display(Name = "Przebieg podczas tankowania")]
        public int PrzebiegTankow { get; set; }

        [Display(Name = "Ilość paliwa zatankowanego")]
        public decimal IloscPaliwa { get; set; }

        [Display(Name = "Wartość zatankowanego paliwa")]
        public decimal WartoscPaliwa { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data tankowania")]
        public DateTime DataTank { get; set; }

        public bool Aktywny { get; set; }

        public int IdFirma { get; set; }
        [ForeignKey("IdFirma")]
        public virtual Firma Firma { get; set; }
    }
}
