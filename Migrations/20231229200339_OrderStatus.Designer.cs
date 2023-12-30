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
    [Migration("20231229200339_OrderStatus")]
    partial class OrderStatus
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

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime");

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

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<byte[]>("Password")
                        .HasColumnType("binary(128)");

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

                    b.HasIndex("ClientId");

                    b.ToTable("ClientLogin");
                });

            modelBuilder.Entity("AuFood.Models.Client_ClientLogin", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("ClientLoginId")
                        .HasColumnType("int");

                    b.HasKey("ClientId", "ClientLoginId");

                    b.HasIndex("ClientLoginId");

                    b.ToTable("Client_ClientLogin");
                });

            modelBuilder.Entity("AuFood.Models.Consumer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<string>("Email")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<byte[]>("Password")
                        .HasMaxLength(128)
                        .HasColumnType("binary(128)")
                        .IsFixedLength();

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<ulong>("PhoneConfirmed")
                        .HasColumnType("bit(1)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("Consumer");
                });

            modelBuilder.Entity("AuFood.Models.ConsumerAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Complement")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("ConsumerId")
                        .HasColumnType("int");

                    b.Property<string>("Neighborhood")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Number")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Street")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int?>("ZipCode")
                        .HasColumnType("int(8)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("CityId");

                    b.HasIndex("ConsumerId");

                    b.ToTable("ConsumerAddress");
                });

            modelBuilder.Entity("AuFood.Models.ConsumerStore", b =>
                {
                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<int>("ConsumerId")
                        .HasColumnType("int");

                    b.HasKey("StoreId", "ConsumerId");

                    b.HasIndex("ConsumerId");

                    b.ToTable("ConsumerStore");
                });

            modelBuilder.Entity("AuFood.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ConsumerAddressId")
                        .HasColumnType("int");

                    b.Property<int>("ConsumerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<int>("DeliveryMethod")
                        .HasColumnType("int(2)");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int(2)");

                    b.Property<int?>("Status")
                        .HasColumnType("int(2)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("double");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("ConsumerAddressId");

                    b.HasIndex("ConsumerId");

                    b.HasIndex("StoreId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("AuFood.Models.OrderProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderProduct");
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("ProductCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("QtdPeopleServe")
                        .HasColumnType("int(2)");

                    b.Property<int>("TimeDelivery")
                        .HasColumnType("int(3)");

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
                        .HasColumnType("double");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductPrice");
                });

            modelBuilder.Entity("AuFood.Models.ProductStore", b =>
                {
                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("StoreId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductStore");
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

                    b.Property<string>("BackgroundImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
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
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("InstagramUrl")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NumberAddress")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Phone")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Whatsapp")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("CityId");

                    b.HasIndex("ClientId");

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

            modelBuilder.Entity("AuFood.Models.StoreCategoryStore", b =>
                {
                    b.Property<int?>("StoreId")
                        .HasColumnType("int");

                    b.Property<int>("StoreCategoryId")
                        .HasColumnType("int");

                    b.HasKey("StoreId", "StoreCategoryId");

                    b.HasIndex("StoreCategoryId");

                    b.ToTable("StoreCategoryStore");
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
                        .WithMany("City")
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
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ClientLogin_Client");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("AuFood.Models.Client_ClientLogin", b =>
                {
                    b.HasOne("AuFood.Models.Client", "Client")
                        .WithMany("Client_ClientLogin")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuFood.Models.ClientLogin", "ClientLogin")
                        .WithMany("Client_ClientLogin")
                        .HasForeignKey("ClientLoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("ClientLogin");
                });

            modelBuilder.Entity("AuFood.Models.ConsumerAddress", b =>
                {
                    b.HasOne("AuFood.Models.City", "City")
                        .WithMany("ConsumerAddress")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Consumer_City");

                    b.HasOne("AuFood.Models.Consumer", "Consumer")
                        .WithMany("ConsumerAddress")
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Consumer_ConsumerAddress");

                    b.Navigation("City");

                    b.Navigation("Consumer");
                });

            modelBuilder.Entity("AuFood.Models.ConsumerStore", b =>
                {
                    b.HasOne("AuFood.Models.Consumer", "Consumer")
                        .WithMany("ConsumerStore")
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuFood.Models.Store", "Store")
                        .WithMany("ConsumerStore")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consumer");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("AuFood.Models.Order", b =>
                {
                    b.HasOne("AuFood.Models.ConsumerAddress", "ConsumerAddress")
                        .WithMany("Order")
                        .HasForeignKey("ConsumerAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Order_ConsumerAdress");

                    b.HasOne("AuFood.Models.Consumer", "Consumer")
                        .WithMany("Order")
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Order_Consumer");

                    b.HasOne("AuFood.Models.Store", "Store")
                        .WithMany("Order")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Order_Store");

                    b.Navigation("Consumer");

                    b.Navigation("ConsumerAddress");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("AuFood.Models.OrderProduct", b =>
                {
                    b.HasOne("AuFood.Models.Order", "Order")
                        .WithMany("OrderProduct")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuFood.Models.Product", "Product")
                        .WithMany("OrderProduct")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
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
                        .WithMany("Product")
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

            modelBuilder.Entity("AuFood.Models.ProductStore", b =>
                {
                    b.HasOne("AuFood.Models.Product", "Product")
                        .WithMany("ProductStore")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuFood.Models.Store", "Store")
                        .WithMany("ProductStore")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("AuFood.Models.Store", b =>
                {
                    b.HasOne("AuFood.Models.City", "City")
                        .WithMany("Store")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Store_City");

                    b.HasOne("AuFood.Models.Client", "Client")
                        .WithMany("Store")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Store_Client");

                    b.Navigation("City");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("AuFood.Models.StoreCategoryStore", b =>
                {
                    b.HasOne("AuFood.Models.StoreCategory", "StoreCategory")
                        .WithMany("StoreCategoryStore")
                        .HasForeignKey("StoreCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuFood.Models.Store", "Store")
                        .WithMany("StoreCategoryStore")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");

                    b.Navigation("StoreCategory");
                });

            modelBuilder.Entity("AuFood.Models.City", b =>
                {
                    b.Navigation("ConsumerAddress");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("AuFood.Models.Client", b =>
                {
                    b.Navigation("Client_ClientLogin");

                    b.Navigation("ClientsLogin");

                    b.Navigation("Products");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("AuFood.Models.ClientLogin", b =>
                {
                    b.Navigation("Client_ClientLogin");
                });

            modelBuilder.Entity("AuFood.Models.Consumer", b =>
                {
                    b.Navigation("ConsumerAddress");

                    b.Navigation("ConsumerStore");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("AuFood.Models.ConsumerAddress", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("AuFood.Models.Order", b =>
                {
                    b.Navigation("OrderProduct");
                });

            modelBuilder.Entity("AuFood.Models.Product", b =>
                {
                    b.Navigation("OrderProduct");

                    b.Navigation("ProductStore");

                    b.Navigation("ProductsPrice");
                });

            modelBuilder.Entity("AuFood.Models.ProductCategory", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("AuFood.Models.State", b =>
                {
                    b.Navigation("City");
                });

            modelBuilder.Entity("AuFood.Models.Store", b =>
                {
                    b.Navigation("AvaliationsStories");

                    b.Navigation("ConsumerStore");

                    b.Navigation("Order");

                    b.Navigation("ProductStore");

                    b.Navigation("StoreCategoryStore");
                });

            modelBuilder.Entity("AuFood.Models.StoreCategory", b =>
                {
                    b.Navigation("StoreCategoryStore");
                });
#pragma warning restore 612, 618
        }
    }
}
