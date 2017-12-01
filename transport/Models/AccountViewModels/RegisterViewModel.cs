using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace transport.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFirma { get; set; }

        [Display(Name ="Nazwa firmy")]
        public string Nazwa { get; set; }

        public string NIP { get; set; }

        public string Regon { get; set; }

        [Display(Name ="Właściciel")]
        public string Wlasciciel { get; set; }

        [Display(Name = "Ulica")]
        public string UlicaF { get; set; }

        [Display(Name = "Kod")]
        [DataType(DataType.PostalCode)]
        public string KodF { get; set; }

        [Display(Name = "Miasto")]
        public string MiastoF { get; set; }

        [Display(Name = "Telefon")]
        [DataType(DataType.PhoneNumber)]
        public string TelefonF { get; set; }

        [DataType(DataType.Date)]
        public DateTime OCP { get; set; }

        public bool AktywnyF { get; set; }
        
    }
}
