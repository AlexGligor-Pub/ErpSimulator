namespace Domain.Entities.UNS
{
    public class UnsOrderComponentMap
    { 
        public UnsOrder UnsOrder { get; set; }
        public string UnsOrderId { get; set; }

        public Component Component { get; set; }
        public string ComponentId { get; set; }
    }
}
