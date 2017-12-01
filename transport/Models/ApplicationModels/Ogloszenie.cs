using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace transport.Models.ApplicationModels
{
    public class Ogloszenie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOgloszenie { get; set; }

        public int FirmaId { get; set; }

        public Firma Firmy { get; set; }

        [Display(Name = "Data dodania")]
        [DataType(DataType.DateTime)]
        //  [DefaultValue(typeof(DateTime),DateTime.Now.ToString("yyyy-MM-dd"))]
        public DateTime DataDodania { get; set; }

        [Display(Name = "Typ ogłoszenia")]
        public string Typ { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Treść")]
        public string Tresc { get; set; }

        public bool Aktywny { get; set; }
    }
}
