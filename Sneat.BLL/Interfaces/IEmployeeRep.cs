using Sneat.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneat.BLL.Interfaces
{
    public interface IEmployeeRep : IGenaricRep<Employee>
    {
        IQueryable<Employee> GetEmpByName(string SearchName);
        

    }
}
