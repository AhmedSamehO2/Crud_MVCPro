using System.ComponentModel.DataAnnotations;

namespace Sneat.PL.Models
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public IEnumerable<string> Roles { get; set; }

    }
}
