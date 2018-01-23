using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static transport.Data.ApplicationDbContext;

namespace transport.Models.ApplicationModels
{
    public class Pojazd
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPojazd { get; set; }  

        public int IdPracownik { get; set; }
        [ForeignKey("IdPracownik")]
        public virtual Pracownik Pracownik { get; set; }

        public string Marka { get; set; }

        public string Model { get; set; }
               
        public string VIN { get; set; }

        [Display(Name = "Nr rejestracyjny")]
        public string NrRejestr { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data produkcji")]
        public DateTime DataProd { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ostatni odczyt tachografu")]
        public DateTime TachoOdczyt { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Legalizacja tachografu")]
        public DateTime TachoLegal { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data przeglądu")]
        public DateTime DataPrzegl { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data ubezpieczenia")]
        public DateTime DataUbez { get; set; }

        [Display(Name = "Średnie spalanie")]
        public decimal SpalanieSred { get; set; }

        [Display(Name = "Przebieg przy zakupie pojazdu")]
        public int PrzebiegZakup { get; set; }

        [Display(Name = "Aktualny przebieg")]
        public int PrzebiegAktu { get; set; }

        [Display(Name = "Przebieg podczas ostatniego serwisu")]
        public int PrzebiegSerwis { get; set; }

        [Display(Name = "Rodzaj kabiny")]
        public string RodzajKabiny { get; set; }

        [Display(Name = "Emisja spalin")]
        public string EmisjaSpalin { get; set; }

        public bool Retarder { get; set; }

        public bool Aktywny { get; set; }

        public List<Tankowanie> Tankowania { get; set; }

        public List<Zlecenie> Zlecenie { get; set; }

        public int IdFirma { get; set; }
        [ForeignKey("IdFirma")]
        public virtual Firma Firma { get; set; }


    }
}
