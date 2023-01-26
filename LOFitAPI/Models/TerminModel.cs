using LOFitAPI.Enums;

namespace LOFitAPI.Models
{
    public class TerminModel
    {
        public int Id { get; set; }
        public int Id_trenera { get; set; }
        public DateOnly Data_terminu { get; set; }
        public TimeOnly Czas_od { get; set; }
        public TimeOnly Czas_do { get; set; }
        public TypTrenera Typ { get; set; }
        public int Id_usera { get; set; }
        public decimal Cena_treningu { get; set; }
        public decimal Cena_diety { get; set; }
        public StatusWeryfikacji Zatwierdzony { get; set; }
    }
}
