using LOFitAPI.Controllers.PostModels.Registration;
using LOFitAPI.DbModels.Accounts;
using LOFitAPI.DbModels.MenuCoach;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers.Accounts
{
    public static class UzytkownikDbController
    {
        private static UzytkownikModel Struktura(SqlDataReader reader)
        {
            UzytkownikModel model = new UzytkownikModel();

            model.Id = (int)reader[0];
            model.Imie = (string)reader[1];
            try { model.Nazwisko = reader[2].ToString(); } catch { model.Nazwisko = null; }
            model.Plec = (int)reader[3];
            try { model.Data_urodzenia = (DateTime)reader[4]; } catch { model.Data_urodzenia = null; }
            try { model.Id_trenera = (int)reader[5]; } catch { model.Id_trenera = null; }
            try { model.Id_dietetyka = (int)reader[6]; } catch { model.Id_dietetyka = null; }
            try { model.Nr_telefonu = (int)reader[7]; } catch { model.Nr_telefonu = null; }
            try { model.Waga_poczatkowa = (int)reader[9]; } catch { model.Waga_poczatkowa = null; }
            try { model.Waga_cel = (int)reader[10]; } catch { model.Waga_cel = null; }
            try { model.Kcla_dzien = (int)reader[11]; } catch { model.Kcla_dzien = null; }
            try { model.Kcla_dzien_trening = (int)reader[12]; } catch { model.Kcla_dzien_trening = null; }

            return model;
        }
        public static bool Add(UzytkownikPostModel form)
        {
            int id = -1;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    //Utworzenie Użytkownika
                    string query = $"INSERT INTO Uzytkownik VALUES('{form.Imie}', {SqlTools.ReturnString(form.Nazwisko)}, {form.Plec}, {SqlTools.ReturnDate(form.Data_urodzenia)}, NULL, NULL, {SqlTools.ReturnInt(form.Nr_telefonu)},{SqlTools.ReturnDateTime(DateTime.Now)},NULL,NULL,NULL,NULL); SELECT SCOPE_IDENTITY();";

                    SqlCommand command = new SqlCommand(query, Connection);
                    id = Convert.ToInt32(command.ExecuteScalar());

                    //Utworzenie konta
                    string query3 = $"INSERT INTO Konto VALUES ('{form.Email}','{form.Haslo}',{1},{0},{id}, NULL,NULL)";

                    SqlCommand command3 = new SqlCommand(query3, Connection);
                    SqlDataReader reader3 = command3.ExecuteReader();

                    reader3.Close();

                    Connection.Close();
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();

                    return false;
                }
            }

            return true;
        }
        public static string Update(UzytkownikModel form)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();
                    //Utworzenie Użytkownika
                    string query = $"UPDATE Uzytkownik SET imie ='{form.Imie}', nazwisko={SqlTools.ReturnString(form.Nazwisko)}, plec={form.Plec}, data_urodzenia={SqlTools.ReturnDate(form.Data_urodzenia)}, id_trenera={SqlTools.ReturnString(form.Id_trenera)}, id_dietetyka={SqlTools.ReturnString(form.Id_dietetyka)}, nr_telefonu={SqlTools.ReturnInt(form.Nr_telefonu)}, waga_poczatkowa={SqlTools.ReturnInt(form.Waga_poczatkowa)}, waga_cel={SqlTools.ReturnInt(form.Waga_cel)}, kcla_dzien={SqlTools.ReturnInt(form.Kcla_dzien)}, kcla_dzien_trening={SqlTools.ReturnInt(form.Kcla_dzien_trening)} WHERE id = {form.Id}";

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
        public static UzytkownikModel GetOne(int id)
        {
            UzytkownikModel model = new UzytkownikModel();
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"SELECT * FROM Uzytkownik WHERE id ={id}", Connection);
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

                return new UzytkownikModel();
            }

            return model;
        }
        public static List<UzytkownikModel> GetAll()
        {
            List<UzytkownikModel> list = new List<UzytkownikModel>();

            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"SELECT * FROM Uzytkownik", Connection);
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

                return new List<UzytkownikModel>();
            }

            return list;
        }
    }
}