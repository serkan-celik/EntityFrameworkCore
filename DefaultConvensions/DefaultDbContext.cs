using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.DefaultConnvensions
{
    public class DefaultDbContext : DbContext
    {
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<SiparisDetay> SiparisDetaylari { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=BTGM0306-703\\SQLEXPRESS; Database=EfTestDb; Integrated Security=True;  TrustServerCertificate=True");
        }
    }

    public class Kategori
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public ICollection<Urun> Urunler { get; set; }
    }

    public class Urun
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public double Fiyat { get; set; }
        public string Aciklama { get; set; }
        public Kategori Kategori { get; set; }
    }

    public class Siparis
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public DateTime Tarih { get; set; }
        public double ToplamTutar { get; set; }
        public ICollection<SiparisDetay> SiparisDetaylari { get; set; }
    }
    public class SiparisDetay
    {
        public int Id { get; set; }
        public int Adet { get; set; }
        public int Tutar { get; set; }
        public Siparis Siparis { get; set; }
        public Urun Urun { get; set; }
    }

    public class Musteri
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadı { get; set; }
    }

    public class Kullanici
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Sifre { get; set; }
        public Musteri Musteri { get; set; }
    }

}
