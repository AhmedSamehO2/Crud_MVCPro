using Microsoft.EntityFrameworkCore;
using Sneat.BLL.Interfaces;
using Sneat.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneat.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork , IDisposable
    {
        private readonly SneatdbContext _dbcontext;

        public IDepartmentRep DepartmentRep { get ; set; }
        public IEmployeeRep EmployeeRep { get ; set ; }
        public UnitOfWork(SneatdbContext dbcontext)
        {
            EmployeeRep = new EmployeeRepo(dbcontext);
            DepartmentRep = new DepartmentRepo(dbcontext);
            _dbcontext = dbcontext;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbcontext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
