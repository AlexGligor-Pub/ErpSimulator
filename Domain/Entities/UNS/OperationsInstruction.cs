using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities.UNS
{
    public class OperationsInstruction
    {
        [Key]
        public string ID { get; set; }

        public string? Description { get; set; }

        public string? WorkMasterID { get; set; }

        public string? WorkMasterVersion { get; set; }

        public string WorkCenter { get; set; }

        public string? Equipment { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [JsonIgnore]
        public virtual ICollection<UnsOrderOperationstMap> UnsOrderList { get; set; }
    }
}
