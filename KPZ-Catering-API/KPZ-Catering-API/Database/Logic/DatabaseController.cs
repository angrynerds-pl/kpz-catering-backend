using KPZ_Catering_API.Database.Entities;
using KPZ_Catering_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Database.Logic
{
    /// <summary>
    /// Class controls flow of data in backend
    /// </summary>
    public static class DatabaseController
    {
        static CateringContext cateringContext = new CateringContext();
        /// <summary>
        /// Method returns list of avaliable dishes
        /// </summary>
        /// <returns>List of avaliable dishes</returns>
        public static List<Danie> getDishes()
        {
            return cateringContext.Dish.ToList();
        }

        /// <summary>
        /// Method to returns list of orders
        /// </summary>
        /// <returns>List of all orders</returns>
        public static List<Zamowienie> getOrders() {
            return cateringContext.Order.ToList();
        }

        public static Klient getClientById(Int64 id) {
            return cateringContext.Client.Where(i => i.klient_id == id).ToList()[0];
        }

        /// <summary>
        /// Method put order into database
        /// </summary>
        /// <param name="orderDetails">Order getted by endpoint</param>
        public static void putOrder(OrderDetails orderDetails)
        {
            cateringContext.Database.EnsureCreated();
            Klient klient = new Klient()
            {
                imie = orderDetails.client.name,
                nazwisko = orderDetails.client.lastName,
                email = orderDetails.client.email,
                nr_tel = orderDetails.client.phone.ToString()
            };
            List<Danie> dania = new List<Danie>();
            foreach (Dish dish in orderDetails.dishes)
            {
                dania.Add(new Danie() { cena = dish.price, nazwa = dish.name, sklad = dish.description });
            }
            cateringContext.Client.Add(klient);
            Zamowienie zamowienie = new Zamowienie() { klient = klient, status_zamowienia="Złożone"};
            DanieZamowienie danieZamowienie = new DanieZamowienie();
            foreach (Danie danie in dania) {
                danieZamowienie = new DanieZamowienie() { danie = cateringContext.Dish.Where(s => (s.nazwa == danie.nazwa)).ToList()[0], zamowienie = zamowienie };
                zamowienie.daniaZamowienia.Add(danieZamowienie);
            }
            cateringContext.Order.Add(zamowienie);
            cateringContext.SaveChanges();

            }
    }
}
