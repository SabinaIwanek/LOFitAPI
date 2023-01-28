namespace LOFitAPI.Controllers.PostModels.Login
{
    public class LoginPostModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
        public bool IsCode { get; set; }
    }
}
