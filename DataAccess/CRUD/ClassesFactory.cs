using DataAccess.DAOs;
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
        public ClassesFactory()
        {
            sqlDao = SqlDao.GetInstance();
        }

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
            var trainerScheduleList = new List<T>();

            var sqlOperation = new SqlOperation()
            {
                ProcedureName = "RET_ALL_APPOINTMENTS_AVAILABILITY_PR"
            };

            var listResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0)
            {
                foreach (var row in listResults)
                {
                    var trainerSchedule = BuildTrainerSchedule(row);
                    trainerScheduleList.Add((T)Convert.ChangeType(trainerSchedule, typeof(T)));
                }
            }
            return trainerScheduleList;
        }

        public override void Update(BaseDTO baseDTO, int? changeBy)
        {
            throw new NotImplementedException();
        }


        private TrainerSchedule BuildTrainerSchedule(Dictionary<string, object> row)
        {
            var trainerSchedule = new TrainerSchedule()
            {
                Name = row["name"].ToString(),
                UserId = Convert.ToInt32(row["user_id"]),
                Id = Convert.ToInt32(row["schedule_id"]), // Adjusted to match the property name in TrainerSchedule
                StartDate = Convert.ToDateTime(row["start_date"]),
                EndDate = Convert.ToDateTime(row["end_date"]),
                IsAvailable = Convert.ToChar(row["is_available"]),
                CreationDate = Convert.ToDateTime(row["start_date"])
            };

            return trainerSchedule;
        }
    }
}
