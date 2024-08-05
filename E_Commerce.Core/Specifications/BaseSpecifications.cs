using E_Commerce.Core.Models;
using System.Linq.Expressions;

namespace E_Commerce.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseModel
    {
        //Make Constructor That intialize where(Criteria)
        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }
        public BaseSpecifications()
        {

        }
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; protected set; }

        public Expression<Func<T, object>> OrderByDesc { get; protected set; }

        public int Take { get; protected set; }

        public int Skip { get; protected set; }

        public bool IsPaginated { get; protected set; }

        protected void ApplyPagination(int PageSize, int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;

        }
    }
}
