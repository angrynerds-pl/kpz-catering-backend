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
            return cateringContext.Dishes.ToList();
        }

        //public static List<DanieZamowienie> getDishOrder(long DishId, long OrderId)
        //{
        //    return cateringContext.DaniaZamowienia.Where(s => s.zamowienie_zamowienie_id == Order).ToList();
        //}

        public static List<DanieZamowienie> getAllDishesOrders() {
            return cateringContext.DishesOrders.ToList();
        }

        /// <summary>
        /// Method to returns list of orders
        /// </summary>
        /// <returns>List of all orders</returns>
        public static List<Zamowienie> getOrders() {
            return cateringContext.Orders.ToList();
        }



        /// <summary>
        /// Method to returns list of 
        /// not realised orders
        /// </summary>
        /// <returns>List of current orders</returns>
        public static List<Zamowienie> getCurrentOrders() {
            return cateringContext.Orders.Where(o => o.status_zamowienia != "w realizacji" && o.status_zamowienia != "anulowano").ToList();
        }

        public static Klient getClientById(Int64 id) {
            return cateringContext.Clients.Where(i => i.klient_id == id).ToList()[0];
        }

        public static List<Admin> GetAdmins() {
            return cateringContext.Admins.ToList();
        }

        /// <summary>
        /// Method put order into database
        /// </summary>
        /// <param name="orderDetails">Order getted by endpoint</param>
        public static void putOrder(OrderDetails orderDetails)
        {
            cateringContext.Database.EnsureCreated();
            Dictionary<String, int> danieILiczbaDan = new Dictionary<String, int>();
            Danie danieHelper = new Danie();
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
                if (danieILiczbaDan.ContainsKey(dish.name))
                {
                    danieILiczbaDan[dish.name]++;
                }
                else {
                    danieILiczbaDan[dish.name]= 1;
                }
            }
            List<Klient> klienciWBazie = cateringContext.Clients.Where(s => s.email == klient.email).ToList();
            if (klienciWBazie.Count == 0){
                cateringContext.Clients.Add(klient);
            }else {
                klienciWBazie[0].imie=klient.imie;
                klienciWBazie[0].nazwisko = klient.nazwisko;
                klienciWBazie[0].kod_pocztowy = klient.kod_pocztowy;
                klienciWBazie[0].miasto = klient.miasto;
                klienciWBazie[0].nr_domu = klient.nr_domu;
                klienciWBazie[0].nr_mieszkania = klient.nr_mieszkania;
                klienciWBazie[0].nr_tel = klient.nr_tel;
                klienciWBazie[0].ulica = klient.ulica;
            }
            cateringContext.Clients.Update(klienciWBazie[0]);
            Zamowienie zamowienie = new Zamowienie() { klient = klient, status_zamowienia="Złożone"};
            DanieZamowienie danieZamowienie = new DanieZamowienie();
            foreach (String nazwaDania in danieILiczbaDan.Keys) {
                danieZamowienie = new DanieZamowienie() { danie = cateringContext.Dishes.Where(s => (s.nazwa == nazwaDania)).ToList()[0], zamowienie = zamowienie, ilosc_dania= danieILiczbaDan[nazwaDania]};
                zamowienie.daniaZamowienia.Add(danieZamowienie);
            }
            cateringContext.Orders.Add(zamowienie);
            cateringContext.SaveChanges();
            }
    }
}
