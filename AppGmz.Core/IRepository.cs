using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AppGmz.Models.DomainModels;

namespace AppGmz.Core
{
    public interface IRepository<T> where T : EntityBase
    {
        Task SaveAsync(CancellationToken token);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> CreateAsync(T obj);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindById(Guid id);
        Task<bool> RemoveById(Guid id);
    }
}