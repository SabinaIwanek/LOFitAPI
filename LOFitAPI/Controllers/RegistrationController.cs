using LOFitAPI.Controllers.PostModels;
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
            if (form == null) return "Błąd danych.";

            if(UzytkownikDbController.Create(form))
                return Ok("Założono konto.");

            return "Błąd połączenia z bazą.";
        }
    }
}
