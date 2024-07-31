using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class EquipmentCrudFactory : CrudFactory
    {
        public EquipmentCrudFactory()
        {
            sqlDao = SqlDao.GetInstance();
        }



        public override void Create(BaseDTO baseDTO)
        {
            var equipment = baseDTO as Equipment;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "INS_EQUIPMENT_PR";

            sqlOperation.AddStringParameter("equipment_name", equipment.EquipmentName);
            sqlOperation.AddIntParameter("location", equipment.LocationNumber);

            sqlDao.ExecuteProcedure(sqlOperation);

        }

        public override void DeleteByID(int id)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var equipmentList = new List<T>();
            var sqlOperation = new SqlOperation()

            {
                ProcedureName = "RET_ALL_EQUIPMENTS_PR"
            };

            var listResult = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResult.Count > 0)
            {
                foreach (var row in listResult)
                {
                    var equipment = BuildEquipment(row);
                    equipmentList.Add((T)Convert.ChangeType(equipment, typeof(T)));
                }
            }
            return equipmentList;
        }

        public  T RetrieveAllMeasuresById<T>(int id)
        {
            var sqlOperation = new SqlOperation()
            {
                ProcedureName = "RET_EQUIPMENTS_BY_ID_PR"
            };
            sqlOperation.AddIntParameter("@equipment_id", id);

            var listResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0)
            {
                var row = listResults[0];
                var equipment = (T)Convert.ChangeType(BuildEquipment(row), typeof(T));
                return equipment;
            }
            return default(T);
        }
        public override void Update(BaseDTO baseDTO, int? id)
        {
            var equipment = baseDTO as Equipment;
            var sqlOperation = new SqlOperation();

            sqlOperation.ProcedureName = "UP_EQUIPMENT_PR";
            sqlOperation.AddIntParameter("equipment_id", equipment.Id);
            sqlOperation.AddStringParameter("equipment_name", equipment.EquipmentName);
            sqlOperation.AddIntParameter("location", equipment.LocationNumber);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        private Equipment BuildEquipment(Dictionary<string, object> row)
        {
            var equipment = new Equipment()
            {
                Id = (int)row["equipment_id"],
                EquipmentName = (string)row["equipment_name"],
                LocationNumber = (int)row["location"]
            };
            return equipment;
        }
    }
}
