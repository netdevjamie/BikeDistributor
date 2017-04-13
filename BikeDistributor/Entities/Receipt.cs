namespace BikeDistributor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Receipt")]
    public partial class Receipt
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [StringLength(150)]
        public string OrderNumber { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
