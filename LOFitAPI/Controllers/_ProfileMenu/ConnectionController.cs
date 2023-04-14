using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LOFitAPI.DbControllers.ProffileMenu;
using LOFitAPI.DbModels.ProfileMenu;

namespace LOFitAPI.Controllers._ProfileMenu
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ConnectionController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> Add(PowiazanieModel model)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            model.Id_usera = (int)idUsera;

            string answer = PowiazanieDbController.Add(model);

            return Ok(answer);
        }

        [HttpPut]
        public ActionResult<string> Update(PowiazanieModel model)
        {
            string answer = PowiazanieDbController.Update(model);

            return Ok(answer);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<string> Delete(int id)
        {
            string answer = PowiazanieDbController.Delete(id);

            return Ok(answer);
        }

        [HttpGet]
        [Route("coachList/{id}")]
        public ActionResult<List<PowiazanieModel>> GetCoachList(int id)
        {
            if (id == -1)
            {
                int? idCoach = KontoDbController.ReturnUserId(User?.Identity?.Name);

                if (idCoach == null)
                    return Unauthorized();

                id = (int)idCoach;
            }

            List<PowiazanieModel> model = PowiazanieDbController.GetListUser(id);

            return Ok(model);
        }

        [HttpGet]
        [Route("userList/{id}")]
        public ActionResult<List<PowiazanieModel>> GetUserList(int id)
        {
            if (id == -1)
            {
                int? idUser = KontoDbController.ReturnUserId(User?.Identity?.Name);

                if (idUser == null)
                    return Unauthorized();

                id = (int)idUser;
            }

            List<PowiazanieModel> model = PowiazanieDbController.GetListUser(id);

            return Ok(model);
        }

        [HttpGet]
        [Route("coachstate/{id}")]
        public ActionResult<int> GetCoachState(int id)
        {
            int? idUser = KontoDbController.ReturnUserId(User?.Identity?.Name);

            if (idUser == null)
                return Unauthorized();


            int type = PowiazanieDbController.GetCoachState(id, (int)idUser);

            return Ok(type);
        }
    }
}