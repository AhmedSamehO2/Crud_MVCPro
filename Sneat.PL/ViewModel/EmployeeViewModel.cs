using Sneat.DAL.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sneat.PL.ViewModel
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(20, ErrorMessage = "Max name is 20")]
        [MinLength(5, ErrorMessage = "Min name is 5")]
        public string Name { get; set; }
        [Range(20, 40, ErrorMessage = "Age Must Be is range for 20 to 40")]
        public int Age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{3,10}$", ErrorMessage = "Ex. 12-street-Haram")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string? PhotoUrl { get; set; }
        public string? CvUrl { get; set; }
        public IFormFile? Photo { get; set; }
        public IFormFile? Cv { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        [InverseProperty("Employees")]
        public DepartmentViewModel? Department { get; set; }
    }
}
