using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITemplateDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Value> Values { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Dapper Load Data from Store procedure
        /// </summary>
        /// <typeparam name="T">Object to return</typeparam>
        /// <typeparam name="U">Parameters of store procedure</typeparam>
        /// <param name="storedProcedure">name of Store Procedure</param>
        /// <param name="parameters">Parameters of Store Procedure</param>
        /// <returns></returns>
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters);
    }
}
