using Sneat.BLL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneat.BLL.Interfaces
{
    public interface IGenaricRep<T> where T : class
    {
       Task <IEnumerable<T>> getAllAsync();
        Task AddAsync(T item);
        Task<T> GetByIdAsync(int id);
        void Delete(T item);
        void Update(T item);
        Task<IEnumerable<T>> GetAllAsyncWithSpec(ISpecification<T> spec);
        Task<T> GetByIdAsyncWithSpec(ISpecification<T> spec);
    }
}
