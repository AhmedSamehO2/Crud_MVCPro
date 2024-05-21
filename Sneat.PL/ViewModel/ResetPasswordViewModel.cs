using System.ComponentModel.DataAnnotations;

namespace Sneat.PL.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="Password Is Requiered")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "ConfirmPassword Is Requiered")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Password  Dosen't Match")]
        public string ConfirmPassword { get; set; }
    }
}
