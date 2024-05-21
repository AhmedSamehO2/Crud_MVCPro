using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneat.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IDepartmentRep DepartmentRep { get; set; }
        public IEmployeeRep EmployeeRep { get; set; }

        Task<int> CompleteAsync();
    }
}
