using LOFitAPI.DbControllers;
using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class WorkoutController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> Add(TreningModel trening)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            trening.Id_usera = (int)idUsera;

            int answer = TreningDbController.Add(trening);

            return Ok(answer);
        }

        [HttpPut]
        public ActionResult<string> Update(TreningModel trening)
        {
            string answer = TreningDbController.Update(trening);

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
            int? idUsera = KontoDbController.ReturnUserId(User?.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            List<TreningModel> trening = TreningDbController.GetUserList((int)idUsera);

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