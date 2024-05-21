using System.ComponentModel.DataAnnotations;

namespace Sneat.PL.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="FirstName Is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName Is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email IS Required")]
        [EmailAddress(ErrorMessage = "Email InValid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Dosen'nt Match")]
        public string ConfirmPassword { get; set; }
        public bool IsActive { get; set; }
    }
}
