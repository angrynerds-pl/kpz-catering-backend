using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Database.Entities
{
    [Table("admini", Schema = "catering")]
    public class Admin
    {
        Int64 admin_id { get; set; }
        String login { get; set; }
        String haslo { get; set; }
        String imie { get; set; }
        String nazwisko { get; set; }
    }
}
