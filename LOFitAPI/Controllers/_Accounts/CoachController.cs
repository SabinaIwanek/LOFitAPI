using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers._Accounts
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CoachController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public ActionResult<TrenerModel> GetOne(int id)
        {
            if (id == -1) {
                int? idCoach = KontoDbController.ReturnUserId(User?.Identity?.Name);

                if (idCoach == null)
                    return Unauthorized();

                id = (int)idCoach;
            }

            TrenerModel model = new TrenerModel();

            model = TrenerDbController.GetOne(id);

            return Ok(model);
        }
        [HttpGet]
        public ActionResult<List<TrenerModel>> GetAll()
        {
            List<TrenerModel> list = new List<TrenerModel>();

            list = TrenerDbController.GetAll();

            return Ok(list);
        }
    }
}
