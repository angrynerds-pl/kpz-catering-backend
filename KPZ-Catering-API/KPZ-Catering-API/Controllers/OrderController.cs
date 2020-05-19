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

        /// <summary>
        /// Endpoint gets list of all orders
        /// </summary>
        /// <returns>List of orders in json format</returns>
        [HttpGet("ordersList")]
        public String getOrderList() {
            var dbOrders = Database.Logic.DatabaseController.getOrders();
            var listOfOrders = new List<Entities.OrderDetails>();
            foreach (var order in dbOrders) {
                listOfOrders.Add(Database.Logic.OrderParser.FromDbToEntity.parseOrderDetails(order));
            }
            return JsonConvert.SerializeObject(listOfOrders);
        }

        //[HttpGet("ordersListAlt")]
        /*public String getOrderListAlt()
        {
            var dbDishesOrders = Database.Logic.DatabaseController.getAllDishesOrders();
            var listOfOrders = new List<Entities.OrderDetails>();
            foreach (var order in dbOrders)
            {
                listOfOrders.Add(Database.Logic.OrderParser.FromDbToEntity.parseOrderDetailsAlt(dbDishesOrders));
            }
            return JsonConvert.SerializeObject(listOfOrders);
        }*/

        /// <summary>
        /// Endpoint gets list of current orders
        /// </summary>
        /// <returns>List of current orders in json format</returns>
        [HttpGet("currentOrders")]
        public String getCurrentOrders() {
            var dbCurrentOrders = Database.Logic.DatabaseController.getCurrentOrders();
            var listOfCurrentOrderes = new List<Entities.OrderDetails>();
            foreach (var order in dbCurrentOrders) {
                listOfCurrentOrderes.Add(Database.Logic.OrderParser.FromDbToEntity.parseOrderDetails(order));
            }
            return JsonConvert.SerializeObject(listOfCurrentOrderes);
        }
    }
}