using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sneat.BLL.Specifications
{
    public interface ISpecification<T> where T : class
    {
        //_dbcontext.Employees.where(P=>P.Id==id).AsNoTracking().Include(E=>E.Department)
        public Expression<Func<T,bool>> Criteria { get; set; }
        public List<Expression<Func<T,object>>> Includes { get; set; }
    }
}
