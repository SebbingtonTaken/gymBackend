using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class Routines : BaseDTO
    {
        public string RoutineType { get; set; }
        public string RoutineId { get; set; }
        public List<RoutineDay> RoutineDay { get; set; }
    }
}
