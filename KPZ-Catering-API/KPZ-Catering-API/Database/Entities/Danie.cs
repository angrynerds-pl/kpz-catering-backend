using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Database.Entities
{
    [Table("dania", Schema = "catering")]
    public class Danie
    {
        [Key]
        public long danie_id { get; set; }
        public virtual ICollection<DanieZamowienie> daniaZamowienia { get; set; }
        public String nazwa { get; set; }
        public Decimal cena { get; set; }
        public String? sklad { get; set; }
    }
}
