using CongestionTaxCalculator.Core.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Specification
{
    public class SpecificationBase<T> : ISpecification<T>
    {
        public SpecificationBase(Expression<Func<T, bool>>? whereExpression = null, List<Expression<Func<T, object>>>? includeExpression = null, Expression<Func<T, object>>? orderByExpression = null, Expression<Func<T, object>>? orderByDescendingExpression = null)
        {
            WhereExpression = whereExpression;
            IncludeExpression = includeExpression;
            OrderByExpression = orderByExpression;
            OrderByDescendingExpression = orderByDescendingExpression;
        }

        public Expression<Func<T, bool>>? WhereExpression { get; private set; }
        public List<Expression<Func<T, object>>>? IncludeExpression { get; private set; }
        public Expression<Func<T, object>>? OrderByExpression { get; private set; }
        public Expression<Func<T, object>>? OrderByDescendingExpression { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            IncludeExpression ??= [];
            IncludeExpression.Add(includeExpression);
        }

        protected void SetWhere(Expression<Func<T, bool>> whereExpression)
        {
            WhereExpression = whereExpression;
        }

        protected void SetOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderByExpression = orderByExpression;
        }

        protected void SetOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescendingExpression = orderByDescExpression;
        }
    }
}
