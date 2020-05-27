using KPZ_Catering_API.Database.Entities;
using KPZ_Catering_API.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        /// <summary>
        /// method to return all
        /// dishes order
        /// </summary>
        /// <returns>list of dishes order</returns>
        public static List<DanieZamowienie> getAllDishesOrders() {
            return cateringContext.DishesOrders.ToList();
        }

        /// <summary>
        /// Method to return 
        /// dish specified by id 
        /// </summary>
        /// <param name="id">dish's id</param>
        /// <returns>dish specified by id</returns>
        public static Danie getDishById(Int64 id) {
            return cateringContext.Dishes.Where(i => i.danie_id == id).ToList()[0];
        }

        /// <summary>
        /// Method to return Dishes
        /// Order by id
        /// </summary>
        /// <param name="id">Dishes order id</param>
        /// <returns>Dishes order specified by id</returns>
        public static List<DanieZamowienie> getDishesOrderById(Int64 id) {
            return cateringContext.DishesOrders.Where(i => i.zamowienie_zamowienie_id == id).ToList();
        }

        /// <summary>
        /// Method to return list of orders
        /// </summary>
        /// <returns>List of all orders</returns>
        public static List<Zamowienie> getOrders() {
            return cateringContext.Orders.ToList();
        }

        /// <summary>
        /// Method to return order 
        /// specified by id 
        /// </summary>
        /// <param name="id">order's id</param>
        /// <returns>order specified by id</returns>
        public static Zamowienie getOrderById(Int64 id) {
            return cateringContext.Orders.Where(s => s.zamowienie_id == id).ToList()[0];
        }

        /// <summary>
        /// Method to update status
        /// of the order
        /// </summary>
        /// <param name="status">new order's status</param>
        public static void updateStatus(KPZ_Catering_API.Entities.Status status) {
            var dbOrder = getOrderById(status.idZamowienia);
            dbOrder.status_zamowienia = status.nowyStatus;
            cateringContext.Orders.Update(dbOrder);
            cateringContext.SaveChanges();
        }

        /// <summary>
        /// Method to return list of 
        /// not realised orders
        /// </summary>
        /// <returns>List of current orders</returns>
        public static List<Zamowienie> getCurrentOrders() {
            return cateringContext.Orders.Where(o => o.status_zamowienia != "w realizacji" && o.status_zamowienia != "anulowano").ToList();
        }

        /// <summary>
        /// Method to returns client 
        /// specified by id 
        /// </summary>
        /// <param name="id">client's id</param>
        /// <returns>client specified by id</returns>
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
            Decimal suma = 0;
            var eAddress = orderDetails.address;
            Klient klient = new Klient()
            {
                imie = orderDetails.client.name,
                nazwisko = orderDetails.client.lastName,
                email = orderDetails.client.email,
                nr_tel = orderDetails.client.phone.ToString(),
                nr_domu = eAddress.nrDomu,
                kod_pocztowy = eAddress.kodPocztowy,
                miasto = eAddress.miasto,
                nr_mieszkania = eAddress.nrMieszkania,
                ulica = eAddress.ulica
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
                suma += dish.price;
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
                cateringContext.Clients.Update(klienciWBazie[0]);
            }
            Zamowienie zamowienie = new Zamowienie() { klient = klient, status_zamowienia="Złożone", suma = suma, data_zamowienia = DateTime.Parse(orderDetails.orderTime), cyklicznosc = (short)orderDetails.periodicity, preferowana_pora=orderDetails.timePreference, data_dostarczenia=DateTime.Parse(orderDetails.orderDeliveredTime)};
            DanieZamowienie danieZamowienie = new DanieZamowienie();
            foreach (String nazwaDania in danieILiczbaDan.Keys) {
                danieZamowienie = new DanieZamowienie() { danie = cateringContext.Dishes.Where(s => (s.nazwa == nazwaDania)).ToList()[0], zamowienie = zamowienie, ilosc_dania= danieILiczbaDan[nazwaDania]};
                zamowienie.daniaZamowienia.Add(danieZamowienie);
            }
            cateringContext.Orders.Add(zamowienie);
            cateringContext.SaveChanges();
            var orderHub = new Extentions.SignalR.OrderHub();
            orderHub.sendNewOrder(orderDetails);
            Extentions.MailKit.Mail.newOrder(klient);
        }

        public static String putAccount(Account account) {
            cateringContext.Database.EnsureCreated();
            if (cateringContext.Accounts.Where(a => a.login == account.login).ToList().Count != 0) {
                return "Account's login already used";
            }
            else {
                Klient klient = new Klient()
                {
                    imie = account.client.name,
                    nazwisko = account.client.lastName,
                    email = account.client.email,
                    nr_tel = account.client.phone.ToString(),
                    nr_domu = account.address.nrDomu,
                    kod_pocztowy = account.address.kodPocztowy,
                    miasto = account.address.miasto,
                    nr_mieszkania = account.address.nrMieszkania,
                    ulica = account.address.ulica
                };
                Konto konto = new Konto() { login = account.login, haslo = account.haslo, klient = klient };
                cateringContext.Accounts.Add(konto);
                cateringContext.SaveChanges();
                Extentions.MailKit.Mail.newAccount(konto);
                return "done";
            }
        }

        public static List<Konto> getAccounts() {
            return cateringContext.Accounts.ToList();
        }
    }
}
