using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Database.Entities
{
    [Table("konto", Schema = "catering")]
    public class Konto
    {
        [Key]
        public Int64 konto_id { get; set; }
        public Int64 klienci_klient_id { get; set; }
        public String login { get; set; }
        public String haslo { get; set; }

    }
}
