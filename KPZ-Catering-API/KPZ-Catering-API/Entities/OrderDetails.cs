using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Entities
{
    public class OrderDetails
    {
        public Client client{ get; set; }
        public List<Dish> dishes { get; set; }
        public double sum { get; set; }
    }
}
