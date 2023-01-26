namespace LOFitAPI.Models
{
    public class TreningModel
    {
        public int Id { get; set; }
        public int Id_usera { get; set; }
        public string Nazwa { get; set; }
        public TimeOnly Czas { get; set; }
        public string Opis { get; set; }
        public DateTime Data_czas { get; set; }
        public bool W_bazie_usera { get; set; }

    }
}
