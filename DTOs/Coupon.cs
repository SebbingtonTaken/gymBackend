using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class Coupon : BaseDTO
    {
        public String CouponCode { get; set; }

        public int DiscountPercentage { get; set; }

        public String UserCategory {  get; set; }

        public DateTime ExpiryDate { get; set; }

        public String State { get; set; }
    }
}
