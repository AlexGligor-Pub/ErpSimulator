using Domain.Entities.UNS;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class OperationsInstructionsService
    {
        private readonly SqlDbContext _context;

        public OperationsInstructionsService(SqlDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        // Read All Operations
        public async Task<List<OperationsInstruction>> GetOperationsInstructionsAsync() => 
            await _context.OperationsInstructions.ToListAsync();

        public List<OperationsInstruction> GetOperationsInstructionsByUnsOrderId(string id)
        {
            var operationsInstructions = _context.Set<OperationsInstruction>().FromSqlRaw("SELECT oi.[ID], oi.[Description],oi.[WorkMasterID],oi.[WorkMasterVersion], " +
                "oi.[WorkCenter],oi.[Equipment],oi.[StartTime],oi.[EndTime],oi.[UnsOrderID] " +
                "FROM[OperationsInstructions] oi" +
                "INNER JOIN[UnsOrderOperationstMap] uom  ON uom.OperationsInstructionId = oi.ID" +
                "WHERE uom.UnsOrderId ={0} ", id).ToList();

            return operationsInstructions;
        }

    }

}
