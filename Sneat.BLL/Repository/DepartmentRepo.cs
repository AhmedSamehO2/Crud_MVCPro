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
    public class DepartmentRepo : GenaricRepo<Department>, IDepartmentRep
    {
        public DepartmentRepo(SneatdbContext dbcontext) : base(dbcontext)
        {

        }
    }
}
