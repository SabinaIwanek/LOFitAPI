using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CoachController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<TrenerModel>> GetAll()
        {
            List<TrenerModel> list = new List<TrenerModel>();

            list = TrenerDbController.GetAll();

            return Ok(list);
        }
    }
}
