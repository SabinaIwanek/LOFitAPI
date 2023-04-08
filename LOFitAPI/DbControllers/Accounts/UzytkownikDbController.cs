using LOFitAPI.Controllers.PostModels.Registration;
using LOFitAPI.DbModels;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers.Accounts
{
    public static class UzytkownikDbController
    {
        public static bool Add(UzytkownikPostModel form)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    //Utworzenie Użytkownika
                    string query = $"INSERT INTO Uzytkownik VALUES('{form.Imie}', {SqlTools.ReturnString(form.Nazwisko)}, {form.Plec}, {SqlTools.ReturnDate(form.Data_urodzenia)}, NULL, NULL, {SqlTools.ReturnInt(form.Nr_telefonu)},{SqlTools.ReturnDateTime(DateTime.Now)})";

                    SqlCommand command = new SqlCommand(query, Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Close();

                    //Odczyt id utworzonego użytkownika
                    string query2 = "SELECT TOP 1 * FROM Uzytkownik ORDER BY data_zalozenia DESC";

                    SqlCommand command2 = new SqlCommand(query2, Connection);
                    SqlDataReader reader2 = command2.ExecuteReader();

                    int id = -1;

                    while (reader2.Read())
                    {
                        id = (int)reader2[0];
                    }

                    reader2.Close();

                    //Utworzenie konta
                    string query3 = $"INSERT INTO Konto VALUES ('{form.Email}','{form.Haslo}',{1},{0},{id}, NULL,NULL)";

                    SqlCommand command3 = new SqlCommand(query3, Connection);
                    SqlDataReader reader3 = command3.ExecuteReader();

                    reader3.Close();

                    Connection.Close();
                }
                catch(Exception ex)
                {
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
                    string query = $"UPDATE Uzytkownik SET imie ={form.Imie}, nazwisko={SqlTools.ReturnString(form.Nazwisko)}, plec={form.Plec}, data_urodzenia={SqlTools.ReturnDate(form.Data_urodzenia)}, id_trenera={SqlTools.ReturnString(form.Id_trenera)}, id_dietetyka={SqlTools.ReturnString(form.Id_dietetyka)}, nr_telefonu={SqlTools.ReturnInt(form.Nr_telefonu)} WHERE id = {SqlTools.ReturnString(form.Id)}";

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
                        model.Id = (int)reader[0];
                        model.Imie = (string)reader[1];
                        try { model.Nazwisko = reader[2].ToString(); } catch { model.Nazwisko = null; }
                        model.Plec = (int)reader[3];
                        try { model.Data_urodzenia = (DateTime)reader[4]; } catch { model.Data_urodzenia = null; }
                        try { model.Id_trenera = (int)reader[5]; } catch { model.Id_trenera = null; }
                        try { model.Id_dietetyka = (int)reader[6]; } catch { model.Id_dietetyka = null; }
                        try { model.Nr_telefonu = (int)reader[7]; } catch { model.Nr_telefonu = null; }
                    }

                    reader.Close();
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                return null;
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
                        UzytkownikModel model = new UzytkownikModel();

                        model.Id = (int)reader[0];
                        model.Imie = (string)reader[1];
                        try { model.Nazwisko = reader[2].ToString(); } catch { model.Nazwisko = null; }
                        model.Plec = (int)reader[3];
                        try { model.Data_urodzenia = (DateTime)reader[4]; } catch { model.Data_urodzenia = null; }
                        try { model.Id_trenera = (int)reader[5]; } catch { model.Id_trenera = null; }
                        try { model.Id_dietetyka = (int)reader[6]; } catch { model.Id_dietetyka = null; }
                        try { model.Nr_telefonu = (int)reader[7]; } catch { model.Nr_telefonu = null; }

                        list.Add(model);
                    }

                    reader.Close();
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return list;
        }
    }
}