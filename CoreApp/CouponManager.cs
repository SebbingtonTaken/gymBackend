using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class CouponManager
    {
        public void Create(Coupon coup)
        {
            CouponCrudFactory factory = new CouponCrudFactory();

            coup.CouponCode = GenerateCouponCode();

            factory.Create(coup);
        }

        public string GenerateCouponCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            CouponCrudFactory ccrud = new CouponCrudFactory();

            const int otpLength = 8;

            //genera codigo random
            Random random = new Random();
            while (true)
            {
                string code = new string(Enumerable.Repeat(chars, otpLength)
                 .Select(s => s[random.Next(s.Length)]).ToArray());

                if (ccrud.RetrieveByCode<Coupon>(code) == null)
                {
                    return code;
                }
            }
        }

        public void Update(Coupon coupon)
        {
            var factory = new CouponCrudFactory();

            factory.Update(coupon, coupon.Id);
        }
        public void Delete(int id)
        {
        }
        public List<Coupon> RetrieveAll()
        {

            var factory = new CouponCrudFactory();

            return factory.RetrieveAll<Coupon>();


        }
        public Coupon RetrieveByCouponCode(String code)
        {
            var factory = new CouponCrudFactory();

            return factory.RetrieveByCode<Coupon>(code);
        }

    }
}
