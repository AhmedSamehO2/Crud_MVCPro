using Microsoft.EntityFrameworkCore;
using Sneat.BLL.Interfaces;
using Sneat.DAL.Context;
using Sneat.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneat.BLL.Repository
{
    internal class EmployeeRepo : GenaricRepo<Employee>, IEmployeeRep
    {
        private readonly SneatdbContext _dbcontext;

        public EmployeeRepo(SneatdbContext dbcontext) : base(dbcontext)
        {
           _dbcontext = dbcontext;
        }
        public IQueryable<Employee> GetEmpByName(string serchName)
        =>
            _dbcontext.Employees.Where(E => E.Name.ToLower().Contains(serchName.ToLower())).Include(E => E.Department);

        
    }
}
