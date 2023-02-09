namespace LOFitAPI.DbModels
{
    public class TreningNaLiscie
    {
        public int Id { get; set; }
        public int Id_usera { get; set; }
        public int? Id_trenera { get; set; }
        public int Id_treningu { get; set; }
        public bool Zatwierdzony { get; set; }
    }
}
