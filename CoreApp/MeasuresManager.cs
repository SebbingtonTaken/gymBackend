using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class MeasureManager
    {
        public void Create(Measure measure)
        {
            var mCrud = new MeasuresCrudFactory();

            mCrud.Create(measure);
        }

        public List<Measure> RetrieveAll()
        {
            var mCrud = new MeasuresCrudFactory();
            return mCrud.RetrieveAll<Measure>();
        }

        public List <Measure> RetrieveById(int Id)
        {
            var mCrud = new MeasuresCrudFactory();
            return mCrud.RetrieveAllMeasuresByUserId<Measure>(Id);
        }

        public void Update(Measure measure)
        {
            var mCrud = new MeasuresCrudFactory();
            mCrud.Update(measure, 0);
        }

        public List<Measure> RetrieveByMonth(int month , int userId)
        {
            var mCrud = new MeasuresCrudFactory();

            return mCrud.RetrieveByMonth<Measure>(month, userId);
        }
    }
}
