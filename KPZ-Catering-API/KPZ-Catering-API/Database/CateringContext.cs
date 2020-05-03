using KPZ_Catering_API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Database
{
    public class CateringContext : DbContext
    {
        public DbSet<Danie> Dish { get; set; }
        public DbSet<Klient> Client { get; set; }
        public DbSet<Zamowienie> Order { get; set; }
        public DbSet<Konto> Account { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<DanieZamowienie> DaniaZamowienia { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(" Server = tcp:cateringsqlserver.database.windows.net,1433; Initial Catalog = cateringDatabase; Persist Security Info = False; User ID = PanPrezes; Password = 35afeXLNBV8N; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DanieZamowienie>().HasKey(e => new { e.danie_danie_id, e.zamowienie_zamowienie_id });
            modelBuilder.Entity<DanieZamowienie>()
                    .HasOne(dz => dz.danie)
                    .WithMany(d => d.daniaZamowienia)
                    .HasForeignKey(dz => dz.danie_danie_id);
            modelBuilder.Entity<DanieZamowienie>()
                    .HasOne(dz => dz.zamowienie)
                    .WithMany(z=>z.daniaZamowienia)
                    .HasForeignKey(dz => dz.zamowienie_zamowienie_id);
        }
    }
}
