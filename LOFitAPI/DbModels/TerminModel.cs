namespace LOFitAPI.DbModels
{
    public class TerminModel
    {
        public int Id { get; set; }
        public int Id_trenera { get; set; }
        public DateTime Termin_od { get; set; }
        public DateTime Termin_do { get; set; }
        public int Typ { get; set; } //TypTrenera
        public int? Id_usera { get; set; }
        public decimal? Cena_treningu { get; set; }
        public decimal? Cena_diety { get; set; }
        public int Zatwierdzony { get; set; } //StatusWeryfikacji
    }
}
