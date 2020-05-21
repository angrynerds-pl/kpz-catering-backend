using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Extentions.SignalR
{
    public class OrderHub : Hub
    {
        public Task sendNewOrder(Entities.OrderDetails newOrder) {
            return Clients.Caller.SendAsync("The new order was created", newOrder);
        }
    }
}
