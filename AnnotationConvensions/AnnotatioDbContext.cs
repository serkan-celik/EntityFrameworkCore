using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCoreApp.FluentConvensions
{
    public class AnnotationDbContext : DbContext
    {
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Urun> Urunler { get; set; }

        public DbSet<KategoriUrun> KategoriUrunleri { get; set; }
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
        [Key]
        //Custom PrimaryKey
        public int Musteri_Id { get; set; }
        public string Adi { get; set; }
        public string Soyadı { get; set; }
    }

    //Dependent Table
    public class Kullanici
    {
        [Key,ForeignKey(nameof(Musteri))]
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Sifre { get; set; }
        //[ForeignKey("Id")] alternatif
        public Musteri Musteri { get; set; }
    }

    #endregion

    #region One to Many Relation

    public class Siparis
    {
        [Key]
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public DateTime Tarih { get; set; }
        public double ToplamTutar { get; set; }
        public ICollection<SiparisDetay> SiparisDetaylari { get; set; }// Opsiyonel
    }
    public class SiparisDetay
    {
        public int Id { get; set; }
        public int Siparis_Id { get; set; }
        public int Urun_Id { get; set; }
        public int Adet { get; set; }
        public int Tutar { get; set; }
        [ForeignKey("Siparis_Id")]
        public Siparis Siparis { get; set; }
        [ForeignKey("Urun_Id")]
        public Urun Urun { get; set; }
    }

    #endregion

    #region Many to Many Relation

    //Princible Table
    public class Kategori
    {
        [Key]
        public int Id { get; set; }
        public string Adi { get; set; }
        public ICollection<Urun> Urunler { get; set; }//Opsiyonel
    }

    public class KategoriUrun
    {
        [Key]
        public int Id { get; set; }
        public int Kategori_Id { get; set; }
        public int Urun_Id { get; set; }
        [ForeignKey("Kategori_Id")]
        public Kategori Kategori { get; set; }
        [ForeignKey("Urun_Id")]
        public Urun Urun { get; set; }
    }

        //Princible Table
        public class Urun
    {
        [Key]
        public int Id { get; set; }
        public string Adi { get; set; }
        public double Fiyat { get; set; }
        public string Aciklama { get; set; }
        public ICollection<Kategori> Kategoriler { get; set; }//Opsiyonel
    }

    #endregion
}
