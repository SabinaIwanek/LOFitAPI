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
    public class ProductListController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> Add(ProduktNaLiscieModel product)
        {
            if(product.Id_usera == -1)
            {
                int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

                if (idUsera == null)
                    return Unauthorized();

                product.Id_usera = (int)idUsera;
            }

            int id = ProduktNaLiscieDbController.Add(product);

            return Ok(id);
        }

        [HttpPut]
        public ActionResult<string> Update(ProduktNaLiscieModel product)
        {
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
        [Route("CheckedBoxChange/{id}/{check}")]
        public ActionResult<string> CheckedBoxChange(int id, int check)
        {
            string answer = ProduktNaLiscieDbController.CheckedBoxChange(id, check);

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
        [Route("userlist/{dateString}/{idUsera}")]
        public ActionResult<List<ProduktNaLiscieModel>> GetUserList(string dateString, int idUsera)
        {
            DateTime date = DateTime.Parse(dateString);
            
            if(idUsera == -1)
            {
                int? id = KontoDbController.ReturnUserId(User?.Identity?.Name);

                if (id == null)
                    return Unauthorized();

                idUsera = (int)id;
            }

            List<ProduktNaLiscieModel> list = ProduktNaLiscieDbController.GetUserList(idUsera, date);

            return Ok(list);
        }
    }

}
