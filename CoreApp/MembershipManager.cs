using DataAccess.CRUD;
using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class MembershipManager
    {
        private MembershipCrudFactory membershipCrudFactory;

        public MembershipManager()
        {
            membershipCrudFactory = new MembershipCrudFactory();

        }

        public  void Create(Membership membership)
        {
            membershipCrudFactory.Create(membership);
        }

        public List<Membership> RetrieveAllMemberships()
        {
            List<Membership> memberships = membershipCrudFactory.RetrieveAll<Membership>();

            return memberships;
        }

        public Membership RetrieveMembershipById(int membershipID)
        {
            return membershipCrudFactory.RetrieveAllMeasuresById<Membership>(membershipID);
        }

        public void Update(Membership membership, int? changeBy)
        {
            membershipCrudFactory.Update(membership , changeBy);
        }


    }
}
