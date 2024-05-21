using System.ComponentModel.DataAnnotations;

namespace Sneat.PL.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email IS Required")]
        [EmailAddress(ErrorMessage = "Email InValid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RemeberMe { get; set; }
    }
}
