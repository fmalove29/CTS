using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using CrudAsp.Models;

namespace CrudAsp.Repository
{
    public interface IRepository<T> where T: BaseEntity
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public  Task<T> GetByIdAsync(Guid Id);
        public  Task<T> AddAsync(T entity);
        public  Task<T> UpdateAsync(T entity);
        public Task<T> DeleteAsync(Guid Id);
        public Task<DbSet<T>> GetDbSet();


    }
}