using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeaseurementAppointmentsController : ControllerBase
    {

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(MeasurementAppointments measurementAppointments)
        {
            var ma = new MappointmentsManager();
            ma.Create(measurementAppointments);
            return Ok(measurementAppointments);
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            var ma = new MappointmentsManager();
            var result = ma.RetrieveAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int Id)
        {
            var ma = new MappointmentsManager();
            var result = ma.RetrieveById(Id);
            return Ok(result);
        }

        [HttpGet]
        [Route("RetrieveByTrainerId")]
        public ActionResult RetrieveByTrainerId(int Id)
        {
            var ma = new MappointmentsManager();
            var result = ma.RetrieveByTrainerId(Id);
            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(MeasurementAppointments measurementAppointments)
        {
            var ma = new MappointmentsManager();
            ma.Update(measurementAppointments);
            return Ok(measurementAppointments);
        }

    }
}
