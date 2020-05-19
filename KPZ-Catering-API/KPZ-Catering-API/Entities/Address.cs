using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Entities
{
    public class Address
    {
        public String ulica { get; set; }
        public Int32 nrDomu { get; set; }
        public Int32? nrMieszkania { get; set; }
        public String kodPocztowy { get; set; }
        public String miasto { get; set; }
    }
}
