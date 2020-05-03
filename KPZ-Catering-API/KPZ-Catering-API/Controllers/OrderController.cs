using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KPZ_Catering_API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}