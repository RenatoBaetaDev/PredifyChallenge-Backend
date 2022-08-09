using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using Teste_Back_end_Predify2.Models;

namespace Teste_Back_end_Predify2.Data
{
    public class DataContext : DbContext
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;

        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            ManyToManyConfiguration(modelBuilder);
            Seed(modelBuilder);

        }

        private void ManyToManyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessSupplier>().HasKey(key => new { key.BusinessId, key.SupplierId });

            modelBuilder.Entity<BusinessSupplier>()
                .HasOne(relation => relation.Business)
                .WithMany(a => a.BusinessSupplier)
                .HasForeignKey(am => am.BusinessId);

            modelBuilder.Entity<BusinessSupplier>()
                .HasOne(relation => relation.Supplier)
                .WithMany(a => a.BusinessSupplier)
                .HasForeignKey(am => am.SupplierId);
        }
        
        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Business>().HasData(
                new Business
                {
                    Id = 1,
                    TradeName = "Nome Fantasia",
                    Cnpj = "12345678912345",
                    Uf = "São Paulo",
                },
                new Business
                {
                    Id = 2,
                    TradeName = "Fantasy Name",
                    Cnpj = "01234567891111",
                    Uf = "Joinville"
                }
            );

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier
                {
                    Id = 1,
                    Name = "Primeiro Fornecedor",
                    CpfCnpj = "12345678901444",
                    CreatedAt = DateTime.Now,

                }
            );

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier
                {
                    Id = 2,
                    Name = "Segundo Fornecedor",
                    CpfCnpj = "12345678901123",
                    CreatedAt = DateTime.Now,

                }
            );

            modelBuilder.Entity<Phone>().HasData(
                new Phone
                {
                    Id = 1,
                    Number = "(11) 99999-7777",
                    Type = "Mobile",
                    SupplierID = 1
                }
            );

            modelBuilder.Entity<BusinessSupplier>().HasData(
                new BusinessSupplier
                {
                    BusinessId = 1,
                    SupplierId = 1,
                }
            );

            modelBuilder.Entity<BusinessSupplier>().HasData(
                new BusinessSupplier
                {
                    BusinessId = 1,
                    SupplierId = 2,
                }
            );
        }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<BusinessSupplier> BusinessSuppliers { get; set; }
        public DbSet<Phone> Phones { get; set; }
    }
}
