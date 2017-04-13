using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeDistributor.UnitTests
{
    [TestClass]
    public class OrderTest
    {
        private readonly static Bike Defy = new Bike("Giant", "Defy 1", "Sweet road bike.", Bike.OneThousand);
        private readonly static Bike Elite = new Bike("Specialized", "Venge Elite", "Spendy roadie machine.", Bike.TwoThousand);
        private readonly static Bike DuraAce = new Bike("Specialized", "S-Works Venge Dura-Ace", "Super spendy roadie machine.", Bike.FiveThousand);

        [TestMethod]
        public void ReceiptOneDefy()
        {
            var company = new Company();
            company.Name = "Anywhere Bike Shop";
            var order = new Order(company);
            order.AddLine(new ReceiptLineItem(Defy, 1));
            var result = order.GetReceipt().Body;
            Assert.AreEqual(ResultStatementOneDefy, result);
        }

        private const string ResultStatementOneDefy = @"Order Receipt for Anywhere Bike Shop
	1 x Giant Defy 1 = $1,000.00
Sub-Total: $1,000.00
Tax: $72.50
Total: $1,072.50";

        [TestMethod]
        public void ReceiptOneElite()
        {
            var company = new Company();
            company.Name = "Anywhere Bike Shop";
            var order = new Order(company);
            order.AddLine(new ReceiptLineItem(Elite, 1));
            var result = order.GetReceipt().Body;
            Assert.AreEqual(ResultStatementOneElite, result);
        }

        private const string ResultStatementOneElite = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized Venge Elite = $2,000.00
Sub-Total: $2,000.00
Tax: $145.00
Total: $2,145.00";

        [TestMethod]
        public void ReceiptOneDuraAce()
        {

            var company = new Company();
            company.Name = "Anywhere Bike Shop";
            var order = new Order(company);
            order.AddLine(new ReceiptLineItem(DuraAce, 1));
            var result = order.GetReceipt().Body;
            Assert.AreEqual(ResultStatementOneDuraAce, result);
        }

        private const string ResultStatementOneDuraAce = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized S-Works Venge Dura-Ace = $5,000.00
Sub-Total: $5,000.00
Tax: $362.50
Total: $5,362.50";

        [TestMethod]
        public void HtmlReceiptOneDefy()
        {
            var company = new Company();
            company.Name = "Anywhere Bike Shop";
            var order = new Order(company);
            order.AddLine(new ReceiptLineItem(Defy, 1));
            var htmlResult = order.GetReceiptAsHtml().Body;
            Assert.AreEqual(HtmlResultStatementOneDefy, htmlResult);
        }

        private const string HtmlResultStatementOneDefy = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Giant Defy 1 = $1,000.00</li></ul><h3>Sub-Total: $1,000.00</h3><h3>Tax: $72.50</h3><h2>Total: $1,072.50</h2></body></html>";

        [TestMethod]
        public void HtmlReceiptOneElite()
        {
            var company = new Company();
            company.Name = "Anywhere Bike Shop";
            var order = new Order(company);
            order.AddLine(new ReceiptLineItem(Elite, 1));
            var htmlResult = order.GetReceiptAsHtml().Body;
            Assert.AreEqual(HtmlResultStatementOneElite, htmlResult);
        }

        private const string HtmlResultStatementOneElite = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized Venge Elite = $2,000.00</li></ul><h3>Sub-Total: $2,000.00</h3><h3>Tax: $145.00</h3><h2>Total: $2,145.00</h2></body></html>";

        [TestMethod]
        public void HtmlReceiptOneDuraAce()
        {
            var company = new Company();
            company.Name = "Anywhere Bike Shop";
            var order = new Order(company);
            order.AddLine(new ReceiptLineItem(DuraAce, 1));
            var htmlResult = order.GetReceiptAsHtml().Body;
            Assert.AreEqual(HtmlResultStatementOneDuraAce, htmlResult);
        }

        private const string HtmlResultStatementOneDuraAce = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized S-Works Venge Dura-Ace = $5,000.00</li></ul><h3>Sub-Total: $5,000.00</h3><h3>Tax: $362.50</h3><h2>Total: $5,362.50</h2></body></html>";
    }
}
