using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class Membership:BaseDTO
    {
        public string MembershipName { get; set; }

        public double MembershipPrice { get; set; }
        public int PersonalClassesInclude { get; set; }
        public int GroupClassesInclude { get; set; }
        public double RegistrationFee { get; set; }
    }
}