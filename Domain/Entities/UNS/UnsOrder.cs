using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities.UNS
{
    public class UnsOrder
    {
        [Key]
        public string ID { get; set; }

        public string? Description { get; set; }

        public string Type { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string? Priority { get; set; }

        public string? OrderState { get; set; }

        public string? Status { get; set; }

        public string? MaterialId { get; set; }

        public int OrderQuantity { get; set; }

        public string? UnitOfMeasure { get; set; }

        public string? Facility { get; set; }

        public string? Executor { get; set; }

        public virtual ICollection<Component> ComponentList { get; set; }

        public virtual OperationsInstruction OperationsInstruction { get; set; }

        [JsonIgnore]
        public int? OrdersBucketId { get; set; }

        [ForeignKey("OrdersBucketId")]
        [JsonIgnore]
        public virtual OrdersBucket? OrdersBucket { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
