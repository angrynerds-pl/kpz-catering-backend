using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Database.Logic.OrderParser
{
    /// <summary>
    /// Class to parse data from database to class model
    /// </summary>
    public class FromDbToEntity
    {
        /// <summary>
        /// Method to parse dish from Db to class model
        /// </summary>
        /// <param name="dbDish">Dish from database</param>
        /// <returns>Parsed dish</returns>
        public static KPZ_Catering_API.Entities.Dish parseDish(Entities.Danie dbDish, int? count) {
            return new KPZ_Catering_API.Entities.Dish() { name = dbDish.nazwa, description = dbDish.sklad, price = dbDish.cena, count = count};
        }

        /// <summary>
        /// Method to parse client from Db to class model
        /// </summary>
        /// <param name="dbClient">Client from database</param>
        /// <returns>Parsed Client</returns>
        public static KPZ_Catering_API.Entities.Client parseClient(Entities.Klient dbClient) {
            return new KPZ_Catering_API.Entities.Client()
            {
                name = dbClient.imie,
                lastName = dbClient.nazwisko,
                email = dbClient.email,
                phone = dbClient.nr_tel
            };
        }
        
        /// <summary>
        /// Method to parse OrderDetails from Db to class model
        /// </summary>
        /// <param name="dbOrder">OrderDetails from database</param>
        /// <returns>Parsed OrderDetails</returns>
        public static KPZ_Catering_API.Entities.OrderDetails parseOrderDetails(Entities.Zamowienie dbOrder) {
            var eClient = parseClient(Database.Logic.DatabaseController.getClientById(dbOrder.klienci_klient_id));
            var eDishes = new List<KPZ_Catering_API.Entities.Dish>();
            //Console.WriteLine(dbOrder.daniaZamowienia.Count);
            foreach (var dishOrders in DatabaseController.getDishesOrderById(dbOrder.zamowienie_id)) {
                eDishes.Add(parseDish(DatabaseController.getDishById(dishOrders.danie_danie_id), dishOrders.ilosc_dania));
            }

            return new KPZ_Catering_API.Entities.OrderDetails()
            {
                client = eClient,
                dishes = eDishes,
                sum = (double)dbOrder.suma,
                orderTime = dbOrder.data_zamowienia.ToString(),
                periodicity = dbOrder.cyklicznosc,
                status = dbOrder.status_zamowienia,
                timePreference = dbOrder.preferowana_pora,
                orderDeliveredTime = dbOrder.data_dostarczenia.ToString(),
                address = new KPZ_Catering_API.Entities.Address() 
                { 
                kodPocztowy = dbOrder.klient.kod_pocztowy,
                miasto = dbOrder.klient.miasto,
                ulica = dbOrder.klient.ulica,
                nrDomu = dbOrder.klient.nr_domu,
                nrMieszkania = dbOrder.klient.nr_mieszkania
                }
            };
        }
    }
}
