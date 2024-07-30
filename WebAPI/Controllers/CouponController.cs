using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]

        public ActionResult Create(Coupon coupon)
        {
            try
            {
                var pm = new CouponManager();
                pm.Create(coupon);
                return Ok(coupon);
            }

            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Coupon coupon)
        {
            try
            {
                var pm = new CouponManager();
                pm.Update(coupon);
                return Ok(coupon);
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
                var cm = new CouponManager();
                var result = cm.RetrieveAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }

        [HttpGet]
        [Route("RetrieveByCouponCode")]
        public ActionResult RetrieveByCouponCode(String code)
        {
            try
            {
                var cm = new CouponManager();
                var result = cm.RetrieveByCouponCode(code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
    }
}
