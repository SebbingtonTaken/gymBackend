using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class MeasurementAppointmentsCrudFactory : CrudFactory
    {
        public MeasurementAppointmentsCrudFactory()
        {
            sqlDao = SqlDao.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var appointment = baseDTO as MeasurementAppointments;
            var sqlOperation = new SqlOperation
            {
                ProcedureName = "INS_MEASUREMENT_APPOINTMENTS_PR"
            };
            sqlOperation.AddIntParameter("user_id", appointment.UserId);
            sqlOperation.AddIntParameter("appointment_date", appointment.TrainerScheduleId);
            sqlOperation.AddStringParameter("appointment_status", appointment.AppointmentStatus);
            sqlOperation.AddDateTimeParameter("creation_date", appointment.CreationDate);


            sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void DeleteByID(int id)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var appointmentList = new List<T>();
            var sqlOperation = new SqlOperation
            {
                ProcedureName = "RET_ALL_MEASUREMENT_APPOINTMENT_PR"
            };
            var listResult = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResult.Count > 0)
            {
                foreach (var row in listResult)
                {
                    var appointment = BuildAppointment(row);
                    appointmentList.Add((T)Convert.ChangeType(appointment, typeof(T)));
                }
            }
            return appointmentList;
        }

        public  T RetrieveAllMeasuresById<T>(int id)
        {
            var sqlOperation = new SqlOperation
            {
                ProcedureName = "RET_MEASUREMENT_APPOINTMENT_BY_USER_ID_PR"
            };
            sqlOperation.AddIntParameter("@UserId", id);

            var listResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0)
            {
                var row = listResults[0];
                var appointment = (T)Convert.ChangeType(BuildAppointment(row), typeof(T));
                return appointment;
            }
            return default(T);
        }

        public List<T> RetrieveByTrainerId<T>(int trainerId)
        {
            var appointmentList = new List<T>();
            var sqlOperation = new SqlOperation
            {
                ProcedureName = "RET_MEASUREMENT_APPOINTMENT_BY_TRAINER_ID_PR"
            };
            sqlOperation.AddIntParameter("@TrainerId", trainerId);

            var listResult = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResult.Count > 0)
            {
                foreach (var row in listResult)
                {
                    var appointment = BuildAppointment(row);
                    appointmentList.Add((T)Convert.ChangeType(appointment, typeof(T)));
                }
            }
            return appointmentList;
        }

        public override void Update(BaseDTO baseDTO, int? id)
        {
            var measurementAppointment = baseDTO as MeasurementAppointments;
            var sqlOperation = new SqlOperation();

            sqlOperation.ProcedureName = "UP_MEASUREMENT_APPOINTMENTS_PR";
            sqlOperation.AddIntParameter("@appointment_id", measurementAppointment.Id);
            sqlOperation.AddIntParameter("@user_id", measurementAppointment.UserId);
            sqlOperation.AddIntParameter("@appointment_date", measurementAppointment.TrainerScheduleId);
            sqlOperation.AddStringParameter("@appointment_status", measurementAppointment.AppointmentStatus);
            sqlOperation.AddDateTimeParameter("@creation_date", measurementAppointment.CreationDate);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        private MeasurementAppointments BuildAppointment(Dictionary<string, object> row)
        {
            var appointment = new MeasurementAppointments
            {
                Id = (int)row["AppointmentID"],
                UserId = (int)row["UserID"],
                CustomerName = (string)row["CustomerName"],
                TrainerName = (string)row["TrainerName"],
                TrainerScheduleId = (int)row["TrainerScheduleId"], 
                AppointmentDate = (DateTime)row["AppointmentDate"],
                AppointmentStatus = (string)row["AppointmentStatus"],

            };
            return appointment;
        }

    }
}
