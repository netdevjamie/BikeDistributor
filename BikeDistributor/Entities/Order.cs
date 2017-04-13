namespace BikeDistributor
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Text;

    [Table("Order")]
    public partial class Order
    {

        private const double TaxRate = .0725d;
        private readonly IList<ReceiptLineItem> _lines = new List<ReceiptLineItem>();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string OrderNumber { get; set; }

        public virtual Company Company { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }

        public virtual Receipt Receipt { get; set; }

        public Order() { }

        public Order(Company company)
        {
            Company = company;
            OrderNumber = Guid.NewGuid().ToString();
        }

        public void AddLine(ReceiptLineItem line)
        {
            _lines.Add(line);
        }

        public Receipt GetReceipt(PlaceOrderViewModel model, int quantity)
        {
            _lines.Add(new ReceiptLineItem(new Bike(model.Brand,model.Model,"",model.Price), quantity));
            var receipt = new Receipt();
            var totalAmount = 0d;
            var result = new StringBuilder(string.Format("Order Receipt for {0}{1}", model.Order.Company.Name, Environment.NewLine));
            foreach (var line in _lines)
            {
                var thisAmount = GetThisAmount(line);
                result.AppendLine(string.Format("\t{0} x {1} {2} = {3}", quantity, model.Brand, model.Model, thisAmount.ToString("C")));
                totalAmount += thisAmount;
            }
            result.AppendLine(string.Format("Sub-Total: {0}", totalAmount.ToString("C")));
            var tax = totalAmount * TaxRate;
            result.AppendLine(string.Format("Tax: {0}", tax.ToString("C")));
            result.Append(string.Format("Total: {0}", (totalAmount + tax).ToString("C")));
            Receipt = receipt;
            Receipt.Body = result.ToString();
            return Receipt;
        }
        public Receipt GetReceipt()
        {
            var receipt = new Receipt();
            var totalAmount = 0d;
            var result = new StringBuilder(string.Format("Order Receipt for {0}{1}", Company.Name, Environment.NewLine));
            foreach (var line in _lines)
            {
                var thisAmount = GetThisAmount(line);
                result.AppendLine(string.Format("\t{0} x {1} {2} = {3}", line.Quantity, line.Bike.Brand, line.Bike.Model, thisAmount.ToString("C")));
                totalAmount += thisAmount;
            }
            result.AppendLine(string.Format("Sub-Total: {0}", totalAmount.ToString("C")));
            var tax = totalAmount * TaxRate;
            result.AppendLine(string.Format("Tax: {0}", tax.ToString("C")));
            result.Append(string.Format("Total: {0}", (totalAmount + tax).ToString("C")));
            Receipt = receipt;
            Receipt.Body = result.ToString();
            return Receipt;
        }

        public double GetThisAmount(ReceiptLineItem line)
        {
            var thisAmount = 0d;

            if (line.Bike.Price == Bike.OneThousand && line.Quantity >= 20)
                thisAmount += Convert.ToDouble(line.Quantity * line.Bike.Price) * .9d;
            else if (line.Bike.Price == Bike.TwoThousand && line.Quantity >= 10)
                thisAmount += Convert.ToDouble(line.Quantity * line.Bike.Price) * .8d;
            else if (line.Bike.Price == Bike.FiveThousand && line.Quantity >= 5)
                thisAmount += Convert.ToDouble(line.Quantity * line.Bike.Price) * .8d;
            else
                thisAmount += Convert.ToDouble(line.Quantity * line.Bike.Price);
            return thisAmount;
        }

        public Receipt GetReceiptAsHtml()
        {
            var receipt = new Receipt();
            var totalAmount = 0d;
            var result = new StringBuilder(string.Format("<html><body><h1>Order Receipt for {0}</h1>", Company.Name));
            if (_lines.Any())
            {
                result.Append("<ul>");
                foreach (var line in _lines)
                {
                    var thisAmount = GetThisAmount(line);
                    result.Append(string.Format("<li>{0} x {1} {2} = {3}</li>", line.Quantity, line.Bike.Brand, line.Bike.Model, thisAmount.ToString("C")));
                    totalAmount += thisAmount;
                }
                result.Append("</ul>");
            }
            result.Append(string.Format("<h3>Sub-Total: {0}</h3>", totalAmount.ToString("C")));
            var tax = totalAmount * TaxRate;
            result.Append(string.Format("<h3>Tax: {0}</h3>", tax.ToString("C")));
            result.Append(string.Format("<h2>Total: {0}</h2>", (totalAmount + tax).ToString("C")));
            result.Append("</body></html>");
            Receipt = receipt;
            Receipt.Body = result.ToString();
            return Receipt;
        }

        public Receipt GetReceiptAsHtml(PlaceOrderViewModel model, int quantity)
        {
            _lines.Add(new ReceiptLineItem(new Bike(model.Brand, model.Model, "", model.Price), quantity));
            var receipt = new Receipt();
            var totalAmount = 0d;
            var result = new StringBuilder(string.Format("<html><body><h1>Order Receipt for {0}</h1>", Company.Name));
            if (_lines.Any())
            {
                result.Append("<ul>");
                foreach (var line in _lines)
                {
                    var thisAmount = GetThisAmount(line);
                    result.Append(string.Format("<li>{0} x {1} {2} = {3}</li>", line.Quantity, line.Bike.Brand, line.Bike.Model, thisAmount.ToString("C")));
                    totalAmount += thisAmount;
                }
                result.Append("</ul>");
            }
            result.Append(string.Format("<h3>Sub-Total: {0}</h3>", totalAmount.ToString("C")));
            var tax = totalAmount * TaxRate;
            result.Append(string.Format("<h3>Tax: {0}</h3>", tax.ToString("C")));
            result.Append(string.Format("<h2>Total: {0}</h2>", (totalAmount + tax).ToString("C")));
            result.Append("</body></html>");
            Receipt = receipt;
            Receipt.Body = result.ToString();
            return Receipt;
        }

    }
}
