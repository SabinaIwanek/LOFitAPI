using LOFitAPI.Enums;

namespace LOFitAPI.Models
{
    public class UzytkownikModel
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public Plec Plec { get; set; }
        public DateOnly Data_urodzenia { get; set; }
        public int Id_trenera { get; set; }
        public int Id_dietetyka { get; set; }
        public int Nr_telefonu { get; set; }
    }
}
