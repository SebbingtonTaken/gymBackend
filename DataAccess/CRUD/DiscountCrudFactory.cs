using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class DiscountCrudFactory : CrudFactory
    {
        public DiscountCrudFactory()
        {
            sqlDao = SqlDao.GetInstance();
        }
        public override void Create(BaseDTO baseDTO)
        {
            var disc = baseDTO as Discount;

            //Crear instructivo para que el DAO pueda realizar un create de la base de datos

            var sqlOperation = new SqlOperation();

            // set del nombre del procedimiento


           

            sqlOperation.ProcedureName = "INS_DISCOUNTS_PR";

            sqlOperation.AddIntParameter("discount_percentage", disc.DiscountPercentage);

            sqlOperation.AddStringParameter("user_category", disc.UserCategory);

            sqlOperation.AddDateTimeParameter("start_date", disc.StartDate);

            sqlOperation.AddDateTimeParameter("end_date", disc.EndDate);

            sqlOperation.AddCharParameter("is_active", disc.IsActive);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void DeleteByID(int id)
        {
            var sqlOperation = new SqlOperation();

            sqlOperation.AddIntParameter("@P_ID", id);
            sqlOperation.ProcedureName = "DEL_DISCOUNT_BY_ID_PR";
            sqlDao.ExecuteProcedure(sqlOperation);
        }


        public override List<T> RetrieveAll<T>()
        {
            var lstDiscounts = new List<T>();
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "RET_ALL_DISCOUNTS_PR";
            var lstResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var coupon = BuildDiscount(row);
                    lstDiscounts.Add((T)Convert.ChangeType(coupon, typeof(T)));
                }
            }
            return lstDiscounts;
        }



        public  T RetrieveById<T>(int id)
        {

            var lstDiscounts = new List<T>();
            var sqlOperation = new SqlOperation();

            sqlOperation.AddIntParameter("discount_id", id);
            sqlOperation.ProcedureName = "RET_DISCOUNT_BY_ID_PR";
            var lstResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                var row = lstResults[0];

                var retDiscount = (T)Convert.ChangeType(BuildDiscount(row), typeof(T));
                return retDiscount;
            }
            return default(T);
        }

      
    

    public override void Update(BaseDTO baseDTO, int? id)
        {
            var disc = baseDTO as Discount;

            //Crear instructivo para que el DAO pueda realizar un create de la base de datos

            var sqlOperation = new SqlOperation();

            // set del nombre del procedimiento




            sqlOperation.ProcedureName = "UP_DISCOUNTS_PR";


            sqlOperation.AddIntParameter("discount_id", disc.Id);

            sqlOperation.AddIntParameter("discount_percentage", disc.DiscountPercentage);

            sqlOperation.AddStringParameter("user_category", disc.UserCategory);

            sqlOperation.AddDateTimeParameter("start_date", disc.StartDate);

            sqlOperation.AddDateTimeParameter("end_date", disc.EndDate);

            sqlOperation.AddCharParameter("is_active", disc.IsActive);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        private Discount BuildDiscount(Dictionary<string, object> row)
        {
            var disc = new Discount()
            {
                Id = (int)row["discount_id"],
                DiscountPercentage = (int)row["discount_percentage"],
                UserCategory = (string)row["user_category"],
                StartDate = (DateTime)row["start_date"],
                EndDate = (DateTime)row["end_date"],
                CreationDate = (DateTime)row["creation_date"],
                IsActive = Convert.ToChar(row["is_active"])
            };
            return disc;
        }
    }
}
