using Domain.Entities.UNS;
using HiveMQtt.Client.Options;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class BucketOrderProcessorService
    {
        MQTTPublisher mQTTPublisher;
        OrderStateMachineService orderStateMachineService;
        public BucketOrderProcessorService(OrderStateMachineService orderStateMachineService, MQTTPublisher mQTT)
        {

            this.orderStateMachineService = orderStateMachineService;
            mQTTPublisher = mQTT;

        }
        public async Task SentToUNS(List<UnsOrder> unsOrders)
        {
            string jsonString = JsonSerializer.Serialize(unsOrders);

            
            var isSent = await mQTTPublisher.PublishMessage(jsonString);

            if(isSent == false) 
                return;

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
