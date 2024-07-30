
using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public abstract class CrudFactory
    {
        protected SqlDao sqlDao;

        public abstract void Create(BaseDTO baseDTO);

        public abstract void Update(BaseDTO baseDTO , int? id );

        public abstract void DeleteByID(int id);

        public abstract List<T> RetrieveAll<T>();
    }
}
