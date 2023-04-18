namespace LOFitAPI.DbModels.Menu
{
    public class ProduktModel
    {
        public int Id { get; set; }
        public int? Id_konta { get; set; }
        public string Nazwa { get; set; }
        public int? Ean { get; set; }
        public int Gramy { get; set; }
        public int Kcla { get; set; }
        public int? Bialko { get; set; }
        public int? Tluszcze { get; set; }
        public int? Wegle { get; set; }
        public int W_bazie_programu { get; set; } //StatusWeryfikacji
    }
}