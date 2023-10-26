﻿using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using AuFood.Models;

namespace AuFood.Models
{                              
    public partial class _DbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        
        public DbSet<ProductPrice> ProductPrice { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<ClientLogin> ClientLogin { get; set; }
        
        public DbSet<Store> Store { get; set; }
        
        public DbSet<StoreCategory> StoreCategory { get; set; }
        public DbSet<StoreCategoryMapping> StoreCategoryMapping { get; set; }

        public DbSet<City> City { get; set; }

        public DbSet<State> State { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json")
                   .Build();

            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            var connectionString = configuration.GetConnectionString("aufood");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                opt => opt.CommandTimeout(int.MaxValue));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
              .HasCharSet("utf8");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");
                
                entity.Property(e => e.QtdPeopleServe)
                    .HasColumnType("int(2)");

                entity.Property(e => e.Name)
                    .HasMaxLength(30);

                entity.Property(e => e.Description)
                    .HasMaxLength(100);

                entity.Property(e => e.TimeDelivery)
                    .HasColumnType("double(2,2)");

                entity.Property(e => e.Image)
                    .HasColumnType("text");

                entity.Property(e => e.ListStoreId)
                    .HasColumnType("text");

                entity.HasOne(e => e.ProductCategory)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.IdProductCategory)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Product_ProductCategory");

                entity.HasOne(e => e.Client)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.IdClient)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Client_Product");
            });

            modelBuilder.Entity<ProductPrice>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Price)
                    .HasColumnType("double(2,2)");

                entity.Property(e => e.DayWeek)
                    .HasColumnType("int(1)");

                entity.HasOne(e => e.Product)
                    .WithMany(e => e.ProductsPrice)
                    .HasForeignKey(e => e.IdProduct)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProductPrice_Product");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Name)
                    .HasMaxLength(30);

                entity.Property(e => e.Icon)
                    .HasMaxLength(30);

                entity.Property(e => e.Image)
                    .HasColumnType("text");

                entity.HasMany(e => e.Products)
                    .WithOne(e => e.ProductCategory)
                    .HasForeignKey(e => e.IdProductCategory)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Product_ProductCategory");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Name)
                    .HasMaxLength(30);

                entity.Property(e => e.Phone)
                    .HasMaxLength(30);

                entity.Property(e => e.Whatsapp)
                    .HasMaxLength(30);

                entity.Property(e => e.Email)
                    .HasMaxLength(30);

                entity.Property(e => e.Logo)
                    .HasColumnType("text");
            });

            //criar tabela com os campos acima
            modelBuilder.Entity<ClientLogin>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Name)
                    .HasMaxLength(30);

                entity.Property(e => e.Phone)
                    .HasMaxLength(30);

                entity.Property(e => e.Email)
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .HasColumnType("text");

                entity.Property(e => e.Photo)
                    .HasColumnType("text");

                entity.Property(e => e.Profile)
                    .HasMaxLength(30);

                entity.Property(e => e.ListClient)
                    .HasColumnType("text");

                entity.HasOne(e => e.Client)
                    .WithMany(e => e.ClientsLogin)
                    .HasForeignKey(e => e.IdClient)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ClientLogin_Client");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Name)
                    .HasMaxLength(30);

                entity.Property(e => e.Phone)
                    .HasMaxLength(30);

                entity.Property(e => e.Whatsapp)
                    .HasMaxLength(30);

                entity.Property(e => e.Email)
                    .HasMaxLength(30);

                entity.Property(e => e.Street)
                    .HasMaxLength(20);
                
                entity.Property(e => e.Address)
                    .HasMaxLength(50);
                
                entity.Property(e => e.NumberAddress)
                    .HasMaxLength(8);

                entity.Property(e => e.Cep)
                    .HasColumnType("int(8)");

                entity.Property(e => e.Cnpj)
                    .HasMaxLength(14);

                entity.Property(e => e.InstagramUrl)
                    .HasMaxLength(100);
                
                entity.Property(e => e.FacebookUrl)
                    .HasMaxLength(100);

                entity.Property(e => e.Logo)
                    .HasColumnType("text");
                
                entity.Property(e => e.BackgroundImage)
                    .HasColumnType("text");

                entity.Property(e => e.Description)
                    .HasMaxLength(255);

                //entity.Property(e => e.TimeDelivery)
                //    .HasColumnType("double(2,2)");

                entity.HasOne(e => e.City)
                    .WithMany(e => e.Stories)
                    .HasForeignKey(e => e.CityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Store_City");
            });

            modelBuilder.Entity<StoreCategory>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Name)
                    .HasMaxLength(30);

                entity.Property(e => e.Icon)
                    .HasMaxLength(30);

                
            });

            modelBuilder.Entity<StoreCategoryMapping>()
           .HasKey(scs => new { scs.StoreId, scs.StoreCategoryId });

            modelBuilder.Entity<StoreCategoryMapping>()
                .HasOne(scs => scs.Store)
                .WithMany(s => s.StoreCategoryStores)
                .HasForeignKey(scs => scs.StoreId);

            modelBuilder.Entity<StoreCategoryMapping>()
                .HasOne(scs => scs.StoreCategory)
                .WithMany(sc => sc.StoreCategoryStores)
                .HasForeignKey(scs => scs.StoreCategoryId);

            //Exemplo de uso das tabelas Store StoreCategory StoreCategoryStore

            //criação:
            //var store = new Store { Name = "Minha Loja" };
            //var category = new StoreCategory { Name = "Categoria 1" };

            //var storeCategoryStore = new StoreCategoryStore
            //{
            //    Store = store,
            //    StoreCategory = category
            //};

            //context.StoreCategoryStores.Add(storeCategoryStore);
            //context.SaveChanges();

            //consulta:
            //var storesInCategory = context.StoreCategoryStores
            //    .Where(sc => sc.StoreCategoryId == categoryId)
            //    .Select(sc => sc.Store)
            //    .ToList();

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.Id)
                   .HasName("PRIMARY");

                entity.Property(e => e.Name)
                    .HasMaxLength(30);

                entity.Property(e => e.Uf)
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Name)
                    .HasMaxLength(30);
                
                entity.HasOne(e => e.State)
                    .WithMany(e => e.Cities)
                    .HasForeignKey(e => e.StateId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_State_City");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
