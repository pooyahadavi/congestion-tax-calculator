using CongestionTaxCalculator.Core.Interfaces.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Specification
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> ApplySpecification<T>(this IQueryable<T> inputQuery, ISpecification<T> specification) where T : class
        {
            var query = inputQuery;
            if (specification.WhereExpression is not null)
            {
                query = query.Where(specification.WhereExpression);
            }
            if (specification.OrderByExpression is not null)
            {
                query = query.OrderBy(specification.OrderByExpression);
            }
            if (specification.OrderByDescendingExpression is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescendingExpression);
            }
            if (specification.IncludeExpression is not null)
            {
                query = specification.IncludeExpression.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }
    }
}
