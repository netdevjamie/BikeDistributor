namespace BikeDistributor
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BikeDistEntities : DbContext
    {
        public BikeDistEntities()
            : base("name=BikeDistEntities")
        {
        }

        public virtual DbSet<Bike> Bike { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Receipt> Receipt { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bike>()
                .Property(e => e.Brand)
                .IsUnicode(false);

            modelBuilder.Entity<Bike>()
                .Property(e => e.Model)
                .IsUnicode(false);

            modelBuilder.Entity<Bike>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Bike>()
                .Property(e => e.BikeSize)
                .IsUnicode(false);

            modelBuilder.Entity<Bike>()
                .Property(e => e.BikeType)
                .IsUnicode(false);

            modelBuilder.Entity<Bike>()
                .Property(e => e.InventoryStatus)
                .IsUnicode(false);

            modelBuilder.Entity<Bike>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Company>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.WebAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.PrimaryContact)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.PaymentMethod)
                .IsUnicode(false);

            modelBuilder.Entity<Receipt>()
                .Property(e => e.OrderNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Receipt>()
                .Property(e => e.Body)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<BikeDistributor.Models.PlaceOrderViewModel> PlaceOrderViewModels { get; set; }
    }
}
