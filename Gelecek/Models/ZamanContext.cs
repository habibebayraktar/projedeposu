using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gelecek.Models
{
    public class ZamanContext: DbContext
    {

        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<Posta> Postalar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-7JPVSVV\SQLEXPRESS;Initial Catalog=Time; Integrated Security= true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Uye>().ToTable("TblUyeler");
            modelBuilder.Entity<Uye>().Property(o => o.Ad).HasColumnType("varchar(50)").IsRequired();
            modelBuilder.Entity<Uye>().Property(o => o.Soyad).HasColumnType("varchar(75)").IsRequired();
            modelBuilder.Entity<Uye>().Property(o => o.Eposta).HasColumnType("varchar(75)").IsRequired();
            modelBuilder.Entity<Uye>().Property(o => o.Sifre).HasColumnType("varchar(45)").IsRequired(); 
            //sifre en fazla 15 karakter olsun.
            modelBuilder.Entity<Uye>().HasAlternateKey(o => o.Eposta); //unıqe olmasını sağladık


            //Posta Tbl

            modelBuilder.Entity<Posta>().ToTable("TblPostalar");
            modelBuilder.Entity<Posta>().Property(o => o.PostaAdresi).HasColumnType("varchar(75)").IsRequired();
            modelBuilder.Entity<Posta>().Property(o => o.Metin).HasColumnType("varchar(7000)").IsRequired();
            modelBuilder.Entity<Posta>().Property(o => o.MetinKonusu).HasColumnType("varchar(50)");

        }
    }
}
