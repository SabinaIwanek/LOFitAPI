using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbControllers;
using LOFitAPI.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers
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
        [Route("{id}")]
        public ActionResult<OpiniaModel> GetOne(int id)
        {
            OpiniaModel model = OpiniaDbController.GetOne(id);

            return Ok(model);
        }

        [HttpGet]
        [Route("coachList")]
        public ActionResult<List<OpiniaModel>> GetCoachList()
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            List<OpiniaModel> model = OpiniaDbController.GetList((int)idUsera);

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
