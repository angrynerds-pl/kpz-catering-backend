using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Database.Entities
{
    [Table("konta", Schema = "catering")]
    public class Konto
    {
        public Int64 konto_id { get; set; }
        public String login { get; set; }
        public String haslo { get; set; }
        [ForeignKey("klient")]
        public Int64 klienci_klient_id { get; set; }
        public Klient klient { get; set; }
    }
}
