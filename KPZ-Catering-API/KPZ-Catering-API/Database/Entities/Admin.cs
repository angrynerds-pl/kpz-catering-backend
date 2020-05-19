using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Database.Entities
{
    [Table("admini", Schema = "catering")]
    public class Admin:IdentityUser
    {
        [Key]
        public long admin_id { get; set; }
        public String login { get; set; }
        public String haslo { get; set; }
        public String? imie { get; set; }
        public String? nazwisko { get; set; }
        [NotMapped]
        public String token { get; set; }
    }
}
