using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbControllers.ProffileMenu;
using LOFitAPI.DbModels.Accounts;
using LOFitAPI.DbModels.ProfileMenu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers._Accounts
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPut]
        public ActionResult<string> Update(UzytkownikModel model)
        {
            string answer = UzytkownikDbController.Update(model);

            return Ok(answer);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<UzytkownikModel> GetOne(int id)
        {
            UzytkownikModel model = UzytkownikDbController.GetOne(id);

            return Ok(model);
        }

        [HttpGet]
        [Route("getName/{id}")]
        public ActionResult<string> GetName(int id)
        {
            UzytkownikModel model = UzytkownikDbController.GetOne(id);

            if(model == null) return string.Empty;

            return Ok(model.Imie);
        }
    }
}