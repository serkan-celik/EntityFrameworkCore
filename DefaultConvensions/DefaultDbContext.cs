using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.DefaultConvensions
{
    public class FluentDbContext : DbContext
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

    #region One to One Relation

    //Princible Table
    public class Musteri
    {
        //Auto PrimaryKey
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadı { get; set; }
    }

    //Dependent Table
    public class Kullanici
    {
        //Auto PrimaryKey
        public int Id { get; set; }
        //Shadow property automatic geneterated
        //public int MusteriId { get; set; }
        public string Adi { get; set; }
        public string Sifre { get; set; }
        public Musteri Musteri { get; set; }
    }

    #endregion

    #region One to Many Relation

    public class Siparis
    {
        //Auto PrimaryKey
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public DateTime Tarih { get; set; }
        public double ToplamTutar { get; set; }
        public ICollection<SiparisDetay> SiparisDetaylari { get; set; }//Opsiyonel
    }
    public class SiparisDetay
    {
        //Id AutoKey
        public int Id { get; set; }
        //Shadow property automatic geneterated
        //public int SiparisId { get; set; }
        public int Adet { get; set; }
        public int Tutar { get; set; }
        public Siparis Siparis { get; set; }
        public Urun Urun { get; set; }
    }

    #endregion

    #region Many to Many Relation

    //Princible Table
    public class Kategori
    {
        //Auto PrimaryKey
        public int Id { get; set; }
        public string Adi { get; set; }
        public ICollection<Urun> Urunler { get; set; }
    }

    //KategoriUrun Dependent Table Automatic Generated

    //Princible Table
    public class Urun
    {
        //Auto PrimaryKey
        public int Id { get; set; }
        public string Adi { get; set; }
        public double Fiyat { get; set; }
        public string Aciklama { get; set; }
        public ICollection<Kategori> Kategoriler { get; set; }
    }

    #endregion
}
