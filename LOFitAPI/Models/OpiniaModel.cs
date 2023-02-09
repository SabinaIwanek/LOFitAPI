namespace LOFitAPI.Models
{
    public class OpiniaModel
    {
        public int Id { get; set; }
        public int Id_usera { get; set; }
        public int Id_trenera { get; set; }
        public string Opis { get; set; }
        public int Ocena { get; set; }
        public int Zgloszona { get; set; }
        public string Opis_zgloczenia { get; set; }
    }
}