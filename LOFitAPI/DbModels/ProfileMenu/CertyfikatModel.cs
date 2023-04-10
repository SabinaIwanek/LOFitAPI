namespace LOFitAPI.DbModels.ProfileMenu
{
    public class CertyfikatModel
    {
        public int Id { get; set; }
        public int Id_trenera { get; set; }
        public string Nazwa { get; set; }
        public string Organizacja { get; set; }
        public DateTime Data_certyfikatu { get; set; } //DateOnly
        public string? Kod_certyfikatu { get; set; }
        public int Zatwierdzony { get; set; } //StatusWeryfikacji
    }
}