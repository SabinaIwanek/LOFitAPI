namespace LOFitAPI.Models
{
    public class ProduktModel
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public int? Ean { get; set; }
        public int Kcla { get; set; }
        public int Gramy { get; set; }
        public int W_bazie_programu { get; set; } //StatusWeryfikacji
    }
}