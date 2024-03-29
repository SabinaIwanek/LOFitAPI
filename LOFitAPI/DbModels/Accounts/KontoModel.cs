﻿using LOFitAPI.Enums;

namespace LOFitAPI.DbModels.Accounts
{
    public class KontoModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Haslo { get; set; }
        public TypKonta Typ_konta { get; set; }
        public int Zablokowane { get; set; }
        public int Id_uzytkownika { get; set; }
        public int? Kod_jednorazowy { get; set; }
        public DateTime? Data_czas_kodu { get; set; }
    }
}