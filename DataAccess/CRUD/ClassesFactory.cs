using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class ClassesFactory : CrudFactory
    {
        public override void Create(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        public override void DeleteByID(int id)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }


        public override void Update(BaseDTO baseDTO, int? id)
        {
            throw new NotImplementedException();
        }
    }
}
