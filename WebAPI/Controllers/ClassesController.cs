using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : Controller
    {

        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var classesManager = new ClassesManager();
                var result = classesManager.RetrieveAllTrainerSchedules();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }

    }
}
