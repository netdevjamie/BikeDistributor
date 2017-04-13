namespace BikeDistributor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bike")]
    public partial class Bike
    {

        public const int OneThousand = 1000;
        public const int TwoThousand = 2000;
        public const int FiveThousand = 5000;

        public Bike() { }

        public Bike(string brand, string model, string description, decimal price)
        {
            Brand = brand;
            Model = model;
            Description = description;
            Price = price;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string BikeSize { get; set; }

        [StringLength(50)]
        public string BikeType { get; set; }

        [StringLength(50)]
        public string InventoryStatus { get; set; }

        public decimal? Price { get; set; }
        public int Quantity { get; set; }
    }
}
