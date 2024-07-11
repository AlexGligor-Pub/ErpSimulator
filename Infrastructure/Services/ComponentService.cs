using Domain.Entities.UNS;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class ComponentService
    {
        private readonly SqlDbContext _context;

        public ComponentService(SqlDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        // Read All Components
        public async Task<List<Component>> GetComponentsAsync()
        {
            var components = await _context.Components.ToListAsync();

            return components;
        }

        // Read All Components associated to an UnsOrder
        public List<Component> GetComponentsByUnsOrderId(string id)
        {
            var components =  _context.Set<Component>().FromSqlRaw("" +
                "SELECT c.[ComponentId], c.[Quantity], c.[UnitOfMeasure], c.[UnsOrderID] " +
                "FROM [UnsOrderComponentMap] ucm " +
                "INNER JOIN [Components] c ON ucm.ComponentId = c.ComponentId " +
                "WHERE ucm.UnsOrderId ={0}", id).ToList();

            return components;
        }
    }

}
