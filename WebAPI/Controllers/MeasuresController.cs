using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Measure measure)
        {
            var mc = new MeasureManager();
            mc.Create(measure);

            return Ok(measure);
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            var mc = new MeasureManager();
            var result = mc.RetrieveAll();

            return Ok(result);
        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int Id)
        {
            var mc = new MeasureManager();
            var result = mc.RetrieveById(Id);
            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Measure measure)
        {
            var mc = new MeasureManager();
            mc.Update(measure);
            return Ok(measure);
        }

        [HttpGet]
        [Route("RetrieveByMonth")]
        public ActionResult RetrieveByMonth(int month, int userId)
        {
            var mc = new MeasureManager();
            var result = mc.RetrieveByMonth(month, userId);
            return Ok(result);
        }
    }
}
