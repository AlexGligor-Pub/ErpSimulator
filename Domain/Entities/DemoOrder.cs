using Domain.Enums;

namespace Domain.Entities
{
    public class DemoOrder
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public OrderState? State { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int TestMigrationsUpdate { get; set; }
    }
}
