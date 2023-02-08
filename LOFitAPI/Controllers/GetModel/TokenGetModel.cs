namespace LOFitAPI.Controllers.GetModel
{
    public class TokenGetModel
    {
        public int Typ { get; set; }
        public string Token { get; set; }

        public TokenGetModel(int typ, string token)
        {
            Typ = typ;
            Token = token;
        }
    }
}
