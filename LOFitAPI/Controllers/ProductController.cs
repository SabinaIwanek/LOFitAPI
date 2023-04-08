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
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> Add(ProduktModel product)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            product.Id_usera = (int)idUsera;

            int answer = ProduktDbController.Add(product);

            return Ok(answer);
        }

        [HttpPut]
        public ActionResult<string> Update(ProduktModel product)
        {
            string answer = ProduktDbController.Update(product);

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
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            List<ProduktModel> product = ProduktDbController.GetUserList((int)idUsera);

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
