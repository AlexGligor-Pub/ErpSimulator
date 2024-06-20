using Domain.Entities;
using Domain.Entities.UNS;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class OrdersBucketService
    {
        private BucketOrderProcessorService bucketOrderProcessorService;
        private readonly SqlDbContext _context;

        private readonly UnsOrderService _unsOrderService;

        public OrdersBucketService(SqlDbContext context, UnsOrderService unsOrderService, BucketOrderProcessorService bucketOrderProcessorService)
        {
            _context = context;
            _unsOrderService = unsOrderService;
            _context.Database.EnsureCreated();
            this.bucketOrderProcessorService = bucketOrderProcessorService;
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
            if(ordersBucket.UnsOrders != null)
                if ( ordersBucket.UnsOrders.Count > 0)
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
                    foreach (var operation in ordersBucket.UnsOrder.OperationsInstructionMap)
                        operations.Add(new UnsOrderOperationstMap() { UnsOrderId = order.ID, OperationsInstructionId = operation.OperationsInstructionId });
                    order.OperationsInstructionMap = operations;

                    var components = new List<UnsOrderComponentMap>();
                    foreach (var operation in ordersBucket.UnsOrder.ComponentListMap)
                        components.Add(new UnsOrderComponentMap() { UnsOrderId = order.ID, ComponentId = operation.ComponentId });
                    order.ComponentListMap = components;

                    await _unsOrderService.CreateUnsOrderAsync(order);
                }
                catch (Exception ex) { }
            }
          
            ordersBucket.State = BucketOrdersState.OrdersAreReady;
            await _context.SaveChangesAsync();
        }

        public async Task Sent(OrdersBucket order)
        {
            var isSent = await bucketOrderProcessorService.SentToUNS(order.UnsOrders.ToList());
            if (!isSent)
                return;
            order.State = Domain.Enums.BucketOrdersState.Sent;
            await _context.SaveChangesAsync();
        }

        // Read all OrdersBucket 
        public async Task<List<OrdersBucket>> GetOrdersBucketAsync()
        {
            var orders = await _context.OrdersBucket
                                 .Include(u => u.UnsOrders)
                                      .ThenInclude(o => o.ComponentListMap)
                                        .ThenInclude(uc => uc.Component)
                                 .Include(u => u.UnsOrders)
                                      .ThenInclude(o => o.OperationsInstructionMap)
                                        .ThenInclude(uo => uo.OperationsInstruction)
                                 .Include(u => u.UnsOrder)
                                     .ThenInclude(o => o.ComponentListMap)
                                        .ThenInclude(uc => uc.Component)
                                 .Include(u => u.UnsOrder)
                                      .ThenInclude(o => o.OperationsInstructionMap)
                                        .ThenInclude(uo => uo.OperationsInstruction)
                                 .ToListAsync();

           
            return orders.Select(o => {
                o.UnsOrder.ComponentList = o.UnsOrder.ComponentListMap.Select(cl => cl.Component).ToList();
                o.UnsOrder.OperationsInstruction = o.UnsOrder.OperationsInstructionMap.Select(cl => cl.OperationsInstruction).ToList();
                foreach (var order in o.UnsOrders)
                {
                    order.ComponentList = order.ComponentListMap.Select(cl => cl.Component).ToList();
                    order.OperationsInstruction = order.OperationsInstructionMap.Select(cl => cl.OperationsInstruction).ToList();
                }
                return o;
            }).ToList();
        }

        // Read OrdersBucket by ID
        public async Task<OrdersBucket> GetOrdersBucketByIdAsync(int id)
        {
            return await _context.OrdersBucket
                                 .Include(u => u.UnsOrders)
                                  .ThenInclude(o => o.ComponentListMap)
                                    .ThenInclude(uc => uc.Component)
                                 .Include(u => u.UnsOrders)
                                  .ThenInclude(o => o.OperationsInstructionMap)
                                    .ThenInclude(uo => uo.OperationsInstruction)
                                 .Include(u => u.UnsOrder)
                                    .ThenInclude(o => o.ComponentListMap)
                                        .ThenInclude(uc => uc.Component)
                                     .Include(u => u.UnsOrders)
                                      .ThenInclude(o => o.OperationsInstructionMap)
                                        .ThenInclude(uo => uo.OperationsInstruction)
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
