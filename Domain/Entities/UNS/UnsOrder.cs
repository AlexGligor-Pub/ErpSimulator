using Domain.Enums;
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

        [JsonIgnore]
        public OrderState ERPState { get; set; }

        [JsonIgnore]
        public virtual ICollection<UnsOrderComponentMap> ComponentList { get; set; }

        [JsonIgnore]
        public virtual ICollection<UnsOrderOperationstMap> OperationsInstruction { get; set; }

        [JsonIgnore]
        public int? OrdersBucketId { get; set; }

        [JsonIgnore]
        public virtual OrdersBucket? OrdersBucket { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
