namespace LOFitAPI.DbModels.MenuCoach
{
    public class PlanTygodniowyModel
    {
        public int Id { get; set; }
        public int Id_trenera { get; set; }
        public int Typ { get; set; }
        public string Nazwa { get; set; }
        public int Dzien1 { get; set; }
        public int Dzien2 { get; set; }
        public int Dzien3 { get; set; }
        public int Dzien4 { get; set; }
        public int Dzien5 { get; set; }
        public int Dzien6 { get; set; }
        public int Dzien7 { get; set; }
        public int Kcla { get; set; }
    }
}
