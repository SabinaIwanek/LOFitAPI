﻿namespace LOFitAPI.DbModels.Accounts
{
    public class TrenerModel
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string? Nazwisko { get; set; }
        public int Plec { get; set; } //Plec
        public DateTime? Data_urodzenia { get; set; } //DateOnly
        public int? Nr_telefonu { get; set; }
        public string? Opis_profilu { get; set; }
        public string? Miejscowosc { get; set; }
        public decimal? Cena_uslugi { get; set; }
        public int? Czas_uslugi_min { get; set; }
        public int Zatwierdzony_dietetyk { get; set; } //StatusWeryfikacji
        public int Zatwierdzony_trener { get; set; } //StatusWeryfikacji
        public DateTime Data_zalozenia { get; set; }
    }
}
