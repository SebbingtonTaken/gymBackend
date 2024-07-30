using DataAccess.CRUD;
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
    }
}
