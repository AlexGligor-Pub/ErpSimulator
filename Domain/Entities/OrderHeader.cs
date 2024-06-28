using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class OrderHeader
    {
        [JsonIgnore]
        public OrderState ERPState { get; set; }

        [JsonIgnore]
        public IntegrationClient Client { get; set; } = IntegrationClient.MQTT;
    }
}
