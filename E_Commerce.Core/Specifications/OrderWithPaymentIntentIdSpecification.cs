using E_Commerce.Core.Models.Order_Aggregation;
using System.Linq.Expressions;

namespace E_Commerce.Core.Specifications
{
    public class OrderWithPaymentIntentIdSpecification : BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentIdSpecification(string paymentIntentId) : base(
            order=> order.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
