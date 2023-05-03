using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbControllers.MenuCoach;
using LOFitAPI.DbModels.MenuCoach;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers._Menu
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TermController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> Add(TerminModel model)
        {
            int? idTrenera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idTrenera == null)
                return Unauthorized();

            model.Id_trenera = (int)idTrenera;

            int id = TerminDbController.Add(model);

            return Ok(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<string> Delete(int id)
        {
            string answer = TerminDbController.Delete(id);

            return Ok(answer);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<TerminModel> GetOne(int id)
        {
            int? idTrenera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idTrenera == null)
                return Unauthorized();

            TerminModel model = TerminDbController.GetOne(id);

            return Ok(model);
        }

        [HttpGet]
        [Route("{dateString}")]
        public ActionResult<List<TerminModel>> GetByDate(string dateString)
        {
            DateTime date = DateTime.Parse(dateString);

            int? idTrenera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idTrenera == null)
                return Unauthorized();

            List<TerminModel> model = TerminDbController.GetByDay((int)idTrenera, date);

            return Ok(model);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<TerminModel>> GetAll()
        {
            int? idTrenera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idTrenera == null)
                return Unauthorized();

            List<TerminModel> model = TerminDbController.GetAll((int)idTrenera);

            return Ok(model);
        }
    }

}
