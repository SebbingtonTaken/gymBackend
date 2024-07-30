using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class ExerciseCrudFactory : CrudFactory
    {
        public ExerciseCrudFactory()
        {
            sqlDao = SqlDao.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var exer = baseDTO as Exercise;

            //Crear instructivo para que el DAO pueda realizar un create de la base de datos

            var sqlOperation = new SqlOperation();

            // set del nombre del procedimiento




            sqlOperation.ProcedureName = "INS_EXERCISES_PR";

            sqlOperation.AddStringParameter("exercise_name", exer.ExerciseName);
            sqlOperation.AddStringParameter("exercise_type", exer.ExerciseType);
            sqlOperation.AddIntParameter("equipment_ID", exer.AssignedEquipment.Id);
            sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void DeleteByID(int id)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstExercises = new List<T>();
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "RET_ALL_EXERCISES_PR";
            var lstResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var exercise = BuildExercise(row);
                    lstExercises.Add((T)Convert.ChangeType(exercise, typeof(T)));
                }
            }
            return lstExercises;
        }



        public  T RetrieveById<T>(int id)
        {

            var sqlOperation = new SqlOperation();

            sqlOperation.AddIntParameter("P_ID", id);
            sqlOperation.ProcedureName = "RET_EXERCISE_BY_ID_PR";
            var lstResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                var row = lstResults[0];

                var exercise = (T)Convert.ChangeType(BuildExercise(row), typeof(T));
                return exercise;
            }
            return default(T);
        }

        public override void Update(BaseDTO baseDTO, int? id)
        {
            var exer = baseDTO as Exercise;

            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "UP_EXERCISES_PR";

            sqlOperation.AddStringParameter("exercise_name", exer.ExerciseName);
            sqlOperation.AddStringParameter("exercise_type", exer.ExerciseType);
            sqlOperation.AddIntParameter("exercise_id", exer.Id);
            sqlOperation.AddIntParameter("equipment_ID", exer.AssignedEquipment.Id);
            sqlDao.ExecuteProcedure(sqlOperation);
        }

        private Exercise BuildExercise(Dictionary<string, object> row)
        {

            var equip = new Equipment()
            {
                Id = (int)row["equipment_ID"],
                EquipmentName = (string)row["equipment_name"]
            };
            var exer = new Exercise()
            {
                Id = (int)row["exercise_id"],
                ExerciseName = (string)row["exercise_name"],
                ExerciseType = (string)row["exercise_type"],
                AssignedEquipment = equip,
                CreationDate = (DateTime)row["creation_date"],
            };
            return exer;
        }
    }
}
