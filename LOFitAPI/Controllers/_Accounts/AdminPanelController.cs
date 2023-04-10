using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbControllers.ProffileMenu;
using LOFitAPI.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOFitAPI.Controllers._Accounts
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AdminPanelController : ControllerBase
    {
        [HttpGet]
        [Route("coachs/{type}")]
        public ActionResult<List<TrenerModel>> GetWgTypeCoach(int type)
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            List<TrenerModel> list = new List<TrenerModel>();

            list = TrenerDbController.GetWgType(type);

            return Ok(list);
        }
        [HttpGet]
        [Route("coachs/{id}/{type}")]
        public ActionResult<string> SetCoach(int id, int type)
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            string wynik = TrenerDbController.SetState(id, type);

            return Ok(wynik);
        }

        [HttpGet]
        [Route("certificate/{type}")]
        public ActionResult<List<CertyfikatModel>> GetWgTypeCert(int type)
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            List<CertyfikatModel> list = new List<CertyfikatModel>();

            list = CertyfikatDbController.GetWgType(type);

            return Ok(list);
        }
        [HttpGet]
        [Route("certificate/{id}/{type}")]
        public ActionResult<string> SetCert(int id, int type)
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            string wynik = CertyfikatDbController.SetState(id, type);

            return Ok(wynik);
        }

        [HttpGet]
        [Route("opinion/{type}")]
        public ActionResult<List<OpiniaModel>> GetWgTypeOpinion(int type)
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            List<OpiniaModel> list = new List<OpiniaModel>();

            list = OpiniaDbController.GetWgType(type);

            return Ok(list);
        }
        [HttpGet]
        [Route("opinion/{id}/{type}")]
        public ActionResult<string> SetOpinion(int id, int type)
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            string wynik = type == 0 ? OpiniaDbController.Delete(id) : OpiniaDbController.ResetState(id);

            return Ok(wynik);
        }

        [HttpGet]
        [Route("reports/{type}")]
        public ActionResult<List<ZgloszenieModel>> GetWgTypeReports(int type)
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            List<ZgloszenieModel> list = new List<ZgloszenieModel>();

            list = ZgloszenieDbController.GetWgType(type);

            return Ok(list);
        }

        [HttpGet]
        [Route("reports/{id}/{type}")]
        public ActionResult<string> SetReport(int id, int type)
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            string wynik = ZgloszenieDbController.SetState(id, type);

            return Ok(wynik);
        }

        [HttpGet]
        [Route("appusers/admins")]
        public ActionResult<List<AdministratorModel>> GetAppUsersAdmins()
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            List<AdministratorModel> list = new List<AdministratorModel>();

            list = AdminDbController.GetAll();

            return Ok(list);
        }

        [HttpGet]
        [Route("appusers/users")]
        public ActionResult<List<UzytkownikModel>> GetAppUsers()
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            List<UzytkownikModel> list = new List<UzytkownikModel>();

            list = UzytkownikDbController.GetAll();

            return Ok(list);
        }

        [HttpGet]
        [Route("appusers/coachs")]
        public ActionResult<List<TrenerModel>> GetAppUsersCoachs()
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            List<TrenerModel> list = new List<TrenerModel>();

            list = TrenerDbController.GetAll();

            return Ok(list);
        }

        [HttpGet]
        [Route("appusers/block/{id}/{type}")]
        public ActionResult<string> BlockAccount(int id, int type)
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            int? kontoId = KontoDbController.ReturnKontoId(id, type);
            if (kontoId == null) return "";

            string wynik = KontoDbController.BlockKonto((int)kontoId, 1);

            return Ok(wynik);
        }
        [HttpGet]
        [Route("appusers/unblock/{id}/{type}")]
        public ActionResult<string> UnblockAccount(int id, int type)
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            int? kontoId = KontoDbController.ReturnKontoId(id, type);
            if (kontoId == null) return "";

            string wynik = KontoDbController.BlockKonto((int)kontoId, 0);

            return Ok(wynik);
        }
        [HttpDelete]
        [Route("appusers/{id}/{type}")]
        public ActionResult<string> DeleteAccount(int id, int type)
        {
            int? accountType = KontoDbController.ReturnUserType(User.Identity?.Name);
            if (accountType == null || accountType != 0) return Unauthorized();

            string wynik = KontoDbController.DeleteKonto(id, type);

            return Ok(wynik);
        }
    }
}