using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using AuFood.Models;

namespace AuFood.Models
{
    public partial class _DbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        
        public DbSet<Product_price> Product_price { get; set; }

        public DbSet<Product_category> Product_category { get; set; }

        public DbSet<Login> Login { get; set; }
        
        public DbSet<Store> Store { get; set; }

        public DbSet<City> City { get; set; }

        public DbSet<State> State { get; set; }
        
        public DbSet<Consumer> Consumer { get; set; }
        
        public DbSet<Consumer_address> Consumer_address { get; set; }
        
        public DbSet<Consumer_store> Consumer_store { get; set; }
        
        public DbSet<Store_login> Store_login { get; set; }
        
        public DbSet<Order_product> Order_product { get; set; }

        public DbSet<Order> Order { get; set; }
        

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

                entity.Property(e => e.Name)
                    .HasMaxLength(30);

                entity.Property(e => e.Description)
                    .HasMaxLength(300);

                entity.Property(e => e.Image)
                    .HasColumnType("longtext");

                entity.HasOne(e => e.Product_category)
                    .WithMany(e => e.Product)
                    .HasForeignKey(e => e.Product_category_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_product_product_category");
            });

            modelBuilder.Entity<Product_price>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Price)
                    .HasColumnType("double");

                entity.Property(e => e.Day_week)
                    .HasColumnType("int(1)");

                entity.HasOne(e => e.Product)
                    .WithMany(e => e.Product_price)
                    .HasForeignKey(e => e.Product_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_product_price_product");
            });

            modelBuilder.Entity<Product_category>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Name)
                    .HasMaxLength(30);

                entity.Property(e => e.Icon)
                    .HasMaxLength(30);

                entity.Property(e => e.Image)
                    .HasColumnType("longtext");

                entity.HasMany(e => e.Product)
                    .WithOne(e => e.Product_category)
                    .HasForeignKey(e => e.Product_category_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_product_product_category");
            });

            //modelBuilder.Entity<Client>(entity =>
            //{
            //    entity.HasKey(e => e.Id)
            //        .HasName("PRIMARY");

            //    entity.Property(e => e.Name)
            //        .HasMaxLength(30);

            //    entity.Property(e => e.Created)
            //        .HasColumnType("datetime");

            //    entity.Property(e => e.Phone)
            //        .HasMaxLength(30);

            //    entity.Property(e => e.Whatsapp)
            //        .HasMaxLength(30);

            //    entity.Property(e => e.Email)
            //        .HasMaxLength(30);

            //    entity.Property(e => e.Logo)
            //        .HasColumnType("text");
            //});

            //criar tabela com os campos acima
            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Name)
                    .HasMaxLength(30);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(30);

                entity.Property(e => e.Email)
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .HasColumnType("binary(128)");

                entity.Property(e => e.Photo)
                    .HasColumnType("longtext");

                entity.Property(e => e.Profile)
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Name)
                    .HasMaxLength(30);

                entity.Property(e => e.Views)
                    .HasColumnType("int");

                entity.Property(e => e.Whatsapp)
                    .HasMaxLength(30);

                entity.Property(e => e.Email)
                    .HasMaxLength(30);
                
                entity.Property(e => e.Number_address)
                    .HasMaxLength(8);

                entity.Property(e => e.Cnpj)
                    .HasMaxLength(14);

                entity.Property(e => e.Logo)
                    .HasColumnType("longtext");
                
                entity.Property(e => e.Background_image)
                    .HasColumnType("longtext");

                entity.Property(e => e.Description)
                    .HasMaxLength(255);

                //entity.Property(e => e.TimeDelivery)
                //    .HasColumnType("double(2,2)");

                entity.HasOne(e => e.City)
                   .WithMany(e => e.Store)
                   .HasForeignKey(e => e.City_id)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_store_city");
            });

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
                    .WithMany(e => e.City)
                    .HasForeignKey(e => e.State_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_state_city");
            });

            modelBuilder.Entity<Consumer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Name)
                    .HasMaxLength(30);

                entity.Property(e => e.Phone)
                    .HasMaxLength(30);
                
                entity.Property(e => e.Code)
                    .HasMaxLength(4);

                entity.Property(e => e.Phone_confirmed)
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Email)
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Consumer_address>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Street)
                    .HasMaxLength(30);

                entity.Property(e => e.Number)
                    .HasMaxLength(8);

                entity.Property(e => e.Complement)
                    .HasMaxLength(30);

                entity.Property(e => e.ZipCode)
                    .HasColumnType("int(8)");

                entity.Property(e => e.Neighborhood)
                    .HasMaxLength(30);

                entity.HasOne(e => e.Consumer)
                    .WithMany(e => e.Consumer_address)
                    .HasForeignKey(e => e.Consumer_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_consumer_consumer_address");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Total_price)
                    .HasColumnType("double");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime");

                entity.Property(e => e.Payment_method)
                    .HasColumnType("int(2)"); 
                
                entity.Property(e => e.Delivery_method)
                    .HasColumnType("int(2)");
                
                entity.Property(e => e.Status)
                    .HasColumnType("int(2)");

                entity.HasOne(e => e.Consumer)
                    .WithMany(e => e.Order)
                    .HasForeignKey(e => e.Consumer_id)
                    .HasConstraintName("FK_order_consumer");

                entity.HasOne(e => e.Store)
                    .WithMany(e => e.Order)
                    .HasForeignKey(e => e.Store_id)
                    .HasConstraintName("FK_order_store");

                entity.HasOne(e => e.Consumer_address)
                    .WithMany(e => e.Order)
                    .HasForeignKey(e => e.Consumer_address_id)
                    .HasConstraintName("FK_order_consumer_address");
            });

            modelBuilder.Entity<Order_product>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Product_id)
                    .HasColumnType("int");

                entity.Property(e => e.Order_id)
                    .HasColumnType("int");

                entity.Property(e => e.Observation)
                    .HasMaxLength(100);

                entity.Property(e => e.Price)
                    .HasColumnType("double");

                entity.HasOne(e => e.Product)
                    .WithMany(e => e.Order_product)
                    .HasForeignKey(e => e.Product_id)
                    .HasConstraintName("FK_order_product_product");

                entity.HasOne(e => e.Order)
                    .WithMany(e => e.Order_product)
                    .HasForeignKey(e => e.Order_id)
                    .HasConstraintName("FK_order_product_order");
            });
            
            // ** Relacionamentos N x N ** //

            // ** Consumer_Store
            modelBuilder.Entity<Consumer_store>()
                 .HasKey(x => new { x.Store_id, x.Consumer_id });

            modelBuilder.Entity<Consumer_store>()
                 .HasOne(w => w.Store)
                 .WithMany(s => s.Consumer_store)
                 .HasForeignKey(w => w.Store_id);

            modelBuilder.Entity<Consumer_store>()
                 .HasOne(w => w.Consumer)
                 .WithMany(s => s.Consumer_store)
                 .HasForeignKey(w => w.Consumer_id);

            // ** Product_Store
            modelBuilder.Entity<Product_store>()
                 .HasKey(x => new { x.Store_id, x.Product_id });

            modelBuilder.Entity<Product_store>()
                 .HasOne(w => w.Store)
                 .WithMany(s => s.Product_store)
                 .HasForeignKey(w => w.Store_id);

            modelBuilder.Entity<Product_store>()
                 .HasOne(w => w.Product)
                 .WithMany(s => s.Product_store)
                 .HasForeignKey(w => w.Product_id);

            // ** Store_Login
            modelBuilder.Entity<Store_login>()
                 .HasKey(x => new { x.Store_id, x.Login_id });

            modelBuilder.Entity<Store_login>()
                 .HasOne(w => w.Store)
                 .WithMany(s => s.Store_login)
                 .HasForeignKey(w => w.Store_id);

            modelBuilder.Entity<Store_login>()
                 .HasOne(w => w.Login)
                 .WithMany(s => s.Store_login)
                 .HasForeignKey(w => w.Login_id);

            ///////////// Exemplos de uso
            
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
