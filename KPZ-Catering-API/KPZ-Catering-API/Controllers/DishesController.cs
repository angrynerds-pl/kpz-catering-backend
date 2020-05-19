using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KPZ_Catering_API.Entities;
using Microsoft.AspNetCore.Authorization;
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
            List<Dish> dishes = new List<Dish>();
            foreach (Database.Entities.Danie danie in Database.Logic.DatabaseController.getDishes()) {
                dishes.Add(new Dish() { description = danie.sklad, name = danie.nazwa, price = danie.cena });
            }
            return dishes;
        }
    }
}