using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;

namespace DataAccess.CRUD
{
    public class MembershipCrudFactory : CrudFactory
    {
        public MembershipCrudFactory()
        {
            sqlDao = SqlDao.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var membership = baseDTO as Membership;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "CRE_MEMBERSHIP_PR";

            sqlOperation.AddStringParameter("membership_name", membership.MembershipName);
            sqlOperation.AddDoubleParameter("price", membership.MembershipPrice);
            sqlOperation.AddIntParameter("personal_class_n", membership.PersonalClassesInclude);
            sqlOperation.AddIntParameter("group_class_n", membership.GroupClassesInclude);
            sqlOperation.AddDoubleParameter("registration_fee", membership.RegistrationFee);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void DeleteByID(int id)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var membershipList = new List<T>();

            var sqlOperation = new SqlOperation()
            {
                ProcedureName = "RET_ALL_MEMBERSHIPS_PR"
            };

            var listResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0)
            {
                foreach (var row in listResults)
                {
                    var membership = BuildMembership(row);
                    membershipList.Add((T)Convert.ChangeType(membership, typeof(T)));
                }
            }
            return membershipList;
        }

        public  T RetrieveAllMeasuresById<T>(int id)
        {
            var sqlOperation = new SqlOperation()
            {
                ProcedureName = "RET_MEMBERSHIP_BY_ID_PR"
            };

            sqlOperation.AddIntParameter("@membership_id", id);

            var listResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0)
            {
                var row = listResults[0];
                var membership = (T)Convert.ChangeType(BuildMembership(row), typeof(T));
                return membership;
            }
            return default(T);
        }

        public override void Update(BaseDTO baseDTO, int? id )
        {
            var membership = baseDTO as Membership;
            var sqlOperation = new SqlOperation();

            sqlOperation.ProcedureName = "UP_MEMBERSHIP_PR";
            sqlOperation.AddIntParameter("employee_id", id.HasValue ? id.Value : 0);
            sqlOperation.AddIntParameter("membership_id", membership.Id);
            sqlOperation.AddStringParameter("membership_name", membership.MembershipName);
            sqlOperation.AddDoubleParameter("price", membership.MembershipPrice);
            sqlOperation.AddIntParameter("personal_class_n", membership.PersonalClassesInclude);
            sqlOperation.AddIntParameter("group_class_n", membership.GroupClassesInclude);
            sqlOperation.AddDoubleParameter("registration_fee", membership.RegistrationFee);

            sqlDao.ExecuteProcedure(sqlOperation);
        }


        private Membership BuildMembership(Dictionary<string, object> row)
        {
            var membership = new Membership()
            {
                Id = (int)row["membership_id"],
                MembershipName = (string)row["membership_name"],
                MembershipPrice = Convert.ToDouble(row["price"]),
                PersonalClassesInclude = (int)row["personal_class_n"],
                GroupClassesInclude = (int)row["group_class_n"],
                RegistrationFee = Convert.ToDouble(row["registration_fee"]),
                CreationDate = (DateTime)row["creation_date"],
            };

            return membership;
        }
    }
}
