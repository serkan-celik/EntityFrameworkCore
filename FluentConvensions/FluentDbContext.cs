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
    public class FluentDbContext : DbContext
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Musteri>()
                       .HasKey(m => m.Musteri_Id);

            modelBuilder.Entity<Kullanici>()
                        .HasKey(m => m.Kullanici_Id);

            modelBuilder.Entity<SiparisDetay>()
                        .HasKey(m => new { m.Siparis_Id, m.Urun_Id });

            modelBuilder.Entity<Kullanici>()
                        .HasOne(k => k.Musteri)
                        .WithOne(k => k.Kullanici)
                        .HasForeignKey<Kullanici>(k => k.Kullanici_Id)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SiparisDetay>()
                        .HasOne(k => k.Siparis)
                        .WithMany(k => k.SiparisDetaylari)
                        .HasForeignKey(sd => sd.Siparis_Id)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SiparisDetay>()
                        .HasOne(k => k.Urun)
                        .WithMany(k => k.SiparisDetaylari)
                        .HasForeignKey(sd => sd.Urun_Id)
                        .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<KategoriUrun>()
                        .HasOne(k => k.Kategori)
                        .WithMany(k => k.KategoriUrunleri)
                        .HasForeignKey(sd => sd.Kategori_Id)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KategoriUrun>()
                        .HasOne(k => k.Urun)
                        .WithMany(k => k.UrunKategorileri)
                        .HasForeignKey(sd => sd.Urun_Id)
                        .OnDelete(DeleteBehavior.Restrict);

        }
    }

    #region One to One Relation

    //Princible Table
    public class Musteri
    {
        public int Musteri_Id { get; set; }
        public string Adi { get; set; }
        public string Soyadı { get; set; }
        public Kullanici Kullanici { get; set; }
    }

    //Dependent Table
    public class Kullanici
    {
        public int Kullanici_Id { get; set; }
        public string Adi { get; set; }
        public string Sifre { get; set; }
        public Musteri Musteri { get; set; }
    }

    #endregion

    #region One to Many Relation

    //Princible Table
    public class Siparis
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public DateTime Tarih { get; set; }
        public double ToplamTutar { get; set; }
        public ICollection<SiparisDetay> SiparisDetaylari { get; set; }
    }

    //Dependent Table
    public class SiparisDetay
    {
        public int Siparis_Id { get; set; }
        public int Urun_Id { get; set; }
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
        public int Id { get; set; }
        public string Adi { get; set; }
        public ICollection<KategoriUrun> KategoriUrunleri { get; set; }
    }

    //Dependent Table
    public class KategoriUrun
    {
        public int Id { get; set; }
        public int Kategori_Id { get; set; }
        public int Urun_Id { get; set; }
        public Kategori Kategori { get; set; }
        public Urun Urun { get; set; }
    }

        //Princible Table
        public class Urun
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public double Fiyat { get; set; }
        public string Aciklama { get; set; }
        public ICollection<KategoriUrun> UrunKategorileri { get; set; }
        public ICollection<SiparisDetay> SiparisDetaylari { get; set; }
    }

    #endregion
}
