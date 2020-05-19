using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Database.Entities
{
    [Table("klienci", Schema = "catering")]
    public class Klient {
        [Key]
        public long klient_id { get; set; }
        public String imie { get; set; }
        public String nazwisko { get; set; }
        public String ulica { get; set; }
        public Int32 nr_domu { get; set; }
        public Int32? nr_mieszkania { get; set; }
        public String kod_pocztowy { get; set; } 
        public String miasto { get; set; }
        public String? nr_tel { get; set; } 
        public String email { get; set; } 
    }
}
