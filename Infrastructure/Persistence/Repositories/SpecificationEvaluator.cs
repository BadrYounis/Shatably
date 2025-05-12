using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(
            IQueryable<T> inputQuery, Specifications<T> specifications) where T : class
        {
            var query = inputQuery;  //_dbContext.Set<Product>()

            if (specifications.Criteria is not null)    // p => p.Id == 1
                query = query.Where(specifications.Criteria);   // _dbContext.Set<Product>().Where(p => p.Id == 1)

            query = specifications.IncludeExpressions.Aggregate(
                query,
                (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            if (specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);
            else if (specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            if (specifications.IsPaginated)
                query = query.Skip(specifications.Skip).Take(specifications.Take);

            return query;
        }
    }
}

// _dbContext.Set<T>() => InputQuery(IQueryable)
// _dbContext.Set<T>().Where(Specification.Criteria).Include(exp)