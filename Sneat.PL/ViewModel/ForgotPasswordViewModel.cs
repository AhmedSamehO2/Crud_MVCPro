using System.ComponentModel.DataAnnotations;

namespace Sneat.PL.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Invalid Mail")]
        public string Email { get; set; }
    }
}
