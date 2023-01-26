namespace LOFitAPI.Models
{
    public class PomiarModel
    {
        public int Id { get; set; }
        public int Id_usera { get; set; }
        public DateOnly Data_pomiaru { get; set; }
        public decimal Waga { get; set; }
        public decimal Procent_tluszczu { get; set; }
        public decimal Biceps { get; set; }
        public decimal Klatka_piersiowa { get; set; }
        public decimal Pod_klatka_piersiowa { get; set; }
        public decimal Talia { get; set; }
        public decimal Pas { get; set; }
        public decimal Posladki { get; set; }
        public decimal Udo { get; set; }
        public decimal Kolano { get; set; }
        public decimal Łydka { get; set; }
    }
}
