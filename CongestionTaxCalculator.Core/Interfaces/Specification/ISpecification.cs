using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Interfaces.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? WhereExpression { get; }
        List<Expression<Func<T, object>>>? IncludeExpression { get; }
        Expression<Func<T, object>>? OrderByExpression { get; }
        Expression<Func<T, object>>? OrderByDescendingExpression { get; }
    }
}
