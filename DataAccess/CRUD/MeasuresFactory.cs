using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class MeasuresCrudFactory : CrudFactory
    {
        public MeasuresCrudFactory()
        {
            sqlDao = SqlDao.GetInstance();
        }
        public override void Create(BaseDTO baseDTO)
        {
            var measure = baseDTO as Measure;
            var sqlOperation = new SqlOperation();

            sqlOperation.ProcedureName = "INS_MEASURES_PR";
            sqlOperation.AddIntParameter("user_id", measure.UserId);
            sqlOperation.AddStringParameter("weight", measure.Weight);
            sqlOperation.AddStringParameter("height", measure.Height);
            sqlOperation.AddStringParameter("fat_percentage", measure.FatPercentage);
            sqlOperation.AddDateTimeParameter("measure_date", measure.MeasureDate);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void DeleteByID(int id)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var measureList = new List<T>();
            var sqlOperation = new SqlOperation()
            {
                ProcedureName = "RET_ALL_USER_MEASURES_PR"
            };
            var listResult = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResult.Count > 0)
            {
                foreach (var row in listResult)
                {
                    var measure = BuildMeasure(row);
                    measureList.Add((T)Convert.ChangeType(measure, typeof(T)));
                }
            }
            return measureList;
        }



        public  T RetrieveAllMeasuresById<T>(int id)
        {
            var sqlOperation = new SqlOperation()
            {
                ProcedureName = "RET_USER_MEASURES_BY_USERID_PR"
            };
            sqlOperation.AddIntParameter("@UserId", id);

            var listResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0)
            {
                var row = listResults[0];
                var equipment = (T)Convert.ChangeType(BuildMeasure(row), typeof(T));
                return equipment;
            }
            return default(T);
        }

        public  List<T> RetrieveAllMeasuresByUserId<T>(int id)
        {
            var measureList = new List<T>();
            var sqlOperation = new SqlOperation()
            {
                ProcedureName = "RET_USER_MEASURES_BY_USERID_PR"
            };
            sqlOperation.AddIntParameter("@UserId", id);

            var listResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0)
            {
                foreach (var row in listResults)
                {
                    var measure = BuildMeasure(row);
                    measureList.Add((T)Convert.ChangeType(measure, typeof(T)));
                }
            }
            return measureList;
        }


        public override void Update(BaseDTO baseDTO, int? id)
        {
            var measure = baseDTO as Measure;
            var sqlOperation = new SqlOperation();

            sqlOperation.ProcedureName = "UP_MEASURES_PR";
            sqlOperation.AddIntParameter("measures_user_id", measure.Id);
            sqlOperation.AddIntParameter("user_id", measure.UserId);
            sqlOperation.AddStringParameter("weight", measure.Weight);
            sqlOperation.AddStringParameter("height", measure.Height);
            sqlOperation.AddStringParameter("fat_percentage", measure.FatPercentage);
            sqlOperation.AddDateTimeParameter("measure_date", measure.MeasureDate);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        public List<T> RetrieveByMonth<T>(int month, int userId)
        {
            var measureList = new List<T>();
            var sqlOperation = new SqlOperation
            {
                ProcedureName = "RET_USER_MEASURES_BY_MONTH_PR"
            };
            sqlOperation.AddIntParameter("@Month", month);
            sqlOperation.AddIntParameter("@UserID", userId);

            var listResult = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResult.Count > 0)
            {
                foreach (var row in listResult)
                {
                    var measure = BuildMeasure(row);
                    measureList.Add((T)Convert.ChangeType(measure, typeof(T)));
                }
            }
            return measureList;
        }

        private Measure BuildMeasure(Dictionary<string, object> row)
        {
            var measure = new Measure()
            {
                Id = (int)row["MeasureID"],
                UserId = (int)row["UserID"],
                Weight = (string)row["weight"],
                Height = (string)row["height"],
                FatPercentage = (string)row["FatPercentage"],
                MeasureDate = (DateTime)row["MeasureDate"]
            };
            return measure;
        }
    }
}
