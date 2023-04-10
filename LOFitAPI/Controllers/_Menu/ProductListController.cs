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
    public class ProductListController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Add(ProduktNaLiscieModel product)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            product.Id_usera = (int)idUsera;

            string answer = ProduktNaLiscieDbController.Add(product);

            return Ok(answer);
        }

        [HttpPut]
        public ActionResult<string> Update(ProduktNaLiscieModel product)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            product.Id_usera = (int)idUsera;

            string answer = ProduktNaLiscieDbController.Update(product);

            return Ok(answer);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<string> Delete(int id)
        {
            string answer = ProduktNaLiscieDbController.Delete(id);

            return Ok(answer);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProduktNaLiscieModel> GetOne(int id)
        {
            ProduktNaLiscieModel product = ProduktNaLiscieDbController.GetOne(id);

            return Ok(product);
        }

        [HttpGet]
        [Route("userlist/{dateString}")]
        public ActionResult<List<ProduktNaLiscieModel>> GetUserList(string dateString)
        {
            DateTime date = DateTime.Parse(dateString);

            int? idUsera = KontoDbController.ReturnUserId(User?.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            List<ProduktNaLiscieModel> list = ProduktNaLiscieDbController.GetUserList((int)idUsera, date);

            return Ok(list);
        }
    }

}
