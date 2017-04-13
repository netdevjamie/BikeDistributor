using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor
{
    public class Enums
    {
        public enum Size
        {
            Sm, Md, Lg, XLg
        };
        public enum Type
        {
            Mtb,
            Road,
            Bmx,
            Cruiser,
            Kids,
            Other
        };
        public enum Status
        {
            Pending_Sale,
            Available
        };

        public enum PaymentMethod
        {
            Cash,
            Cheque,
            CreditCard
        };
    }
}
