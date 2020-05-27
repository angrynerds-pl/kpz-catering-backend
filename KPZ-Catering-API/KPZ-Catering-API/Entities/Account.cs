using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Entities
{
    public class Account
    {
        public String login { get; set; }
        public String haslo { get; set; }
        public Client? client { get; set; }
        public Address? address { get; set; }
    }
}
