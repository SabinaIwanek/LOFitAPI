using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbControllers.Menu;
using LOFitAPI.DbControllers.MenuCoach;
using LOFitAPI.DbModels.Menu;
using LOFitAPI.DbModels.MenuCoach;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LOFitAPI.Controllers._Menu
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PlanController : ControllerBase
    {
        [HttpGet]
        [Route("workouts/{id}")]
        public ActionResult<List<List<TreningNaLiscieModel>>> GetWorkouts(int id)
        {
            PlanTygodniowyModel model = PlanTygodniowyDbController.GetOne(id);

            List<List<TreningNaLiscieModel>> list = new List<List<TreningNaLiscieModel>>
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
        [Route("meals/{id}")]
        public ActionResult<List<List<ProduktNaLiscieModel>>> GetMeals(int id)
        {
            PlanTygodniowyModel model = PlanTygodniowyDbController.GetOne(id);

            List<List<ProduktNaLiscieModel>> list = new List<List<ProduktNaLiscieModel>>
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
        [Route("{id}")]
        public ActionResult<PlanTygodniowyModel> GetOne(int id)
        {
            int? idTrenera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idTrenera == null)
                return Unauthorized();

            PlanTygodniowyModel model = PlanTygodniowyDbController.GetOne(id);

            return Ok(model);
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
        public ActionResult<int> Add(PlanTygodniowyModel model)
        {
            int? idTrenera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idTrenera == null)
                return Unauthorized();

            model.Id_trenera = (int)idTrenera;

            int id = PlanTygodniowyDbController.Add(model);

            if (id != 0)
            {
                model.Dzien1 = Int32.Parse($"1{id}");
                model.Dzien2 = Int32.Parse($"2{id}");
                model.Dzien3 = Int32.Parse($"3{id}");
                model.Dzien4 = Int32.Parse($"4{id}");
                model.Dzien5 = Int32.Parse($"5{id}");
                model.Dzien6 = Int32.Parse($"6{id}");
                model.Dzien7 = Int32.Parse($"7{id}");

                model.Id = id;
                model.Nazwa = $"Nowy plan {id}";
                PlanTygodniowyDbController.Update(model);
            }

            return Ok(id);
        }

        [HttpPut]
        public ActionResult<string> Update(PlanTygodniowyModel model)
        {
            int kcla = 0;

            if (model.Typ == 0)
            {
                kcla += TreningNaLiscieDbController.GetOnePlan(model.Dzien1).Where(x => x.Kcla != null).Sum(x => (int)x.Kcla);
                kcla += TreningNaLiscieDbController.GetOnePlan(model.Dzien2).Where(x => x.Kcla != null).Sum(x => (int)x.Kcla);
                kcla += TreningNaLiscieDbController.GetOnePlan(model.Dzien3).Where(x => x.Kcla != null).Sum(x => (int)x.Kcla);
                kcla += TreningNaLiscieDbController.GetOnePlan(model.Dzien4).Where(x => x.Kcla != null).Sum(x => (int)x.Kcla);
                kcla += TreningNaLiscieDbController.GetOnePlan(model.Dzien5).Where(x => x.Kcla != null).Sum(x => (int)x.Kcla);
                kcla += TreningNaLiscieDbController.GetOnePlan(model.Dzien6).Where(x => x.Kcla != null).Sum(x => (int)x.Kcla);
                kcla += TreningNaLiscieDbController.GetOnePlan(model.Dzien7).Where(x => x.Kcla != null).Sum(x => (int)x.Kcla);
            }
            else if (model.Typ == 1)
            {
                kcla += ProduktNaLiscieDbController.GetOnePlan(model.Dzien1).Select(x => x.Gramy * x.Produkt.Kcla / x.Produkt.Gramy).Sum(x => x);
                kcla += ProduktNaLiscieDbController.GetOnePlan(model.Dzien2).Select(x => x.Gramy * x.Produkt.Kcla / x.Produkt.Gramy).Sum(x => x);
                kcla += ProduktNaLiscieDbController.GetOnePlan(model.Dzien3).Select(x => x.Gramy * x.Produkt.Kcla / x.Produkt.Gramy).Sum(x => x);
                kcla += ProduktNaLiscieDbController.GetOnePlan(model.Dzien4).Select(x => x.Gramy * x.Produkt.Kcla / x.Produkt.Gramy).Sum(x => x);
                kcla += ProduktNaLiscieDbController.GetOnePlan(model.Dzien5).Select(x => x.Gramy * x.Produkt.Kcla / x.Produkt.Gramy).Sum(x => x);
                kcla += ProduktNaLiscieDbController.GetOnePlan(model.Dzien6).Select(x => x.Gramy * x.Produkt.Kcla / x.Produkt.Gramy).Sum(x => x);
                kcla += ProduktNaLiscieDbController.GetOnePlan(model.Dzien7).Select(x => x.Gramy * x.Produkt.Kcla / x.Produkt.Gramy).Sum(x => x);
            }

            model.Kcla = kcla;

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
