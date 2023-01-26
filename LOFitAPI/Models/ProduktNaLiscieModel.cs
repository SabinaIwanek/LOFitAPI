namespace LOFitAPI.Models
{
    public class ProduktNaLiscieModel
    {
        public int Id { get; set; }
        public int Id_produktu { get; set; }
        public int Id_usera { get; set; }
        public int Gramy { get; set; }
        public bool W_bazie_usera { get; set; }
        public DateTime Data_czas { get; set; }
        public string Opis_od_trenera { get; set; }
        public int Id_trenera { get; set; }
        public string Nazwa_dania { get; set; }
        public bool Zatwierdzony { get; set; }
    }
}
