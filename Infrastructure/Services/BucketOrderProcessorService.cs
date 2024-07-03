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
        public async Task<bool> SentToUNS(List<UnsOrder> unsOrders)
        {
            var prodOrd = new ProdOrders() { ProductionOrders = unsOrders };
            string jsonString = JsonSerializer.Serialize(prodOrd);
            
            var isSent = await mQTTPublisher.PublishMessage(jsonString);

            if(isSent == false) 
                return false;

            foreach (var unsOrder in unsOrders)
            {
                if(unsOrder.ERPState == Domain.Enums.OrderState.Created)
                    await orderStateMachineService.ChangeState(unsOrder);
            }
            return true;
        }

        public async Task<bool> SentToSAP(List<UnsOrder> unsOrders)
        {
            try
            {
                foreach (var unsOrder in unsOrders)
                {
                    unsOrder.Client = Domain.Enums.IntegrationClient.SAP;
                    // if (unsOrder.ERPState == Domain.Enums.OrderState.Created)
                        await orderStateMachineService.ChangeState(unsOrder);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private class ProdOrders
        {
            public List<UnsOrder> ProductionOrders { get; set; }    
        }
    }
}
