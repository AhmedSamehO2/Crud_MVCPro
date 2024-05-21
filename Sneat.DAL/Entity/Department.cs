using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneat.DAL.Entity
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string DateOfCreation { get; set; }
        [InverseProperty("Department")]
        public ICollection<Employee> Employee { get; set; } = new HashSet<Employee>();

    }
}
