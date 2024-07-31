using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class MappointmentsManager
    {
        public void Create(MeasurementAppointments measurementAppointments)
        {
            var maCrud = new MeasurementAppointmentsCrudFactory();
            maCrud.Create(measurementAppointments);
        }

        public List<MeasurementAppointments> RetrieveAll()
        {
            var maCrud = new MeasurementAppointmentsCrudFactory();
            return maCrud.RetrieveAll<MeasurementAppointments>();
        }

        public MeasurementAppointments RetrieveById(int Id)
        {
            var maCrud = new MeasurementAppointmentsCrudFactory();
            return maCrud.RetrieveAllMeasuresById<MeasurementAppointments>(Id);
        }

        public void Update(MeasurementAppointments measurementAppointments)
        {
            var maCrud = new MeasurementAppointmentsCrudFactory();
            maCrud.Update(measurementAppointments, 0);
        }

        public List<MeasurementAppointments> RetrieveByTrainerId(int Id)
        {
            var maCrud = new MeasurementAppointmentsCrudFactory();
            return maCrud.RetrieveByTrainerId<MeasurementAppointments>(Id);
        }
    }
}
