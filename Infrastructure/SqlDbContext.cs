using Domain.Entities;
using Domain.Entities.UNS;
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
        public DbSet<OrdersBucket> OrdersBucket { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-Many relationship between UnsOrder and Component
            modelBuilder.Entity<UnsOrderComponentMap>()
                .HasKey(uc => new { uc.UnsOrderId, uc.ComponentId });

            modelBuilder.Entity<UnsOrderComponentMap>()
                .HasOne(uc => uc.UnsOrder)
                .WithMany(u => u.ComponentListMap)
                .HasForeignKey(uc => uc.UnsOrderId);

            modelBuilder.Entity<UnsOrderComponentMap>()
                .HasOne(uc => uc.Component)
                .WithMany(c => c.UnsOrderList)
                .HasForeignKey(uc => uc.ComponentId);

            // Many-to-Many relationship between UnsOrder and OperationsInstruction
            modelBuilder.Entity<UnsOrderOperationstMap>()
                .HasKey(uo => new { uo.UnsOrderId, uo.OperationsInstructionId });

            modelBuilder.Entity<UnsOrderOperationstMap>()
                .HasOne(uo => uo.UnsOrder)
                .WithMany(u => u.OperationsInstructionMap)
                .HasForeignKey(uo => uo.UnsOrderId);

            modelBuilder.Entity<UnsOrderOperationstMap>()
                .HasOne(uo => uo.OperationsInstruction)
                .WithMany(o => o.UnsOrderList)
                .HasForeignKey(uo => uo.OperationsInstructionId);

            // One-to-Many relationship between OrdersBucket and UnsOrder
            modelBuilder.Entity<UnsOrder>()
                .HasOne(o => o.OrdersBucket)
                .WithMany(b => b.UnsOrders)
                .HasForeignKey(o => o.OrdersBucketId);

            SeedData.SeedDemoOrderData(modelBuilder);
            SeedData.SeedUnsOrderData(modelBuilder);
            SeedData.SeedOrdersBucketData(modelBuilder);
        }

        
    }
}