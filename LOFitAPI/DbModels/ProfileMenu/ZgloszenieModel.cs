namespace LOFitAPI.DbModels.ProfileMenu
{
    public class ZgloszenieModel
    {
        public int Id { get; set; }
        public int Id_trenera { get; set; }
        public int Id_usera { get; set; }
        public string Opis { get; set; }
        public int Status_weryfikacji { get; set; }
    }
}