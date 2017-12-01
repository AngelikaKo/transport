using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static transport.Data.ApplicationDbContext;

namespace transport.Models.ApplicationModels
{
    public class Naczepa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNaczepa { get; set; }

        public int IdPracownik { get; set; }

        public Pracownik Pracownicy { get; set; }

        public string Marka { get; set; }

        public string Rodzaj { get; set; }

        [Display(Name = "Nr rejestracyjny")]
        public string NrRejestr { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data produkcji")]
        public DateTime DataProd { get; set; }

        public string Wymiary { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data przeglądu")]
        public DateTime DataPrzegl { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data ubezpieczenia")]
        public DateTime DataUbez { get; set; }

        [Display(Name = "Wyposażenie naczepy")]
        public string Wyposazenie { get; set; }

        public bool Aktywny { get; set; }

        public List<Zlecenie> Zlecenia { get; set; }
    }
}
