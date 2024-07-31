using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class ClassesManager
    {
        private ClassesFactory classFactory;

        public ClassesManager()
        {
            classFactory = new ClassesFactory();

        }

        public List<TrainerSchedule> RetrieveAllTrainerSchedules()
        {
            List<TrainerSchedule> trainerSchedules = classFactory.RetrieveAll<TrainerSchedule>();

            return trainerSchedules;
        }

    
    }
}
