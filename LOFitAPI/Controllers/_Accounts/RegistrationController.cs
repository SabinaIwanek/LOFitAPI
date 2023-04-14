﻿using LOFitAPI.Controllers.PostModels.Registration;
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
            if (form == null) return Ok("Błąd danych.");

            if (AdminDbController.Add(form))
                return Ok("Ok");

            return Ok("Błąd połączenia z bazą.");
        }

        [Route("coach")]
        [HttpPost]
        public ActionResult<string> RegistrationTrener(TrenerPostModel form)
        {
            if (form == null) return Ok("Błąd danych.");

            return Ok(TrenerDbController.Add(form));
        }
    }
}