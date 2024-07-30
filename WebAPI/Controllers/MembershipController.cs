using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {

        [HttpPost]
        [Route("CreateMembership")]
        public ActionResult CreateMembership(Membership membership)
        {
            try
            {
                var membershipManager = new MembershipManager();
                membershipManager.Create(membership);



                return Ok(membership);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateMembership/{changeById}")]
        public ActionResult UpdateMembership(Membership membership, int changeById)
        {
            try
            {
                var membershipManager = new MembershipManager();
                membershipManager.Update(membership, changeById);

                return Ok(membership);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveMembershipById/{id}")]
        public ActionResult RetrieveMembershipById(int id)
        {
            try
            {
                var membershipManager = new MembershipManager();
                var membership = membershipManager.RetrieveMembershipById(id);
                if (membership == null)
                {
                    return NotFound();
                }
                return Ok(membership);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveAllMemberships")]
        public ActionResult RetrieveAllMemberships()
        {
            try
            {
                var membershipManager = new MembershipManager();
                var membership = membershipManager.RetrieveAllMemberships();
                if (membership == null)
                {
                    return NotFound();
                }
                return Ok(membership);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
