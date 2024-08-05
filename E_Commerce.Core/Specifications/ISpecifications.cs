using E_Commerce.Core.Models;
using System.Linq.Expressions;

namespace E_Commerce.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseModel
    {
        //Make Signature For Where
        public Expression<Func<T, bool>> Criteria { get; set; }

        //Make Signature For Include
        public List<Expression<Func<T, object>>> Includes { get; set; }
        //Make Signature For OrderBy, 
        public Expression<Func<T, object>> OrderBy { get; }
        //OrderByDesc
        public Expression<Func<T, object>> OrderByDesc { get; }
        public int Take { get; }
        public int Skip { get; }
        public bool IsPaginated { get; }


    }
}
