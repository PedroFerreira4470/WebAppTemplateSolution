using Domain.Entities;

namespace Infrastructure.Persistance.Seed
{
    internal static class SeedExtensions
    {
        public static Customer AddOrders(this Customer customer, params Order[] orders)
        {
            foreach (var order in orders)
            {
                customer.Orders.Add(order);
            }
            return customer;
        }
    }
}
