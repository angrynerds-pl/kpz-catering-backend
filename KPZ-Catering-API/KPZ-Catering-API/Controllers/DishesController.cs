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
    public class DishesController : ControllerBase
    {
        [HttpGet]
        public List<Dish> getListOfDishes()
        {
            Dish dish = new Dish() {name="Kanapka z serem",price=24.5 };
            Dish dish1 = new Dish() { name = "Kanapka z szynką", price = 24.5 };
            List<Dish> dishes = new List<Dish> { dish, dish1};
            return dishes;
        }
    }
}