namespace LOFitAPI.DbModels
{
    public class PowiazanieModel
    {
        public int Id { get; set; }
        public int Id_trenera { get; set; }
        public int Id_usera { get; set; }
        public DateTime Czas_od { get; set; } 
        public DateTime Czas_do { get; set; } 
        public int Zatwierdzone { get; set; }
        public int Podglad_pelny { get; set; }
        public DateTime? Podglad_od_daty { get; set; }
    }
}