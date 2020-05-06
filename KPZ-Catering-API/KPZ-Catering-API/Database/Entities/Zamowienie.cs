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
        public long zamowienie_id { get; set; }
        [ForeignKey("klient")]
        public long klienci_klient_id { get; set; }
        public Klient klient { get; set; }
        public virtual ICollection<DanieZamowienie> daniaZamowienia { get; set; } = new List<DanieZamowienie>();
        public DateTime data_zamowienia { get; set; } 
        public DateTime? data_dostarczenia { get; set; }
        public Int16 cyklicznosc { get; set; } = 0;
        public String status_zamowienia { get; set; } 
        public Decimal? suma { get; set; } = 0;
    }
}
