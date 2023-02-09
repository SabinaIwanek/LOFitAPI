using LOFitAPI.Enums;

namespace LOFitAPI.Models
{
    public class TrenerModel
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Plec { get; set; } //Plec
        public DateTime? Data_urodzenia { get; set; } //DateOnly
        public int? Nr_telefonu { get; set; }
        public string Opis_profilu { get; set; }
        public string? Miejscowosc { get; set; }
        public decimal? Cena_treningu { get; set; }
        public int Czas_treningu_min { get; set; }
        public decimal? Cena_dieta { get; set; }
        public int Czas_dieta_min { get; set; }
        public int Zatwierdzony_dietetyk { get; set; } //StatusWeryfikacji
        public int Zatwierdzony_trener { get; set; } //StatusWeryfikacji
    }
}
