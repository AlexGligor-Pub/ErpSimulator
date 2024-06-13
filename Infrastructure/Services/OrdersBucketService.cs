using Domain.Entities;
using Domain.Entities.UNS;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class OrdersBucketService
    {
        private readonly SqlDbContext _context;

        private readonly UnsOrderService _unsOrderService;

        public OrdersBucketService(SqlDbContext context, UnsOrderService unsOrderService)
        {
            _context = context;
            _unsOrderService = unsOrderService;
            _context.Database.EnsureCreated();
        }

        // Create OrdersBucket
        public async Task CreateOrdersBucketAsync(OrdersBucket ordersBucket)
        {
            ordersBucket.Created = DateTime.Now;
            ordersBucket.State = BucketOrdersState.Created;
            await _context.OrdersBucket.AddAsync(ordersBucket);
            await _context.SaveChangesAsync();
        }

        public async Task GenerateOrdersAsync(OrdersBucket ordersBucket)
        {
            if (ordersBucket.UnsOrders.Count > 0)
                return;

            ordersBucket.State = BucketOrdersState.GeneratingOrders;
            await _context.SaveChangesAsync();

            var seconds = (ordersBucket.EndDate - ordersBucket.StartDate).TotalSeconds/ordersBucket.RequestCount;
            var startDate = ordersBucket.StartDate;

            Random random = new Random(100000);
            for (var i = 0; i < ordersBucket.RequestCount; i++) 
            {
                try
                {
                    var order = (UnsOrder)ordersBucket.UnsOrder.Clone();

                    order.ID = order.ID + "_" + i.ToString() + DateTime.Now.Ticks.ToString() + random.Next().ToString();
                    order.StartTime = startDate;
                    order.EndTime = startDate.AddSeconds(seconds);
                    startDate = order.EndTime;

                    order.OrdersBucket = ordersBucket;
                    order.OrdersBucketId = ordersBucket.Id;

                    var operations = new List<UnsOrderOperationstMap>();
                    foreach (var operation in ordersBucket.UnsOrder.OperationsInstruction)
                        operations.Add(new UnsOrderOperationstMap() { UnsOrderId = order.ID, OperationsInstructionId = operation.OperationsInstructionId });
                    order.OperationsInstruction = operations;

                    var components = new List<UnsOrderComponentMap>();
                    foreach (var operation in ordersBucket.UnsOrder.ComponentList)
                        components.Add(new UnsOrderComponentMap() { UnsOrderId = order.ID, ComponentId = operation.ComponentId });
                    order.ComponentList = components;

                    await _unsOrderService.CreateUnsOrderAsync(order);
                }
                catch (Exception ex) { }
            }
          
            ordersBucket.State = BucketOrdersState.OrdersAreReady;
            await _context.SaveChangesAsync();
        }

        // Read all OrdersBucket 
        public async Task<List<OrdersBucket>> GetOrdersBucketAsync()
        {
            return await _context.OrdersBucket
                                 .Include(u => u.UnsOrders)
                                 .Include(u => u.UnsOrder)
                                 .ToListAsync();
        }

        // Read OrdersBucket by ID
        public async Task<OrdersBucket> GetOrdersBucketByIdAsync(int id)
        {
            return await _context.OrdersBucket
                                 .Include(u => u.UnsOrders)
                                 .Include(u => u.UnsOrder)
                                 .FirstOrDefaultAsync(u => u.Id == id);
        }

        // Update OrdersBucket
        public async Task UpdateOrdersBucketAsync(OrdersBucket OrdersBucket)
        {
            _context.OrdersBucket.Update(OrdersBucket);
            await _context.SaveChangesAsync();
        }

        // Delete OrdersBucket
        public async Task DeleteOrdersBucketAsync(int id)
        {
            var OrdersBucket = await _context.OrdersBucket.FindAsync(id);
            if (OrdersBucket != null)
            {
                _context.OrdersBucket.Remove(OrdersBucket);
                await _context.SaveChangesAsync();
            }
        }
     
    }

}
