namespace LOFitAPI.DbModels.MenuCoach
{
    public class TerminModel
    {
        public int Id { get; set; }
        public int Id_trenera { get; set; }
        public int Id_usera { get; set; }
        public DateTime Termin_od { get; set; }
        public DateTime Termin_do { get; set; }
        public bool Zatwierdzony { get; set; }
    }
}
