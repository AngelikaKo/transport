using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace transport.Models.AccountViewModels
{
    public class RegisterAdministratorViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPracownik { get; set; }

        [Display(Name = "Imię")]
        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public string Ulica { get; set; }

        [Display(Name = "Kod Pocztowy")]
        [DataType(DataType.PostalCode)]
        public string Kod { get; set; }

        public string Miasto { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telefon { get; set; }

        [Display(Name = "Data urodzenia")]
        [DataType(DataType.Date)]
        public DateTime DataUrodz { get; set; }

        [Display(Name = "Data zatrudnienia")]
        [DataType(DataType.Date)]
        public DateTime DataZatru { get; set; }

        [Display(Name = "Data końca umowy")]
        [DataType(DataType.Date)]
        public DateTime DataKonUmowy { get; set; }
               
        [Display(Name = "Nr Dowodu Osobistego")]
        public string NrDowoduOsob { get; set; }

        public bool Aktywny { get; set; }

        public int IdFirma { get; set; }
    }
}
