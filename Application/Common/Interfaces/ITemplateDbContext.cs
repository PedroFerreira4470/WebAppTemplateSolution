using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITemplateDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Value> Values { get; set; }

        public IDbConnection DbConnection { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
