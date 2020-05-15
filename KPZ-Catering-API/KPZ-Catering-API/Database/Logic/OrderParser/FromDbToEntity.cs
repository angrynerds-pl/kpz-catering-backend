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
        public static KPZ_Catering_API.Entities.Dish parseDish(Entities.Danie dbDish) {
            return new KPZ_Catering_API.Entities.Dish() { name = dbDish.nazwa, description = dbDish.sklad, price = dbDish.cena };
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
                address = dbClient.ulica.ToString() + " " + dbClient.nr_mieszkania.ToString() + "/" + dbClient.nr_domu + " " + dbClient.kod_pocztowy + " " + dbClient.miasto,
                email = dbClient.email,
                phone = int.Parse(dbClient.nr_tel)
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
            Console.WriteLine(eClient.name);
            foreach (var dish in dbOrder.daniaZamowienia) {
                eDishes.Add(parseDish(dish.danie));
            }

            return new KPZ_Catering_API.Entities.OrderDetails()
            {
                client = eClient,
                dishes = eDishes,
                sum = (double)dbOrder.suma,
                orderTime = dbOrder.data_zamowienia.ToString(),
                timePreference = dbOrder.data_dostarczenia.ToString(),
                periodicity = dbOrder.cyklicznosc,
                status = dbOrder.status_zamowienia
            };
        }
    }
}
