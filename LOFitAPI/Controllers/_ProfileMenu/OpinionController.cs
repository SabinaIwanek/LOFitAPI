using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LOFitAPI.DbControllers.ProffileMenu;

namespace LOFitAPI.Controllers._ProfileMenu
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OpinionController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> Add(OpiniaModel model)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            model.Id_usera = (int)idUsera;

            string answer = OpiniaDbController.Add(model);

            return Ok(answer);
        }

        [HttpPut]
        public ActionResult<string> Update(OpiniaModel model)
        {
            string answer = OpiniaDbController.Update(model);

            return Ok(answer);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<string> Delete(int id)
        {
            string answer = OpiniaDbController.Delete(id);

            return Ok(answer);
        }

        [HttpGet]
        [Route("myopinion/{coachid}")]
        public ActionResult<OpiniaModel> GetMyOpinion(int coachId)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            OpiniaModel model = OpiniaDbController.GetMyOpinion((int)idUsera, coachId);

            return Ok(model);
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<OpiniaModel> GetOne(int id)
        {
            OpiniaModel model = OpiniaDbController.GetOne(id);

            return Ok(model);
        }

        [HttpGet]
        [Route("coachList/{id}")]
        public ActionResult<List<OpiniaModel>> GetCoachList(int id)
        {
            List<OpiniaModel> model = OpiniaDbController.GetList(id);

            return Ok(model);
        }

        [HttpGet]
        [Route("appList")]
        public ActionResult<List<OpiniaModel>> GetAllList()
        {
            List<OpiniaModel> model = OpiniaDbController.GetAll();

            return Ok(model);
        }
    }
}
