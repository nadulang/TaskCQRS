using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskCQRS.Domain.Entities;

namespace TaskCQRS.Application.Interfaces
{
    public interface IEcommerceContext
    {
        public DbSet<Customers> CustomersData1 { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
