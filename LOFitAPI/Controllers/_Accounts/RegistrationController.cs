using LOFitAPI.Controllers.PostModels.Registration;
using LOFitAPI.DbControllers.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers._Accounts
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        [Route("user")]
        [HttpPost]
        public ActionResult<string> RegistrationUzytkownik(TrenerPostModel formOgolne)
        {
            int? idUsera = KontoDbController.ReturnUserId(formOgolne.Email);

            if (idUsera != null)
                return Ok("Użytkownik o podanym email już istnieje.");

            UzytkownikPostModel form = new UzytkownikPostModel(formOgolne);

            if (form == null) return Ok("Błąd danych.");

            if (UzytkownikDbController.Add(form))
                return Ok("Ok");

            return Ok("Błąd połączenia z bazą.");
        }

        [Route("admin")]
        [HttpPost]
        public ActionResult<string> RegistrationAdmin(AdminPostModel form)
        {
            int? idUsera = KontoDbController.ReturnUserId(form.Email);

            if (idUsera != null)
                return Ok("Użytkownik o podanym email już istnieje.");

            if (form == null) return Ok("Błąd danych.");

            if (AdminDbController.Add(form))
                return Ok("Ok");

            return Ok("Błąd połączenia z bazą.");
        }

        [Route("coach")]
        [HttpPost]
        public ActionResult<string> RegistrationTrener(TrenerPostModel form)
        {
            int? idUsera = KontoDbController.ReturnUserId(form.Email);

            if (idUsera != null)
                return Ok("Użytkownik o podanym email już istnieje.");

            if (form == null) return Ok("Błąd danych.");

            return Ok(TrenerDbController.Add(form));
        }
    }
}
