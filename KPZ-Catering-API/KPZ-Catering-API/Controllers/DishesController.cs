using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KPZ_Catering_API.Database;
using KPZ_Catering_API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Catering_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        CateringContext context = new CateringContext();
        [HttpGet]
        public List<Dish> getListOfDishes()
        {
            Dish dish = new Dish() {name="Kanapka z serem",price=24.5 };
            Dish dish1 = new Dish() { name = "Kanapka z szynką", price = 24.5 };
            List<Dish> dishes = new List<Dish> { dish, dish1};
            return dishes;
        }

        [HttpGet("Test")]
        public List<Database.Entities.Danie> getOneDish() {
            context.Database.EnsureCreated();
            var danie = context.Dish.Where(s => s.cena == (Decimal)9.99).ToList();
            var zamowienie = context.Order.Where(o => o.zamowienie_id == 1);
            var klient = context.Client.Where(c => c.klient_id == 1);
            var konto = context.Account.Where(a => a.klienci_klient_id == 1);
            var admin = context.Admin.Where(admin => admin.admin_id == 1);
            var dania_zamowienia = context.DaniaZamowienia.Where(dania => dania.zamowienie_zamowienie_id == 1);
            return danie;
        }
    }
}