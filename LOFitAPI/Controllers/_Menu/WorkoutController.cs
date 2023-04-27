using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbControllers.Menu;
using LOFitAPI.DbModels.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers._Menu
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class WorkoutController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> Add(TreningModel trening)
        {
            if (trening.Id_konta != 0)
            {
                int? idKonta = KontoDbController.ReturnKontoId(User.Identity?.Name);

                if (idKonta == null)
                    return Unauthorized();

                trening.Id_konta = (int)idKonta;
            }

            int answer = TreningDbController.Add(trening);

            return Ok(answer);
        }

        [HttpPut]
        public ActionResult<string> Update(TreningModel trening)
        {
            string answer = TreningDbController.Update(trening);

            return Ok(answer);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<string> Delete(int id)
        {
            string answer = TreningDbController.Delete(id);

            return Ok(answer);
        }

        [HttpGet]
        [Route("{idTreningu}")]
        public ActionResult<TreningModel> GetOne(int idTreningu)
        {
            TreningModel trening = TreningDbController.GetOne(idTreningu);

            return Ok(trening);
        }

        [HttpGet]
        [Route("userList")]
        public ActionResult<List<TreningModel>> GetUserList()
        {
            int? idKonta = KontoDbController.ReturnKontoId(User?.Identity?.Name);

            if (idKonta == null)
                return Unauthorized();

            List<TreningModel> trening = TreningDbController.GetUserList((int)idKonta);

            return Ok(trening);
        }

        [HttpGet]
        [Route("appList")]
        public ActionResult<List<TreningModel>> GetAppList()
        {
            List<TreningModel> trening = TreningDbController.GetAppList();

            return Ok(trening);
        }
    }
}