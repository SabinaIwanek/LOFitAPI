using LOFitAPI.Controllers.PostModels.Registration;
using LOFitAPI.DbControllers.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        [Route("user")]
        [HttpPost]
        public ActionResult<string> RegistrationUzytkownik(UzytkownikPostModel form)
        {
            if (form == null) return Ok("Błąd danych.");

            if(UzytkownikDbController.Create(form))
                return Ok("Założono konto.");

            return Ok("Błąd połączenia z bazą.");
        }

        [Route("admin")]
        [HttpPost]
        public ActionResult<string> RegistrationAdmin(AdminPostModel form)
        {
            if (form == null) return Ok("Błąd danych.");

            if (AdminDbController.Create(form))
                return Ok("Założono konto.");

            return Ok("Błąd połączenia z bazą.");
        }

        [Route("trener")]
        [HttpPost]
        public ActionResult<string> RegistrationTrener(TrenerPostModel form)
        {
            if (form == null) return Ok("Błąd danych.");

            if (TrenerDbController.Create(form))
                return Ok("Założono konto.");

            return Ok("Błąd połączenia z bazą.");
        }

        [Route("user")]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Kurczak");
        }
    }
}
