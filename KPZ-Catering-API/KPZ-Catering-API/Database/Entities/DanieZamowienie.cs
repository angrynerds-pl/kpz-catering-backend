using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Database.Entities
{
    [Table("dania_zamowienia", Schema = "catering")]
    public class DanieZamowienie
    {
        public Zamowienie zamowienie { get; set; }
        public long zamowienie_zamowienie_id { get; set; }
        public Danie danie { get; set; }
        public long danie_danie_id { get; set; }

    }
}
