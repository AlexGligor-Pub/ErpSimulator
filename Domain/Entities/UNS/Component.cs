using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities.UNS
{
    public class Component
    {
        [Key]
        public string ComponentId { get; set; }

        public int Quantity { get; set; }

        public string UnitOfMeasure { get; set; }

        [JsonIgnore]
        public string? UnsOrderID { get; set; }

        [ForeignKey("UnsOrderID")]
        [JsonIgnore]
        public virtual UnsOrder UnsOrder { get; set; }
    }
}
