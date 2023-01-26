using LOFitAPI.Enums;

namespace LOFitAPI.Models
{
    public class CertyfikatModel
    {
        public int Id { get; set; }
        public int Id_trenera { get; set; }
        public string Nazwa { get; set; }
        public string Organizacja { get; set; }
        public DateOnly Data_certyfikatu { get; set; }
        public string Kod_certyfikatu { get; set; }
        public StatusWeryfikacji Zatwierdzony { get; set; }
    }
}
