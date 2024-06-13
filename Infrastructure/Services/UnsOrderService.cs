using Domain.Entities.UNS;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UnsOrderService
    {
        private readonly SqlDbContext _context;

        public UnsOrderService(SqlDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        // Create UnsOrder
        public async Task CreateUnsOrderAsync(UnsOrder unsOrder)
        {
            await _context.UnsOrders.AddAsync(unsOrder);
            await _context.SaveChangesAsync();
        }

        // Create Component
        public async Task CreateComponentAsync(Component component)
        {
            await _context.Components.AddAsync(component);
            await _context.SaveChangesAsync();
        }

        // Create OperationsInstruction
        public async Task CreateOperationsInstructionAsync(OperationsInstruction operationsInstruction)
        {
            await _context.OperationsInstructions.AddAsync(operationsInstruction);
            await _context.SaveChangesAsync();
        }

        // Read all UnsOrder 
        public async Task<List<UnsOrder>> GetUnsOrdersAsync()
        {
            return await _context.UnsOrders
                                 .Include(u => u.ComponentList)
                                    .ThenInclude(uc => uc.Component)
                                 .Include(u => u.OperationsInstruction)
                                    .ThenInclude(uc => uc.OperationsInstruction)
                                 .ToListAsync();
        }

        // Read UnsOrder by ID
        public async Task<UnsOrder> GetUnsOrderByIdAsync(string id)
        {
            return await _context.UnsOrders
                                 .Include(u => u.ComponentList)
                                    .ThenInclude(uc => uc.Component)
                                 .Include(u => u.OperationsInstruction)
                                    .ThenInclude(uc => uc.OperationsInstruction)
                                 .Include(u => u.OrdersBucket)
                                 .FirstOrDefaultAsync(u => u.ID == id);
        }

        // Read Component by ID
        public async Task<Component> GetComponentByIdAsync(string id)
        {
            return await _context.Components
                                 .Include(c => c.UnsOrderList)
                                    .ThenInclude(uc => uc.Component)
                                 .FirstOrDefaultAsync(c => c.ComponentId == id);
        }

        // Read OperationsInstruction by ID
        public async Task<OperationsInstruction> GetOperationsInstructionByIdAsync(string id)
        {
            return await _context.OperationsInstructions
                                 .Include(o => o.UnsOrderList)
                                    .ThenInclude(uc => uc.OperationsInstruction)
                                 .FirstOrDefaultAsync(o => o.ID == id);
        }

        // Update UnsOrder
        public async Task UpdateUnsOrderAsync(UnsOrder unsOrder)
        {
            _context.UnsOrders.Update(unsOrder);
            await _context.SaveChangesAsync();
        }

        // Update Component
        public async Task UpdateComponentAsync(Component component)
        {
            _context.Components.Update(component);
            await _context.SaveChangesAsync();
        }

        // Update OperationsInstruction
        public async Task UpdateOperationsInstructionAsync(OperationsInstruction operationsInstruction)
        {
            _context.OperationsInstructions.Update(operationsInstruction);
            await _context.SaveChangesAsync();
        }

        // Delete UnsOrder
        public async Task DeleteUnsOrderAsync(string id)
        {
            var unsOrder = await _context.UnsOrders.FindAsync(id);
            if (unsOrder != null)
            {
                _context.UnsOrders.Remove(unsOrder);
                await _context.SaveChangesAsync();
            }
        }

        // Delete Component
        public async Task DeleteComponentAsync(string id)
        {
            var component = await _context.Components.FindAsync(id);
            if (component != null)
            {
                _context.Components.Remove(component);
                await _context.SaveChangesAsync();
            }
        }

        // Delete OperationsInstruction
        public async Task DeleteOperationsInstructionAsync(string id)
        {
            var operationsInstruction = await _context.OperationsInstructions.FindAsync(id);
            if (operationsInstruction != null)
            {
                _context.OperationsInstructions.Remove(operationsInstruction);
                await _context.SaveChangesAsync();
            }
        }
    }

}
