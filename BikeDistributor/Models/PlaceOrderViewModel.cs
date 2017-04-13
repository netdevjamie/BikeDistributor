using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeDistributor.Models
{
    public class PlaceOrderViewModel
    {

        public PlaceOrderViewModel()
        {
            Order = new Order();
            Order.OrderNumber = Guid.NewGuid().ToString();
            Order.Company = new Company();
            Receipt = new Receipt();
            Receipt.OrderNumber = Order.OrderNumber;

            Brand = "";
            Model = "";
            BikeSize = "";
            PaymentMethod = "";
            Price = 0.00M;
        }


        public int Id { get; set; }
        public Order Order { get; set; }
        public Receipt Receipt { get; set; }

        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string BikeSize { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }


        public IEnumerable<SelectListItem> Bikes { get; set; }
    }
}