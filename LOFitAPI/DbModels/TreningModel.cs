namespace LOFitAPI.DbModels
{
    public class TreningModel
    {
        public int Id { get; set; }
        public int Id_usera { get; set; }
        public string Nazwa { get; set; }
        public DateTime? Czas { get; set; } //time
        public string? Opis { get; set; }
        public bool W_bazie_usera { get; set; }

    }
}
