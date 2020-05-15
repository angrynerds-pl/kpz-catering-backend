using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Entities
{
    public class Dish
    {
        public String name { get; set; }
        public Decimal price { get; set; }
        public String description { get; set; }
        public int? count { get; set; }
    }
}
