using E_Commerce.Core.Models.Order_Aggregation;

namespace E_Commerce.Core.Specifications
{
    public class OrderSpecification : BaseSpecifications<Order>
    {
        public OrderSpecification(string email) : base(order => order.BuyerEmail == email)
        {
            Includes.Add(order => order.DeliveryMethod);
            Includes.Add(order => order.Items);
            OrderByDesc = o => o.OrderDate;

        }

        public OrderSpecification(int id, string email)
            : base(order => order.BuyerEmail == email && order.Id == id)
        {
            Includes.Add(order => order.DeliveryMethod);
            Includes.Add(order => order.Items);
        }
    }
}
