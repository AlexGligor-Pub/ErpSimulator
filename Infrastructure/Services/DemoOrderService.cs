
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class DemoOrderService : IDemoOrderService
    {
        SqlDbContext context;
        public DemoOrderService(SqlDbContext sqlDbContext)
        {
            context = sqlDbContext;

            context.Database.EnsureCreated();
        }
        public async Task<List<DemoOrder>> GetDemoOrdersAsync()
        {
            return await context.DemoOrders.ToListAsync();
        }
    }
}
