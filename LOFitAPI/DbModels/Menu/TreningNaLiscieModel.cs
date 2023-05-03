namespace LOFitAPI.DbModels.Menu
{
    public class TreningNaLiscieModel
    {
        public int Id { get; set; }
        public int Id_usera { get; set; }
        public int? Id_trenera { get; set; }
        public int Id_treningu { get; set; }
        public DateTime? Czas { get; set; }
        public int? Kcla { get; set; }
        public string? Opis { get; set; }
        public DateTime Data_czas { get; set; }
        public bool Zatwierdzony { get; set; }
        public int? Id_planu { get; set; }
        public TreningModel Trening { get; set; }
    }
}
