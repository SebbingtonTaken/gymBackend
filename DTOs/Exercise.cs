using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class Exercise:BaseDTO
    {
        public string ExerciseName { get; set; }
        public  string ExerciseType { get; set; }

        public Equipment AssignedEquipment { get; set; }
    }
}
