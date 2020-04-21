using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Database.Entities
{
    [Table("zamowienia", Schema = "catering")]
    public class Zamowienie
    {
        [Key]
        public Int64 zamowienie_id { get; set; }
        public Int64 klienci_klient_id { get; set; }
        public DateTime data_zamowienia { get; set; }
        public DateTime data_dostarczenia { get; set; }
        public bool cyklicznosc { get; set; }
        public String status_zamowienia { get; set; }
    }
}
