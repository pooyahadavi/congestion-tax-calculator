using CongestionTaxCalculator.Core.Interfaces.Aggregate;
using CongestionTaxCalculator.Core.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Interfaces.Repository
{
    public interface IRepository<T> where T : class, IAggregateRoot
    {
        Task<T?> GetAsync(ISpecification<T>? specification = null, CancellationToken cancellationToken = default);

        Task<List<T>> GetListAsync(ISpecification<T>? specification = null, CancellationToken cancellationToken = default);

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
