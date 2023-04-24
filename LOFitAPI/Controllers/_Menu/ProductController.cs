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
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> Add(ProduktModel product)
        {
            if(product.Id_konta != 0)
            {
                int? idKonta = KontoDbController.ReturnKontoId(User.Identity?.Name);

                if (idKonta == null)
                    return Unauthorized();

                product.Id_konta = (int)idKonta;
            }
            
            int answer = ProduktDbController.Add(product);

            return Ok(answer);
        }

        [HttpPut]
        public ActionResult<string> Update(ProduktModel product)
        {
            string answer = ProduktDbController.Update(product);

            return Ok(answer);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<string> Delete(int id)
        {
            string answer = ProduktDbController.Delete(id);

            return Ok(answer);
        }

        [HttpGet]
        [Route("{idProduktu}")]
        public ActionResult<ProduktModel> GetOne(int idProduktu)
        {
            ProduktModel product = ProduktDbController.GetOne(idProduktu);

            return Ok(product);
        }

        [HttpGet]
        [Route("userList")]
        public ActionResult<List<ProduktModel>> GetUserList()
        {
            int? idKonta = KontoDbController.ReturnKontoId(User.Identity?.Name);

            if (idKonta == null)
                return Unauthorized();

            List<ProduktModel> product = ProduktDbController.GetUserList((int)idKonta);

            return Ok(product);
        }

        [HttpGet]
        [Route("appList")]
        public ActionResult<List<ProduktModel>> GetAppList()
        {
            List<ProduktModel> product = ProduktDbController.GetAppList();

            return Ok(product);
        }
    }

}
