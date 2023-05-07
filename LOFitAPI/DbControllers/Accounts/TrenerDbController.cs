using LOFitAPI.Controllers.PostModels.Registration;
using LOFitAPI.DbModels.Accounts;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers.Accounts
{
    public static class TrenerDbController
    {
        private static TrenerModel Struktura(SqlDataReader reader)
        {
            TrenerModel model = new TrenerModel();

            model.Id = (int)reader[0];
            model.Imie = (string)reader[1];
            try { model.Nazwisko = reader[2].ToString(); } catch { model.Nazwisko = null; }
            model.Plec = (int)reader[3];
            try { model.Data_urodzenia = (DateTime)reader[4]; } catch { model.Data_urodzenia = null; }
            try { model.Nr_telefonu = (int)reader[5]; } catch { model.Nr_telefonu = null; }
            try { model.Opis_profilu = reader[6].ToString(); } catch { model.Opis_profilu = null; }
            try { model.Miejscowosc = reader[7].ToString(); } catch { model.Miejscowosc = null; }
            try { model.Cena_uslugi = (decimal)reader[8]; } catch { model.Cena_uslugi = null; }
            try { model.Czas_uslugi_min = (int)reader[9]; } catch { model.Czas_uslugi_min = null; }
            model.Zatwierdzony_dietetyk = (int)reader[10];
            model.Zatwierdzony_trener = (int)reader[11];
            model.Data_zalozenia = (DateTime)reader[12];

            return model;
        }
        public static string Add(TrenerPostModel form)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {

                    int trener = form.Typ_trenera == 2 || form.Typ_trenera == 0 ? 0 : 3;
                    int dietetyk = form.Typ_trenera == 2 || form.Typ_trenera == 1 ? 0 : 3;

                    Connection.Open();
                    //Utworzenie Użytkownika
                    string query = $"Insert INTO Trener VALUES('{form.Imie}',{SqlTools.ReturnString(form.Nazwisko)},{form.Plec},{SqlTools.ReturnDate(form.Data_urodzenia)}, {SqlTools.ReturnInt(form.Nr_telefonu)},NULL,{SqlTools.ReturnString(form.Miejscowosc)}, NULL, NULL,{dietetyk},{trener},{SqlTools.ReturnDateTime(DateTime.Now)})";

                    SqlCommand command = new SqlCommand(query, Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Close();

                    //Odczyt id utworzonego użytkownika
                    string query2 = "SELECT TOP 1 * FROM Trener ORDER BY data_zalozenia DESC";

                    SqlCommand command2 = new SqlCommand(query2, Connection);
                    SqlDataReader reader2 = command2.ExecuteReader();

                    int id = -1;

                    while (reader2.Read())
                    {
                        id = (int)reader2[0];
                    }

                    reader2.Close();

                    //Utworzenie konta
                    string query3 = $"INSERT INTO Konto VALUES ('{form.Email}','{form.Haslo}',{2},{0},{id},NULL,NULL)";

                    SqlCommand command3 = new SqlCommand(query3, Connection);
                    SqlDataReader reader3 = command3.ExecuteReader();

                    reader3.Close();

                    Connection.Close();

                }

                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public static string Update(TrenerModel form)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();
                    //Utworzenie Użytkownika
                    string query = $"UPDATE Trener SET imie ='{form.Imie}', nazwisko={SqlTools.ReturnString(form.Nazwisko)}, plec={form.Plec}, data_urodzenia={SqlTools.ReturnDate(form.Data_urodzenia)}, nr_telefonu={SqlTools.ReturnInt(form.Nr_telefonu)}, opis_profilu={SqlTools.ReturnString(form.Opis_profilu)}, miejscowosc={SqlTools.ReturnString(form.Miejscowosc)}, cena_uslugi={SqlTools.ReturnDecimal(form.Cena_uslugi)},czas_uslugi_min={SqlTools.ReturnInt(form.Czas_uslugi_min)}, zatwierdzony_dietetyk={form.Zatwierdzony_dietetyk},zatwierdzony_trener={form.Zatwierdzony_trener} WHERE id = {SqlTools.ReturnString(form.Id)}";

                    SqlCommand command = new SqlCommand(query, Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Close();
                    Connection.Close();

                }

                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public static List<TrenerModel> GetAll()
        {
            List<TrenerModel> list = new List<TrenerModel>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"SELECT * FROM Trener", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(Struktura(reader));
                    }

                    reader.Close();
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();

                return new List<TrenerModel>();
            }
            return list;
        }
        public static TrenerModel GetOne(int id)
        {
            TrenerModel model = new TrenerModel();
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"SELECT * FROM Trener WHERE id ={id}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model = Struktura(reader);
                    }

                    reader.Close();
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();

                return new TrenerModel();
            }

            return model;
        }
        
        // Admin
        public static List<TrenerModel> GetWgType(int statusWeryfikacji)
        {
            List<TrenerModel> list = new List<TrenerModel>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"SELECT * FROM Trener WHERE zatwierdzony_trener = {statusWeryfikacji}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(Struktura(reader));
                    }

                    reader.Close();
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();

                return new List<TrenerModel>();
            }
            return list;
        }
        public static string SetState(int id, int state)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();

                    string query = $"UPDATE Trener SET zatwierdzony_dietetyk={state},zatwierdzony_trener={state} WHERE id = {id}";

                    SqlCommand command = new SqlCommand(query, Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Close();
                    Connection.Close();
                }

                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}