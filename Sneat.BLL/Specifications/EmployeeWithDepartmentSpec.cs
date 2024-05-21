using Sneat.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneat.BLL.Specifications
{
    public class EmployeeWithDepartmentSpec : BaseSpecifications<Employee>
    {
        public EmployeeWithDepartmentSpec():base()
        {
            Includes.Add(P => P.Department);
        }
        public EmployeeWithDepartmentSpec(int id) : base(P=>P.Id==id)
        {
            Includes.Add(P => P.Department);
        }
    }
}
