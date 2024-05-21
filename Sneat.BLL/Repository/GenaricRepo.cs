using Microsoft.EntityFrameworkCore;
using Sneat.BLL.Interfaces;
using Sneat.BLL.Specifications;
using Sneat.DAL.Context;
using Sneat.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneat.BLL.Repository
{
    public class GenaricRepo<T> : IGenaricRep<T> where T : class
    {
        private readonly SneatdbContext _dbcontext;

        public GenaricRepo(SneatdbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task AddAsync(T item)
        {
            await _dbcontext.AddAsync(item);
        }

        public void Delete(T item)
        {
            _dbcontext.Remove(item);
        }

        public async Task<IEnumerable<T>> getAllAsync()
        {
            if(typeof(T) == typeof(Employee))
            {
            return (IEnumerable<T>) await  _dbcontext.Employees.AsNoTracking().Include(E=>E.Department).ToListAsync();
            }
          return  await _dbcontext.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            //if(typeof(T) == typeof(Employee))
            //{
            //    return await _dbcontext.Employees.Include(E => E.Department).FirstOrDefaultAsync();
            //}
         return await _dbcontext.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAllAsyncWithSpec(ISpecification<T> spec)
        {
           return await SpecificationEvalutor<T>.GetQuery(_dbcontext.Set<T>(),spec).ToListAsync();
        }
        public async Task<T> GetByIdAsyncWithSpec(ISpecification<T> spec)
        {
            return await SpecificationEvalutor<T>.GetQuery(_dbcontext.Set<T>(), spec).FirstOrDefaultAsync();
        }

        public void Update(T item)
        {
           _dbcontext.Update(item);
        }
    }
}
