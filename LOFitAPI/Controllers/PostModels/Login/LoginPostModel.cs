namespace LOFitAPI.Controllers.PostModels.Login
{
    public class LoginPostModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Code { get; set; }
        public bool IsCode { get; set; }
    }
}
