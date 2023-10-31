﻿// <auto-generated />
using System;
using AuFood.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuFood.Migrations
{
    [DbContext(typeof(_DbContext))]
    [Migration("20231027165823_NewColorsStorfe")]
    partial class NewColorsStorfe
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8_general_ci")
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8");

            modelBuilder.Entity("AuFood.Models.AvaliationStore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Rating")
                        .HasColumnType("int(2)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("StoreId");

                    b.ToTable("AvaliationStore");
                });

            modelBuilder.Entity("AuFood.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("StateId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("AuFood.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Logo")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Whatsapp")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("AuFood.Models.ClientLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<string>("ListClient")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Profile")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("IdClient");

                    b.ToTable("ClientLogin");
                });

            modelBuilder.Entity("AuFood.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ListStoreId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("ProductCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("QtdPeopleServe")
                        .HasColumnType("int(2)");

                    b.Property<double>("TimeDelivery")
                        .HasColumnType("double(2,2)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("AuFood.Models.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("AuFood.Models.ProductPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DayWeek")
                        .HasColumnType("int(1)");

                    b.Property<double>("Price")
                        .HasColumnType("double(2,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductPrice");
                });

            modelBuilder.Entity("AuFood.Models.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("State");
                });

            modelBuilder.Entity("AuFood.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("BackgroundImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Cep")
                        .HasColumnType("int(8)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.Property<string>("ColorBackground")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ColorPrimary")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ColorSecondary")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("FacebookUrl")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("InstagramUrl")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("NumberAddress")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Whatsapp")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("CityId");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("AuFood.Models.StoreCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("StoreCategory");
                });

            modelBuilder.Entity("AuFood.Models.StoreCategoryMapping", b =>
                {
                    b.Property<int?>("StoreId")
                        .HasColumnType("int");

                    b.Property<int>("StoreCategoryId")
                        .HasColumnType("int");

                    b.HasKey("StoreId", "StoreCategoryId");

                    b.HasIndex("StoreCategoryId");

                    b.ToTable("StoreCategoryMapping");
                });

            modelBuilder.Entity("AuFood.Models.AvaliationStore", b =>
                {
                    b.HasOne("AuFood.Models.Store", "Store")
                        .WithMany("AvaliationsStories")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Avaliation_Store");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("AuFood.Models.City", b =>
                {
                    b.HasOne("AuFood.Models.State", "State")
                        .WithMany("Cities")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_State_City");

                    b.Navigation("State");
                });

            modelBuilder.Entity("AuFood.Models.ClientLogin", b =>
                {
                    b.HasOne("AuFood.Models.Client", "Client")
                        .WithMany("ClientsLogin")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ClientLogin_Client");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("AuFood.Models.Product", b =>
                {
                    b.HasOne("AuFood.Models.Client", "Client")
                        .WithMany("Products")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Client_Product");

                    b.HasOne("AuFood.Models.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Product_ProductCategory");

                    b.Navigation("Client");

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("AuFood.Models.ProductPrice", b =>
                {
                    b.HasOne("AuFood.Models.Product", "Product")
                        .WithMany("ProductsPrice")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ProductPrice_Product");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("AuFood.Models.Store", b =>
                {
                    b.HasOne("AuFood.Models.City", "City")
                        .WithMany("Stories")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Store_City");

                    b.Navigation("City");
                });

            modelBuilder.Entity("AuFood.Models.StoreCategoryMapping", b =>
                {
                    b.HasOne("AuFood.Models.StoreCategory", "StoreCategory")
                        .WithMany("StoreCategoryStores")
                        .HasForeignKey("StoreCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuFood.Models.Store", "Store")
                        .WithMany("StoreCategoryStores")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");

                    b.Navigation("StoreCategory");
                });

            modelBuilder.Entity("AuFood.Models.City", b =>
                {
                    b.Navigation("Stories");
                });

            modelBuilder.Entity("AuFood.Models.Client", b =>
                {
                    b.Navigation("ClientsLogin");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("AuFood.Models.Product", b =>
                {
                    b.Navigation("ProductsPrice");
                });

            modelBuilder.Entity("AuFood.Models.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("AuFood.Models.State", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("AuFood.Models.Store", b =>
                {
                    b.Navigation("AvaliationsStories");

                    b.Navigation("StoreCategoryStores");
                });

            modelBuilder.Entity("AuFood.Models.StoreCategory", b =>
                {
                    b.Navigation("StoreCategoryStores");
                });
#pragma warning restore 612, 618
        }
    }
}
