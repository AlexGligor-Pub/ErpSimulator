using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
       : base(options)
        {

        }
        public DbSet<DemoOrder> DemoOrders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DemoOrder>().HasData(
            new DemoOrder() { Id = 1, Name = "Order 1", CreatedDate = DateTime.Now, IsDeleted = false, State = OrderState.Created },
            new DemoOrder() { Id = 2, Name = "Order 2", CreatedDate = DateTime.Now, IsDeleted = false, State = OrderState.InWork },
            new DemoOrder() { Id = 3, Name = "Order 3", CreatedDate = DateTime.Now, IsDeleted = false, State = OrderState.Processed},
            new DemoOrder() { Id = 4, Name = "Order 4", CreatedDate = DateTime.Now, IsDeleted = false, State = OrderState.Deleted },
            new DemoOrder() { Id = 5, Name = "Order 5", CreatedDate = DateTime.Now, IsDeleted = false, State = OrderState.None }
            );
        }
    }
}