using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbControllers.Menu;
using LOFitAPI.DbControllers.ProffileMenu;
using LOFitAPI.DbModels.Accounts;
using LOFitAPI.DbModels.Menu;
using LOFitAPI.DbModels.ProfileMenu;
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
            if (id == -1)
            {
                int? idCoach = KontoDbController.ReturnUserId(User?.Identity?.Name);

                if (idCoach == null)
                    return Unauthorized();

                id = (int)idCoach;
            }

            TrenerModel model = TrenerDbController.GetOne(id);

            return Ok(model);
        }

        [HttpPut]
        public ActionResult<string> Update(TrenerModel model)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null || idUsera != model.Id)
                return Unauthorized();

            string answer = TrenerDbController.Update(model);

            return Ok(answer);
        }
        [HttpGet]
        public ActionResult<List<TrenerModel>> GetAll()
        {
            List<TrenerModel> list = new List<TrenerModel>();

            list = TrenerDbController.GetAll();

            return Ok(list);
        }

        [HttpGet]
        [Route("getmy/{type}")] //0->nowe, 1->potwierdzone, 2->odrzucone, 3->wszystkie
        public ActionResult<List<TrenerModel>> GetMy(int type)
        {
            if (type > 3 || type < 0) return BadRequest();

            int? idUsera = KontoDbController.ReturnUserId(User?.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            List<PowiazanieModel> connections = PowiazanieDbController.GetListUser((int)idUsera);

            List<TrenerModel> list = new List<TrenerModel>();

            foreach(var connection in connections)
            {
                if (connection.Czas_od <= DateTime.Now && (connection.Czas_do == null || connection.Czas_do >= DateTime.Now))
                {
                    if (type == 0 && connection.Zatwierdzone != 0) continue;
                    if (type == 1 && connection.Zatwierdzone != 1) continue;
                    if (type == 2 && connection.Zatwierdzone != 2) continue;
                   
                    list.Add(TrenerDbController.GetOne(connection.Id_trenera));
                }
                    
            }

            return Ok(list);
        }
    }
}
