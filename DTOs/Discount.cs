using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class Discount : BaseDTO
    {
        public int DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public String UserCategory { get; set; }

        public Char IsActive { get; set; }

    }
}
