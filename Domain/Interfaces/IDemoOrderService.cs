using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDemoOrderService
    {
        Task<List<DemoOrder>> GetDemoOrdersAsync();
    }
}
