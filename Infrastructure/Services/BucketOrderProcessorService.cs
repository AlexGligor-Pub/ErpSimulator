using Domain.Entities.UNS;
using HiveMQtt.Client.Options;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class BucketOrderProcessorService
    {

        MQTTPublisher mQTTPublisher;
        OrderStateMachineService orderStateMachineService;
        public BucketOrderProcessorService(OrderStateMachineService orderStateMachineService)
        {

            this.orderStateMachineService = orderStateMachineService;

        }
        public async Task SentToUNS(List<UnsOrder> unsOrders)
        {
            // convert list to JSON
            // send JSON to UNS
            // if all ok, sent to state machine 

            string jsonString = JsonSerializer.Serialize(unsOrders);

            var options = new HiveMQClientOptions();
            options.Host = "bf271dc2c3614d1abd4a94cbe9bbdfc5.s1.eu.hivemq.cloud";
            options.Port = 8883;
            options.UserName = "mihai";
            options.Password = "HiveMq2024";
            options.UseTLS = true;

            mQTTPublisher = new MQTTPublisher(options);
            await mQTTPublisher.ConnectToBroker();
            await mQTTPublisher.PublishMessage("Company/Europe/Romania/Cluj-Napoca/ERP/ProductionSchedule", jsonString);

            foreach (var unsOrder in unsOrders)
            {
                if(unsOrder.ERPState == Domain.Enums.OrderState.Created)
                {
                    await orderStateMachineService.ChangeState(unsOrder);
                }
                
            }
        }
    }
}
