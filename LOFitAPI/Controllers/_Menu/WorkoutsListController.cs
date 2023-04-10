using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbControllers.Menu;
using LOFitAPI.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers._Menu
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class WorkoutsListController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Add(TreningNaLiscieModel trening)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            trening.Id_usera = (int)idUsera;

            string answer = TreningNaLiscieDbController.Add(trening);

            return Ok(answer);
        }

        [HttpPut]
        public ActionResult<string> Update(TreningNaLiscieModel trening)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            trening.Id_usera = (int)idUsera;

            string answer = TreningNaLiscieDbController.Update(trening);

            return Ok(answer);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<string> Delete(int id)
        {
            string answer = TreningNaLiscieDbController.Delete(id);

            return Ok(answer);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<TreningNaLiscieModel> GetOne(int id)
        {
            TreningNaLiscieModel trening = TreningNaLiscieDbController.GetOne(id);

            return Ok(trening);
        }

        [HttpGet]
        [Route("userlist/{dateString}")]
        public ActionResult<List<TreningNaLiscieModel>> GetUserList(string dateString)
        {
            DateTime date = DateTime.Parse(dateString);

            int? idUsera = KontoDbController.ReturnUserId(User?.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            List<TreningNaLiscieModel> list = TreningNaLiscieDbController.GetUserList((int)idUsera, date);

            return Ok(list);
        }
    }

}
