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
        public virtual ICollection<UnsOrderComponentMap> UnsOrderList { get; set; }

    }
}
