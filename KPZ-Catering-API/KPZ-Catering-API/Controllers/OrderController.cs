using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KPZ_Catering_API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KPZ_Catering_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Endpoint loads order data into database
        /// </summary>
        /// <param name="order">Order to put in database</param>
        [HttpPost("")]
        public void Post([FromBody] OrderDetails order){
            Database.Logic.DatabaseController.putOrder(order);
        }

        [HttpGet("ordersList")]
        public String getOrderList() {
            var dbOrders = Database.Logic.DatabaseController.getOrders();
            var listOfOrders = new List<Entities.OrderDetails>();
            foreach (var order in dbOrders) {
                listOfOrders.Add(Database.Logic.OrderParser.FromDbToEntity.parseOrderDetails(order));
            }
            return JsonConvert.SerializeObject(listOfOrders);
        }
    }
}