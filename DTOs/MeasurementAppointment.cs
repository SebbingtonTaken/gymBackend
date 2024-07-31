using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{ 
    public class MeasurementAppointments : BaseDTO
{
        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public string TrainerName { get; set; }
        public int TrainerScheduleId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentStatus { get; set; }
    }
}
