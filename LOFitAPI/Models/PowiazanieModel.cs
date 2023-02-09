namespace LOFitAPI.Models
{
    public class PowiazanieModel
    {
        public int Id { get; set; }
        public int Id_trenera { get; set; }
        public int Id_user { get; set; }
        public DateTime Czas_od { get; set; } 
        public DateTime Czas_do { get; set; } 
        public bool Zatwierdzone { get; set; }
        public bool Podglad_pelny { get; set; }
        public DateTime Podglad_od_daty { get; set; }
    }
}