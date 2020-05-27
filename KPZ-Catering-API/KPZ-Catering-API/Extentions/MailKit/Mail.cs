using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace KPZ_Catering_API.Extentions.MailKit
{
    /// <summary>
    /// Class to send emails
    /// using smtp server
    /// </summary>
    static public class Mail
    {
        /// <summary>
        /// Method to send specified client
        /// with information about new order
        /// </summary>
        /// <param name="client">Client that we want to send an email</param>
        public static void newOrder(Database.Entities.Klient client) {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("Through eats", "througheats@catering.io");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress($"{client.imie} {client.nazwisko}", client.email);
            message.To.Add(to);

            message.Subject = "Otrzymaliśmy Twoje zamówienie";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = $"Szanowny Kliencie,\n\n" +
            $"Otrzymaliśmy Twoje zamówienie i jest ono przez nas realizowane.\n\nŻyczymy smacznego,\nZespół Througheats!";
            message.Body = bodyBuilder.ToMessageBody();

            sendEmail(message);
        }

        public static void newAccount(Database.Entities.Konto account) {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("Through eats", "througheats@catering.io");
            message.From.Add(from);

            var dbClient = KPZ_Catering_API.Database.Logic.DatabaseController.getClientById(account.klienci_klient_id);
            MailboxAddress to = new MailboxAddress($"{dbClient.imie} {dbClient.nazwisko}", dbClient.email);
            message.To.Add(to);

            message.Subject = "Utworzono nowe konto";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = $"Szanowny Kliencie,\n\n" +
            $"Zostało założone nowe konto\nDane dotyczące konta:\nLogin: {account.login}\nImię: {dbClient.imie}\nNazwisko: {dbClient.nazwisko}\n\nPozdrawiamy,\nZespół Trougheats!";
            message.Body = bodyBuilder.ToMessageBody();

            sendEmail(message);
        }
        /// <summary>
        /// Method to make smtp server 
        /// connection configuration
        /// </summary>
        /// <param name="message">mail's body</param>
        /// <param name="smtpAddress">smtp server address</param>
        public static void sendEmail(MimeMessage message) {
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("througheats", "89kop_32QFG");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
