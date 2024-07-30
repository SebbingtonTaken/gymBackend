using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class DiscountManager
    {
        public void Create(Discount discount)
        {
            var dCrud = new DiscountCrudFactory();

            dCrud.Create(discount);
        }

        public void Update(Discount discount)
        {
            var dCrud = new DiscountCrudFactory();

            dCrud.Update(discount, discount.Id);
        }
        public void Delete(int id)
        {
            var dCrud = new DiscountCrudFactory();
            dCrud.DeleteByID(id);
        }
        public List<Discount> RetrieveAll()
        {

            var dCrud = new DiscountCrudFactory();

            return dCrud.RetrieveAll<Discount>();


        }
        public Discount RetrieveById(int id)
        {
            var dCrud = new DiscountCrudFactory();

            return dCrud.RetrieveById<Discount>(id);
        }


    }
}
