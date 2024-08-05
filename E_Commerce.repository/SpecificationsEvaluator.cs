using E_Commerce.Core.Models;
using E_Commerce.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.repository
{
    public static class SpecificationsEvaluator<T> where T : BaseModel
    {
        // Function To Build Dynamic  Query 
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> specifications)
        {
            var Query = inputQuery;

            if (specifications.Criteria is not null)
            {
                Query = Query.Where(specifications.Criteria);
            }
            if (specifications.OrderBy is not null)
            {
                Query = Query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDesc is not null)
            {
                Query = Query.OrderByDescending(specifications.OrderByDesc);
            }

            if (specifications.IsPaginated)
            {
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);
            }
            Query = specifications.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

            return Query;
        }
    }
}
