using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
        public class Customer:User
    {
        public Membership UserMembership { get; set; }

        public char IsVerified { get; set; }

        public string ConfirmationMethod { get; set; }
    }

}
