namespace LOFitAPI.DbModels
{
    public class ZgloszenieModel
    {
        public int Id { get; set; }
        public int Id_trenera { get; set; }
        public int Id_usera { get; set; }
        public string Opis { get; set; }
    }
}