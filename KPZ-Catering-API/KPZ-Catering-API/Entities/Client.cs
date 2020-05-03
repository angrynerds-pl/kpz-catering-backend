using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Entities
{
    public class Client
    {
        public String name { get; set; }
        public String lastName { get; set; }
        public String address { get; set; }
        public String email { get; set; }
        public int phone { get; set; }
        public String timePreference { get; set; }
    }
}
