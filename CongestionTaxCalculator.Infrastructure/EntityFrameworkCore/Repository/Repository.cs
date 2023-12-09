using CongestionTaxCalculator.Core.Interfaces.Aggregate;
using CongestionTaxCalculator.Core.Interfaces.Repository;
using CongestionTaxCalculator.Core.Interfaces.Specification;
using CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Context;
using CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Specification;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T?> GetAsync(ISpecification<T>? specification = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Set<T>().AsQueryable();
            if (specification is not null)
            {
                query = query.ApplySpecification(specification);
            }
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<List<T>> GetListAsync(ISpecification<T>? specification = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Set<T>().AsQueryable();
            if (specification is not null)
            {
                query = query.ApplySpecification(specification);
            }
            return await query.ToListAsync(cancellationToken);
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            _context.Set<T>().Add(entity);
            await SaveChangesAsync(cancellationToken);
            return entity;
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            _context.Set<T>().Remove(entity);
            await SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            _context.Set<T>().RemoveRange(entities);
            await SaveChangesAsync(cancellationToken);
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
