using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Entities
{
    public class Address
    {
        public String ulica { get; set; }
        public String nrDomu { get; set; }
        public String? nrMieszkania { get; set; }
        public String kodPocztowy { get; set; }
        public String miasto { get; set; }
    }
}
