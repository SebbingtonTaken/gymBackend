using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class EmployeeSchedule
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string FreeDays { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int EmployeeId { get; set; }
        public char IsAvailable { get; set; }
    }

}
