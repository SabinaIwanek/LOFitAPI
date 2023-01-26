﻿using LOFitAPI.Enums;

namespace LOFitAPI.Models
{
    public class TrenerModel
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public Plec Plec { get; set; }
        public DateOnly Data_urodzenia { get; set; }
        public int Nr_telefonu { get; set; }
        public string Opis_profilu { get; set; }
        public string Miejscowosc { get; set; }
        public decimal Cena_treningu { get; set; }
        public TimeOnly Czas_treningu { get; set; }
        public decimal Cena_dieta { get; set; }
        public TimeOnly Czas_dieta { get; set; }
        public StatusWeryfikacji Zatwierdzony_dietetyk { get; set; }
        public StatusWeryfikacji Zatwierdzony_trener { get; set; }
    }
}