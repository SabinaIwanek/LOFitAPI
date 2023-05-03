using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbControllers.Menu;
using LOFitAPI.DbControllers.MenuCoach;
using LOFitAPI.DbModels.Menu;
using LOFitAPI.DbModels.MenuCoach;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers._Menu
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PlanController : ControllerBase
    {
        [HttpGet]
        [Route("workouts")]
        public ActionResult<List<TreningNaLiscieModel>> GetWorkouts(int id)
        {
            PlanTygodniowyModel model = PlanTygodniowyDbController.GetOne(id);

            List<TreningNaLiscieModel> list = new List<TreningNaLiscieModel>
            {
                TreningNaLiscieDbController.GetOnePlan(model.Dzien1),
                TreningNaLiscieDbController.GetOnePlan(model.Dzien2),
                TreningNaLiscieDbController.GetOnePlan(model.Dzien3),
                TreningNaLiscieDbController.GetOnePlan(model.Dzien4),
                TreningNaLiscieDbController.GetOnePlan(model.Dzien5),
                TreningNaLiscieDbController.GetOnePlan(model.Dzien6),
                TreningNaLiscieDbController.GetOnePlan(model.Dzien7)
            };

            return Ok(list);
        }

        [HttpGet]
        [Route("meals")]
        public ActionResult<List<ProduktNaLiscieModel>> GetMeals(int id)
        {
            PlanTygodniowyModel model = PlanTygodniowyDbController.GetOne(id);

            List<ProduktNaLiscieModel> list = new List<ProduktNaLiscieModel>
            {
                ProduktNaLiscieDbController.GetOnePlan(model.Dzien1),
                ProduktNaLiscieDbController.GetOnePlan(model.Dzien2),
                ProduktNaLiscieDbController.GetOnePlan(model.Dzien3),
                ProduktNaLiscieDbController.GetOnePlan(model.Dzien4),
                ProduktNaLiscieDbController.GetOnePlan(model.Dzien5),
                ProduktNaLiscieDbController.GetOnePlan(model.Dzien6),
                ProduktNaLiscieDbController.GetOnePlan(model.Dzien7)
            };

            return Ok(list);
        }

        [HttpGet]
        [Route("all/{type}")]
        public ActionResult<List<PlanTygodniowyModel>> GetByType(int type)
        {
            int? idTrenera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idTrenera == null)
                return Unauthorized();

            List<PlanTygodniowyModel> model = PlanTygodniowyDbController.GetByType((int)idTrenera, type);

            return Ok(model);
        }

        [HttpPost]
        public ActionResult<string> Add(PlanTygodniowyModel model)
        {
            int? idTrenera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idTrenera == null)
                return Unauthorized();

            model.Id_trenera = (int)idTrenera;

            string answer = PlanTygodniowyDbController.Add(model);

            return Ok(answer);
        }

        [HttpPut]
        public ActionResult<string> Update(PlanTygodniowyModel model)
        {
            string answer = PlanTygodniowyDbController.Update(model);

            return Ok(answer);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<string> Delete(int id)
        {
            string answer = PlanTygodniowyDbController.Delete(id);

            return Ok(answer);
        }
    }

}
