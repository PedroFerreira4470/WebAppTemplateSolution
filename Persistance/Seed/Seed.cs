
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistance.Seed
{
    public class Seed
    {
        public static async Task SeedDataAsync(TemplateDbContext context) 
        {
           
            if (context.Customers.Any())
            {
                return;
            }
            await SeedValuesAsync(context);
            await SeedOrdersAsync(context);
            await SeedCustomersAsync(context);
        }

        private static async Task SeedValuesAsync(TemplateDbContext context)
        {
            var values = new List<Value>
            {
                new Value { ValueNumber=1},
                new Value {ValueNumber=2},
                new Value { ValueNumber=3},
            };

            context.Values.AddRange(values);

            await context.SaveChangesAsync();
        }

        private static async Task SeedOrdersAsync(TemplateDbContext context)
        {
            var orders = new List<Order>
            {
                new Order {OrderName="Order1",Priority=PriorityLevel.Low, IsActive=true,Created=DateTime.Now,CreatedBy=1,LastModified=null,LastModifiedBy=1},
                new Order {OrderName="Order2",Priority=PriorityLevel.Medium, IsActive=true,Created=DateTime.Now,CreatedBy=2,LastModified=null,LastModifiedBy=2  },
                new Order {OrderName="Order3",Priority=PriorityLevel.High, IsActive=true,Created=DateTime.Now,CreatedBy=3,LastModified=null,LastModifiedBy=3  },

            };

            context.Orders.AddRange(orders);

            await context.SaveChangesAsync();
        }
        private static async Task SeedCustomersAsync(TemplateDbContext context)
        {
            var customers = new List<Customer>
            {
                new Customer 
                {   Address = "Obere Str. 57",
                    CompanyName = "Alfreds Futterkiste",
                    ContactName = "Maria Anders",
                    Country = "Germany",
                    PostalCode = "12209",
                    IsActive=true,
                    Created=DateTime.Now,
                    CreatedBy=1,
                    LastModified=null,
                    LastModifiedBy=1 
                }.AddOrders(
                    new Order {OrderName="Order3",Priority=PriorityLevel.High, IsActive=true,Created=DateTime.Now,CreatedBy=3,LastModified=null,LastModifiedBy=3  },
                    new Order {OrderName="Order3",Priority=PriorityLevel.High, IsActive=true,Created=DateTime.Now,CreatedBy=3,LastModified=null,LastModifiedBy=3  }
                ),
                new Customer { Address = "Avda. de la Constitución 2222", CompanyName = "Ana Trujillo Emparedados y helados", ContactName = "Ana Trujillo", Country = "Mexico", PostalCode = "05021",IsActive=true,Created=DateTime.Now,CreatedBy=2,LastModified=null,LastModifiedBy=2  },
                new Customer {Address = "Mataderos  2312", CompanyName = "Antonio Moreno Taquería", ContactName = "Antonio Moreno", Country = "Mexico", PostalCode = "05023",IsActive=true,Created=DateTime.Now,CreatedBy=3,LastModified=null,LastModifiedBy=3  },
               
            };

            context.Customers.AddRange(customers);

            await context.SaveChangesAsync();
        }


        private static Task SeedUsersAsync()
        {
            throw new NotImplementedException();
        }

    }

    internal static class OrderExtensions
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
