namespace LOFitAPI.Models
{
    public class UzytkownikModel
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string? Nazwisko { get; set; }
        public int? Plec { get; set; } //Plec
        public DateTime? Data_urodzenia { get; set; } //DateOnly
        public int? Id_trenera { get; set; }
        public int? Id_dietetyka { get; set; }
        public int? Nr_telefonu { get; set; }
    }
}