using LOFitAPI.Enums;

namespace LOFitAPI.Controllers.PostModels.Registration
{
    public class TrenerPostModel
    {
        public string Email { get; set; }
        public string Haslo { get; set; }
        public string Imie { get; set; }
        public string? Nazwisko { get; set; }
        public int Plec { get; set; }
        public DateTime? Data_urodzenia { get; set; }
        public int? Nr_telefonu { get; set; }
        public string? Miejscowosc { get; set; }
        public int Typ_trenera { get; set; }
    }
}
