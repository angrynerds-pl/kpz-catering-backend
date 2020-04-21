using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Database.Entities
{
    [Table("dania_zamowienia", Schema = "catering")]
    public class DaniaZamowienie {
        public Int64 zamowienie_zamowienie_id { get; set; }
        public Int64 danie_danie_id{ get; set; }
    }
}
