﻿using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EntityFrameworkCore
{
    internal class Program
    {
        [Benchmark]
        static void Main(string[] args)
        {
            var ctx = new EfDb();
            //ctx.Kategoriler.AddRange(new Kategori { Adi = "a" }, new Kategori { Adi = "a1" },new Kategori { Adi = "a2" });
            //ctx.SaveChanges();
            //var kategoriler = ctx.Database.ExecuteSql($"");

            ctx.Siparisler.Remove(new Siparis { Id = 6 });
            ctx.SaveChanges();

            Console.WriteLine("işlem başarılı");
        }
    }

    public class EfDb :DbContext
    {
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<SiparisDetay> SiparisDetaylari { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Kisi> Kisiler { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=BTGM0306-703\\SQLEXPRESS; Database=EfTestDb; Integrated Security=True;  TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Kategori>()
            //    .HasOne(k => k.UstKategori)
            //    .WithMany(k => k.AltKategoriler);
            //.HasForeignKey(k => k.UstKategoriId);

            //modelBuilder.Entity<KategoriUrun>().HasKey("KategoriId","UrunId");
            //modelBuilder.Entity<KategoriUrun>().HasKey(k=>new { k.KategoriId, k.UrunId });
            //modelBuilder.Entity<KategoriUrun>().ToTable("KategoriUrunleri");

            modelBuilder.Entity<SiparisDetay>().HasIndex(sd => new {sd.SiparisId, sd.UrunId }).IsUnique();
            modelBuilder.Entity<SiparisDetay>()
                        .HasOne(sd => sd.Siparis)
                        .WithMany(sd => sd.SiparisDetaylari)
                        .OnDelete(DeleteBehavior.Cascade);
        }


        public void Yap(T entitiy) { 

        }
    }

    public class Kategori {
        public int Id { get; set; }
        public int? UstKategoriId { get; set; }
        public string Adi { get; set; }
        public Kategori UstKategori { get; set; }
        public ICollection<Kategori> AltKategoriler { get; set; }
        public ICollection<Urun> Urunler { get; set; }
    }


    public class KategoriUrun
    {
        public int KategoriId { get; set; }
        public int UrunId { get; set; }
        public Kategori Kategori { get; set; }
        public Urun Urun { get; set; }
    }

    public class Urun
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public double Fiyat { get; set; }
        public string Aciklama { get; set; }
        public ICollection<Kategori> Kategoriler { get; set; }
    }

    public class Siparis {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public DateTime Tarih { get; set; }
        public double ToplamTutar { get; set; }
        public ICollection<SiparisDetay> SiparisDetaylari { get; set; }
    }
    public class SiparisDetay
    {
        public int Id { get; set; }
        public int SiparisId { get; set; }
        public int UrunId { get; set; }
        public int Adet { get; set; }
        public int Tutar { get; set; }
        public Siparis Siparis { get; set; }
        public Urun Urun { get; set; }
    }

    public interface IKisi<T>
    {

    }

    public class Kisi
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string TcKimlikNo { get; set; }
        public Musteri Musteri { get; set; }
        public Personel Personel { get; set; }
    }

    public class Musteri
    {
        public int Id { get; set; }
        public string No { get; set; }
        [ForeignKey("Id")]
        public Kisi Kisi { get; set; }
        public Kullanici Kullanici { get; set; }
    }

    public class Personel 
    {
        public int Id { get; set; }
        public int Birim { get; set; }
        [ForeignKey("Id")]
        public Kisi Kisi { get; set; }
        public Kullanici Kullanici { get; set; }
    }

    public class Kullanici 
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Sifre { get; set; }
        [ForeignKey("Id")]
        public Musteri Musteri { get; set; }
        [ForeignKey("Id")]
        public Personel Personel { get; set; }
    }

}