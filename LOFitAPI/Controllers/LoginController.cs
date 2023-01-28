using LOFitAPI.Controllers.PostModels.Login;
using LOFitAPI.DbControllers.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LOFitAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration { get; set; }
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost]
        public ActionResult<string> Authenticate(LoginPostModel form)
        {
            if (form == null || form.Email == null || form.Email == string.Empty) return BadRequest();
            if (form.Password == null || form.Password == string.Empty) return BadRequest();

            if (!KontoDbController.IsOkLogin(form)) return Unauthorized();

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("sub", form.Email)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);
            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        [Route("sendcode")]
        [HttpPost]
        public ActionResult<string> SendCode(string email)
        {
            if (email == null || email == string.Empty) return BadRequest();

            if (!KontoDbController.SendForgottenCode(email)) return Ok($"Nie ma takiego konta: {email}.");

            return Ok($"Wysłano wiadomość na adres {email}.");

        }
    }
}
