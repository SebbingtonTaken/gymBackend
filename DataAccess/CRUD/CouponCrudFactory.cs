using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class CouponCrudFactory : CrudFactory
    {
        public CouponCrudFactory()
        {
            sqlDao = SqlDao.GetInstance();
        }
        public override void Create(BaseDTO baseDTO)
        {
            var coup = baseDTO as Coupon;

            //Crear instructivo para que el DAO pueda realizar un create de la base de datos

            var sqlOperation = new SqlOperation();

            // set del nombre del procedimiento


          

            sqlOperation.ProcedureName = "INS_COUPONS_PR";

            sqlOperation.AddStringParameter("CouponCode", coup.CouponCode);

            sqlOperation.AddIntParameter("DiscountPercentage", coup.DiscountPercentage);

            sqlOperation.AddStringParameter("UserCategory", coup.UserCategory);

            sqlOperation.AddDateTimeParameter("ExpiryDate", coup.ExpiryDate);

            sqlOperation.AddStringParameter("State", coup.State);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void DeleteByID(int id)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstCoupons = new List<T>();
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "RET_ALL_COUPONS_PR";
            var lstResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var coupon = BuildCoupon(row);
                    lstCoupons.Add((T)Convert.ChangeType(coupon, typeof(T)));
                }
            }
            return lstCoupons;
        }

        public  T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();

        }

        public  T RetrieveByCode<T>(string code)
        {
            var sqlOperation = new SqlOperation();

            sqlOperation.AddStringParameter("P_CODE", code);
            sqlOperation.ProcedureName = "RET_COUPON_BY_CODE_PR";
            var lstResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                var row = lstResults[0];

                var retCoupon = (T)Convert.ChangeType(BuildCoupon(row), typeof(T));
                return retCoupon;
            }
            return default(T);
        }

        public override void Update(BaseDTO baseDTO, int? id)
        {
            var coup = baseDTO as Coupon;

            //Crear instructivo para que el DAO pueda realizar un create de la base de datos

            var sqlOperation = new SqlOperation();

            // set del nombre del procedimiento




            sqlOperation.ProcedureName = "UP_COUPONS_PR";

            sqlOperation.AddStringParameter("CouponCode", coup.CouponCode);

            sqlOperation.AddIntParameter("DiscountPercentage", coup.DiscountPercentage);

            sqlOperation.AddStringParameter("UserCategory", coup.UserCategory);

            sqlOperation.AddDateTimeParameter("ExpiryDate", coup.ExpiryDate);

            sqlOperation.AddStringParameter("State", coup.State);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        private Coupon BuildCoupon(Dictionary<string, object> row)
        {
            var coupon = new Coupon()
            {
                CouponCode = (string)row["coupon_code"],
                DiscountPercentage = (int)row["discount_percentage"],
                UserCategory = (string)row["user_category"],
                ExpiryDate = (DateTime)row["expiry_date"],
                CreationDate = (DateTime)row["creation_date"],
                State = (string)row["state"],
            };
            return coupon;
        }
    }
}
