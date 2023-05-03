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
    public class MeasurementController : ControllerBase
    {
        [HttpGet]
        [Route("{dateString}")]
        public ActionResult<PomiarModel> Get(string dateString)
        {
            DateTime date = DateTime.Parse(dateString);

            int? idUsera = KontoDbController.ReturnUserId(User?.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            PomiarModel measurement = PomiarDbController.GetOne(date, (int)idUsera);

            return Ok(measurement);
        }
        
        [HttpGet]
        [Route("week/{dateString}/{idUsera}")]
        public ActionResult<List<PomiarModel>> GetWeekById(string dateString, int idUsera)
        {
            DateTime date = DateTime.Parse(dateString);

            if(idUsera == -1)
            {
                int? id = KontoDbController.ReturnUserId(User.Identity?.Name);

                if (id == null)
                    return Unauthorized();

                idUsera = (int)id;
            }

            List<PomiarModel> measurements = PomiarDbController.GetWeek(date, idUsera);

            return Ok(measurements);
        }
        [HttpPost]
        public ActionResult<string> Add(PomiarModel measurement)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            measurement.Id_usera = (int)idUsera;

            string answer = PomiarDbController.Add(measurement);

            return Ok(answer);
        }

        [HttpPut]
        public ActionResult<string> Update(PomiarModel measurement)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            measurement.Id_usera = (int)idUsera;

            string answer = PomiarDbController.Update(measurement);

            return Ok(answer);
        }
    }

}
