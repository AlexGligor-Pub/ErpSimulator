using Domain.Entities.UNS;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderStateMachineService
    {
        UnsOrderService unsOrderService;
        public OrderStateMachineService(UnsOrderService unsOrderService)
        {
            this.unsOrderService = unsOrderService;
        }

        public async Task ChangeState(UnsOrder order)
        {
            switch(order.ERPState)
            {
                case OrderState.New:
                    await unsOrderService.CreateUnsOrderAsync(order);
                    break;

                case OrderState.Created:
                    order.ERPState = OrderState.SentToUNS;
                    await unsOrderService.UpdateUnsOrderAsync(order);
                    break;

                case OrderState.SentToUNS:
                    break;
            }
        }
    }
}
