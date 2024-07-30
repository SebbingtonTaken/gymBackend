using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class ExerciseManager
    {

        public void Create(Exercise exer)
        {
            ExerciseCrudFactory factory = new ExerciseCrudFactory();

            factory.Create(exer);
        }

        public void Update(Exercise exer)
        {
            ExerciseCrudFactory factory = new ExerciseCrudFactory();


            factory.Update(exer, exer.Id);
        }

        public List<Exercise> RetrieveAll()
        {

            var factory = new ExerciseCrudFactory();

            return factory.RetrieveAll<Exercise>();


        }

        public Exercise RetrieveById(int id)
        {

            var factory = new ExerciseCrudFactory();

            return factory.RetrieveById<Exercise>(id);


        }
    }
}
