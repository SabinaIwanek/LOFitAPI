using LOFitAPI.DbControllers.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LOFitAPI.DbControllers.ProffileMenu;
using LOFitAPI.DbModels.ProfileMenu;

namespace LOFitAPI.Controllers._ProfileMenu
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> Add(ZgloszenieModel model)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            model.Id_usera = (int)idUsera;

            string answer = ZgloszenieDbController.Add(model);

            return Ok(answer);
        }

        [HttpPut]
        public ActionResult<string> Update(ZgloszenieModel model)
        {
            string answer = ZgloszenieDbController.Update(model);

            return Ok(answer);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<string> Delete(int id)
        {
            string answer = ZgloszenieDbController.Delete(id);

            return Ok(answer);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ZgloszenieModel> GetOne(int id)
        {
            ZgloszenieModel model = ZgloszenieDbController.GetOne(id);

            return Ok(model);
        }

        [HttpGet]
        [Route("coachList/{id}")]
        public ActionResult<List<ZgloszenieModel>> GetCoachList(int id)
        {
            List<ZgloszenieModel> model = ZgloszenieDbController.GetList(id);

            return Ok(model);
        }

        [HttpGet]
        [Route("appList")]
        public ActionResult<List<ZgloszenieModel>> GetAllList()
        {
            List<ZgloszenieModel> model = ZgloszenieDbController.GetAll();

            return Ok(model);
        }
    }
}