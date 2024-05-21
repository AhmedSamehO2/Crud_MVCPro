using System.ComponentModel.DataAnnotations;

namespace Sneat.PL.Models
{
    public class AddroleViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Name Is Required !")]
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
