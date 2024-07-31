using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class ExerciseController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Exercise exercise)
        {
            try
            {
                var pm = new ExerciseManager();
                pm.Create(exercise);
                return Ok(exercise);
            }

            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Exercise exercise)
        {
            try
            {
                var pm = new ExerciseManager();
                pm.Update(exercise);
                return Ok(exercise);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var dm = new ExerciseManager();
                var result = dm.RetrieveAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int id)
        {
            try
            {
                var dm = new ExerciseManager();
                var result = dm.RetrieveById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
    }
}
