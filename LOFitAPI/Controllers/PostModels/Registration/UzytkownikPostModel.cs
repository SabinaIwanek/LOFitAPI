namespace LOFitAPI.Controllers.PostModels.Registration
{
    public class UzytkownikPostModel
    {
        public string Email { get; set; }
        public string Haslo { get; set; }
        public string? Imie { get; set; }
        public string? Nazwisko { get; set; }
        public int Plec { get; set; }
        public DateTime? Data_urodzenia { get; set; }
        public int? Nr_telefonu { get; set; }

        
        public UzytkownikPostModel(TrenerPostModel trenerModel)
        {
            Email = trenerModel.Email;
            Haslo = trenerModel.Haslo;
            Imie = trenerModel.Imie;
            Nazwisko = trenerModel.Nazwisko;
            Plec = trenerModel.Plec;
            Data_urodzenia = trenerModel?.Data_urodzenia;
            Nr_telefonu = trenerModel?.Nr_telefonu;
        }
    }
}