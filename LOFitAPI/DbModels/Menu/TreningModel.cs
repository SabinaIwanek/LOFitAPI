namespace LOFitAPI.DbModels.Menu
{
    public class TreningModel
    {
        public int Id { get; set; }
        public int? Id_konta { get; set; }
        public string Nazwa { get; set; }
        public DateTime? Czas { get; set; } //time
        public int? Kcla { get; set; } 
        public string? Opis { get; set; }
        public bool W_bazie_usera { get; set; }

    }
}
