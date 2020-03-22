using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Seed
{
    public static class SeedData
    {
        private static readonly Dictionary<int, User> users = new Dictionary<int, User>();
        private static readonly Dictionary<int, Order> orders = new Dictionary<int, Order>();
        private static readonly Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
        public static async Task SeedDataAsync(TemplateDbContext context, UserManager<User> userManager)
        {
            if (context.Customers.Any()) return;

            await SeedUsersAsync(userManager);
            await SeedValuesAsync(context);
            await SeedOrdersAsync(context);
            await SeedCustomersAsync(context);
            await context.SaveChangesAsync();
        }


        private static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            users.Add(1, new User
            {
                Id = "a",
                DisplayName = "Bob",
                UserName = "bob",
                Email = "bob@test.com"
            }
            );
            users.Add(2, new User
            {
                Id = "b",
                DisplayName = "Jane",
                UserName = "jane",
                Email = "jane@test.com"
            }
            );
            users.Add(3, new User
            {
                Id = "c",
                DisplayName = "Pedro Ferreira",
                UserName = "PedroF",
                Email = "pedrodiogo4470@hotmail.com"
            }
            );

            foreach (var user in users)
            {
                await userManager.CreateAsync(user.Value, "Passw0rd!");
            }
        }
        private static async Task SeedValuesAsync(TemplateDbContext context)
        {
            var values = new List<Value>
            {
                new Value { ValueNumber = 1,Created=DateTime.Now },
                new Value { ValueNumber = 2,Created=DateTime.Now},
                new Value { ValueNumber = 3,Created=DateTime.Now },
            };

            await context.Values.AddRangeAsync(values);

        }
        private static async Task SeedOrdersAsync(TemplateDbContext context)
        {
            orders.Add(1, new Order
            {
                OrderName = "Order1",
                Priority = PriorityLevel.Low,
                IsActive = true,
                Created = DateTime.Now,
                CreatedBy = "pedrodiogo4470@hotmail.com",
                LastModified = null,
                LastModifiedBy = "pedrodiogo4470@hotmail.com"
            }
            );
            orders.Add(2, new Order
            {
                OrderName = "Order2",
                Priority = PriorityLevel.Medium,
                IsActive = true,
                Created = DateTime.Now,
                CreatedBy = "pedrodiogo4470@hotmail.com",
                LastModified = null,
                LastModifiedBy = "pedrodiogo4470@hotmail.com"
            }
            );
            orders.Add(3, new Order
            {
                OrderName = "Order3",
                Priority = PriorityLevel.High,
                IsActive = true,
                Created = DateTime.Now,
                CreatedBy = "pedrodiogo4470@hotmail.com",
                LastModified = null,
                LastModifiedBy = "pedrodiogo4470@hotmail.com"
            }
            );

            await context.Orders.AddRangeAsync(orders.Values.AsEnumerable());
        }
        private static async Task SeedCustomersAsync(TemplateDbContext context)
        {
            customers.Add(1, new Customer
            {
                Address = "Avda. de la Constitución 2222",
                CompanyName = "Ana Trujillo Emparedados y helados",
                ContactName = "Ana Trujillo",
                Country = "Mexico",
                PostalCode = "05021",
                IsActive = true,
                Created = DateTime.Now,
                CreatedBy = "pedrodiogo4470@hotmail.com",
                LastModified = null,
                LastModifiedBy = "pedrodiogo4470@hotmail.com"
            }.AddOrders(
                    orders[1]
                )
            );

            customers.Add(2, new Customer
            {
                Address = "Obere Str. 57",
                CompanyName = "Alfreds Futterkiste",
                ContactName = "Maria Anders",
                Country = "Germany",
                PostalCode = "12209",
                IsActive = true,
                Created = DateTime.Now,
                CreatedBy = "pedrodiogo4470@hotmail.com",
                LastModified = null,
                LastModifiedBy = "pedrodiogo4470@hotmail.com"
            }.AddOrders(
                        orders[2],
                        orders[3]
                    )
            );
            customers.Add(3, new Customer
            {
                Address = "Mataderos  2312",
                CompanyName = "Antonio Moreno Taquería",
                ContactName = "Antonio Moreno",
                Country = "Mexico",
                PostalCode = "05023",
                IsActive = true,
                Created = DateTime.Now,
                CreatedBy = "pedrodiogo4470@hotmail.com",
                LastModified = null,
                LastModifiedBy = "pedrodiogo4470@hotmail.com"
            }
            );


            await context.Customers.AddRangeAsync(customers.Values.AsEnumerable());
        }
    }

}
