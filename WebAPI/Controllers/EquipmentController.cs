using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Equipment equipment)
        {
            var ec = new EquipmentManager();
            ec.Create(equipment);

            return Ok(equipment);
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            var ec = new EquipmentManager();
            var result = ec.RetrieveAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int Id)
        {
            var ec = new EquipmentManager();
            var result = ec.RetrieveById(Id);
            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Equipment equipment)
        {
            var ec = new EquipmentManager();
            ec.Update(equipment);
            return Ok(equipment);
        }

    }
}
