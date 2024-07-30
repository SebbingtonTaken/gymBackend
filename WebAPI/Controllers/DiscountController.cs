using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]

        public ActionResult Create(Discount discount)
        {
            try
            {
                var pm = new DiscountManager();
                pm.Create(discount);
                return Ok(discount);
            }

            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Discount discount)
        {
            try
            {
                var pm = new DiscountManager();
                pm.Update(discount);
                return Ok(discount);
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
                var dm = new DiscountManager();
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
                var dm = new DiscountManager();
                var result = dm.RetrieveById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                var dm = new DiscountManager();
                dm.Delete(id);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
    }
}
