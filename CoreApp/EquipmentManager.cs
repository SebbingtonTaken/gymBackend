using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class EquipmentManager
    {
        public void Create(Equipment equipment)
        {
            var eCrud = new EquipmentCrudFactory();
            eCrud.Create(equipment);
        }
        public List<Equipment> RetrieveAll()
        {
            var eCrud = new EquipmentCrudFactory();
            return eCrud.RetrieveAll<Equipment>();
        }

        public Equipment RetrieveById(int Id)
        {
            var eCrud = new EquipmentCrudFactory();
            return eCrud.RetrieveAllMeasuresById<Equipment>(Id);
        }

        public void Update(Equipment equipment)
        {
            var eCrud = new EquipmentCrudFactory();
            eCrud.Update(equipment, 0);
        }

    }
}
