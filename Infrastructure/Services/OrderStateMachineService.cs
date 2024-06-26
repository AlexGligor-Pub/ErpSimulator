using Domain.Entities.UNS;
using Domain.Enums;
using System.Net;

namespace Infrastructure.Services
{
    public class OrderStateMachineService
    {
        private UnsOrderService unsOrderService;
        private SapOrderService sapOrderService;
        public OrderStateMachineService(UnsOrderService unsOrderService, SapOrderService sapOrderService)
        {
            this.unsOrderService = unsOrderService;
            this.sapOrderService = sapOrderService;
        }

        public async Task ChangeState(UnsOrder order)
        {
            switch(order.ERPState)
            {
                case OrderState.New:
                    await unsOrderService.CreateUnsOrderAsync(order);
                    break;

                case OrderState.Created:

                    if (order.Client == IntegrationClient.SAP)
                    {
                        var status = await sapOrderService.SendOrderToSapAsync(order);
                        if(status == HttpStatusCode.OK)
                            order.ERPState = OrderState.SentToSAP;
                        else
                            order.ERPState = OrderState.SendToSapFailed;
                    }
                    else if (order.Client == IntegrationClient.MQTT)
                    {
                        order.ERPState = OrderState.SentToSAP;
                    }

                    await unsOrderService.UpdateUnsOrderAsync(order);

                    break;

                case OrderState.SentToUNS:
                    break;
            }
        }
    }
}
