﻿using Domain.Entities.UNS;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class BucketOrderProcessorService
    {
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
