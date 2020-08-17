using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Seed
{
    public static class SeedData
    {
        private static readonly Dictionary<int, Order> _orders = new Dictionary<int, Order>();
        private static readonly Dictionary<int, Customer> _customers = new Dictionary<int, Customer>();
        public static async Task SeedDataAsync(ITemplateDbContext context)
        {
            if (context.Customers.Any())
            {
                return;
            }
            await SeedValuesAsync(context);
            await SeedOrdersAsync(context);
            await SeedCustomersAsync(context);
            await context.SaveChangesAsync(CancellationToken.None);
        }

        public static async Task SeedDefaultUsersAsync(UserManager<User> userManager)
        {
            if (userManager.Users.Any())
            {
                return;
            }
            var defaultUser = new User
            {
                Id = "abc",
                DisplayName = "Administrator",
                UserName = "administrator@localhost",
                Email = "administrator@localhost.com"
            };

            await userManager.CreateAsync(defaultUser, "Passw0rd!");


        }
        private static async Task SeedValuesAsync(ITemplateDbContext context)
        {
            var values = new List<Value>
            {
                new Value (1),
                new Value (2),
                new Value (3),
            };

            await context.Values.AddRangeAsync(values);

        }
        private static async Task SeedOrdersAsync(ITemplateDbContext context)
        {
            _orders.Add(1, new Order
            {
                OrderName = "Order1",
                Priority = PriorityLevel.Low,
                //IsActive = true,
                //Created = DateTime.Now,
                //CreatedBy = "pedrodiogo4470@hotmail.com",
                //LastModified = null,
                //LastModifiedBy = "pedrodiogo4470@hotmail.com"
            }
            );
            _orders.Add(2, new Order
            {
                OrderName = "Order2",
                Priority = PriorityLevel.Medium,
                //IsActive = true,
                //context.Entry(Order).Property("IsActive").CurrentValue = true,
                //context.Entry(Order).Property("Created").CurrentValue = DateTime.Now,
                //CreatedBy = "pedrodiogo4470@hotmail.com",
                //LastModified = null,
                //LastModifiedBy = "pedrodiogo4470@hotmail.com"
            }
            );
            _orders.Add(3, new Order
            {
                OrderName = "Order3",
                Priority = PriorityLevel.High,
                //IsActive = true,
                //Created = DateTime.Now,
                //CreatedBy = "pedrodiogo4470@hotmail.com",
                //LastModified = null,
                //LastModifiedBy = "pedrodiogo4470@hotmail.com"
            }
            );

            await context.Orders.AddRangeAsync(_orders.Values.AsEnumerable());
        }
        private static async Task SeedCustomersAsync(ITemplateDbContext context)
        {
            _customers.Add(1, new Customer
            {
                Address = "Avda. de la Constitución 2222",
                CompanyName = "Ana Trujillo Emparedados y helados",
                ContactName = "Ana Trujillo",
                Country = "Mexico",
                PostalCode = "05021",
                //IsActive = true,
                //Created = DateTime.Now,
                //CreatedBy = "pedrodiogo4470@hotmail.com",
                //LastModified = null,
                //LastModifiedBy = "pedrodiogo4470@hotmail.com"
            }.AddOrders(_orders[1])
            );

            _customers.Add(2, new Customer
            {
                Address = "Obere Str. 57",
                CompanyName = "Alfreds Futterkiste",
                ContactName = "Maria Anders",
                Country = "Germany",
                PostalCode = "12209",
                //IsActive = true,
                //Created = DateTime.Now,
                //CreatedBy = "pedrodiogo4470@hotmail.com",
                //LastModified = null,
                //LastModifiedBy = "pedrodiogo4470@hotmail.com"
            }.AddOrders(_orders[2], _orders[3])
            );
            _customers.Add(3, new Customer
            {
                Address = "Mataderos  2312",
                CompanyName = "Antonio Moreno Taquería",
                ContactName = "Antonio Moreno",
                Country = "Mexico",
                PostalCode = "05023",
                //IsActive = true,
                //Created = DateTime.Now,
                //CreatedBy = "pedrodiogo4470@hotmail.com",
                //LastModified = null,
                //LastModifiedBy = "pedrodiogo4470@hotmail.com"
            }
            );


            await context.Customers.AddRangeAsync(_customers.Values.AsEnumerable());
        }
    }

}
