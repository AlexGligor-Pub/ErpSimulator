using Domain.Entities;
using Domain.Entities.UNS;
using Domain.Enums;
using Infrastructure.DataBase;
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
        public DbSet<UnsOrder> UnsOrders { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<OperationsInstruction> OperationsInstructions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnsOrder>()
                .HasOne(u => u.OperationsInstruction)
                .WithOne(o => o.UnsOrder)
                .HasForeignKey<OperationsInstruction>(o => o.ID);

            modelBuilder.Entity<Component>()
                .HasOne(c => c.UnsOrder)
                .WithMany(u => u.ComponentList)
                .HasForeignKey(c => c.UnsOrderID);

           
            SeedData.SeedDemoOrderData(modelBuilder);
            SeedData.SeedUnsOrderData(modelBuilder);
        }

        
    }
}