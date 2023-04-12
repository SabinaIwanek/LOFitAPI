using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbControllers.ProffileMenu;
using LOFitAPI.DbModels.ProfileMenu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers._ProfileMenu
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CertificateController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> Add(CertyfikatModel model)
        {
            int? idUsera = KontoDbController.ReturnUserId(User.Identity?.Name);

            if (idUsera == null)
                return Unauthorized();

            model.Id_trenera = (int)idUsera;

            string answer = CertyfikatDbController.Add(model);

            return Ok(answer);
        }

        [HttpPut]
        public ActionResult<string> Update(CertyfikatModel model)
        {
            string answer = CertyfikatDbController.Update(model);

            return Ok(answer);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<string> Delete(int id)
        {
            string answer = CertyfikatDbController.Delete(id);

            return Ok(answer);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<CertyfikatModel> GetOne(int id)
        {
            CertyfikatModel model = CertyfikatDbController.GetOne(id);

            return Ok(model);
        }

        [HttpGet]
        [Route("coachList/{id}")]
        public ActionResult<List<CertyfikatModel>> GetCoachList(int id)
        {
            List<CertyfikatModel> model = CertyfikatDbController.GetList(id);

            return Ok(model);
        }

        [HttpGet]
        [Route("appList")]
        public ActionResult<List<CertyfikatModel>> GetAllList()
        {
            List<CertyfikatModel> model = CertyfikatDbController.GetAll();

            return Ok(model);
        }
    }
}