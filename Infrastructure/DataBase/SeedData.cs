using Domain.Entities;
using Domain.Entities.UNS;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase
{
    public static class SeedData
    {
        public static void SeedOrdersBucketData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdersBucket>().HasData(
                new OrdersBucket() { Id = 1, UnsOrderId = "order1", Created = DateTime.UtcNow , State = BucketOrdersState.Created, RequestCount = 5, StartDate = DateTime.Now.AddDays(2), EndDate = DateTime.Now.AddDays(9) },
                new OrdersBucket() { Id = 2, UnsOrderId = "order2", Created = DateTime.UtcNow , State = BucketOrdersState.Created, RequestCount = 10, StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddDays(18) }
            );
        }
        public static void SeedDemoOrderData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DemoOrder>().HasData(
            new DemoOrder() { Id = 1, Name = "Order 1", CreatedDate = DateTime.Now, IsDeleted = false, State = OrderState.Created },
            new DemoOrder() { Id = 2, Name = "Order 2", CreatedDate = DateTime.Now, IsDeleted = false, State = OrderState.Created },
            new DemoOrder() { Id = 3, Name = "Order 3", CreatedDate = DateTime.Now, IsDeleted = false, State = OrderState.Created },
            new DemoOrder() { Id = 4, Name = "Order 4", CreatedDate = DateTime.Now, IsDeleted = false, State = OrderState.Created },
            new DemoOrder() { Id = 5, Name = "Order 5", CreatedDate = DateTime.Now, IsDeleted = false, State = OrderState.Created }
            );
        }
        public static void SeedUnsOrderData(ModelBuilder modelBuilder)
        {
            var unsOrders = new List<UnsOrder>
        {
            new UnsOrder
            {
                ID = "order1",
                Description = "Order 1 description",
                Type = "Type1",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                Priority = "High",
                OrderState = "Open",
                Status = "Active",
                MaterialId = "mat1",
                OrderQuantity = 100,
                UnitOfMeasure = "pcs",
                Facility = "Facility1",
                Executor = "Executor1"
            },
            new UnsOrder
            {
                ID = "order2",
                Description = "Order 2 description",
                Type = "Type2",
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(3),
                Priority = "Medium",
                OrderState = "Closed",
                Status = "Inactive",
                MaterialId = "mat2",
                OrderQuantity = 200,
                UnitOfMeasure = "pcs",
                Facility = "Facility2",
                Executor = "Executor2"
            }
        };

            var operationsInstructions = new List<OperationsInstruction>
        {
            new OperationsInstruction
            {
                ID = "order1",
                Description = "Operations for Order 1",
                WorkMasterID = "WM1",
                WorkMasterVersion = "1.0",
                WorkCenter = "WC1",
                Equipment = "EQ1",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            },
            new OperationsInstruction
            {
                ID = "order2",
                Description = "Operations for Order 2",
                WorkMasterID = "WM2",
                WorkMasterVersion = "1.1",
                WorkCenter = "WC2",
                Equipment = "EQ2",
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(3)
            }
        };

            var components = new List<Component>
        {
            new Component
            {
                ComponentId = "comp1",
                Quantity = 10,
                UnitOfMeasure = "pcs"
            },
            new Component
            {
                ComponentId = "comp2",
                Quantity = 20,
                UnitOfMeasure = "pcs"
            },
            new Component
            {
                ComponentId = "comp3",
                Quantity = 30,
                UnitOfMeasure = "pcs"
            },
            new Component
            {
                ComponentId = "comp4",
                Quantity = 40,
                UnitOfMeasure = "pcs"
            }
        };
            var ordercomponentlist = new List<UnsOrderComponentMap>()
            {
                new UnsOrderComponentMap(){UnsOrderId = "order1", ComponentId = "comp1"  },
                new UnsOrderComponentMap(){UnsOrderId = "order1", ComponentId = "comp2"  },
                new UnsOrderComponentMap(){UnsOrderId = "order2", ComponentId = "comp3"  },
                new UnsOrderComponentMap(){UnsOrderId = "order2", ComponentId = "comp4"  },
            };

            var orderinstructionlist = new List<UnsOrderOperationstMap>()
            {
                new UnsOrderOperationstMap(){UnsOrderId = "order1", OperationsInstructionId = "order1"  },
                new UnsOrderOperationstMap(){UnsOrderId = "order2", OperationsInstructionId = "order2"  },
            };

            modelBuilder.Entity<UnsOrderComponentMap>().HasData(ordercomponentlist);
            modelBuilder.Entity<UnsOrderOperationstMap>().HasData(orderinstructionlist);

            modelBuilder.Entity<UnsOrder>().HasData(unsOrders);
            modelBuilder.Entity<OperationsInstruction>().HasData(operationsInstructions);
            modelBuilder.Entity<Component>().HasData(components);
        }
    }
}
