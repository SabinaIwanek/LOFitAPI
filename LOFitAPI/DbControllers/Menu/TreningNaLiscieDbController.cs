using LOFitAPI.DbModels.Menu;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers.Menu
{
    public static class TreningNaLiscieDbController
    {
        private static TreningNaLiscieModel Struktura(SqlDataReader reader)
        {
            TreningNaLiscieModel model = new TreningNaLiscieModel();

            model.Id = (int)reader[0];
            model.Id_usera = (int)reader[1];
            try { model.Id_trenera = (int)reader[2]; } catch { model.Id_trenera = null; }
            model.Id_treningu = (int)reader[3];
            try { model.Czas = new DateTime(1970, 1, 1) + (TimeSpan)reader[4]; } catch { model.Czas = null; }
            try { model.Kcla = (int)reader[5]; } catch { model.Id_trenera = null; }
            try { model.Opis = (string)reader[6]; } catch { model.Opis = null; }
            model.Data_czas = (DateTime)reader[7];
            model.Zatwierdzony = (int)reader[8] > 0 ? true : false;
            try { model.Id_planu = (int)reader[9]; } catch { model.Id_planu = null; }

            return model;
        }
        public static int Add(TreningNaLiscieModel model)
        {
            int id = 0;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"INSERT INTO TreningNaLiscie VALUES({model.Id_usera},{SqlTools.ReturnInt(model.Id_trenera)}, {model.Id_treningu},{SqlTools.ReturnTime(model.Czas)},{SqlTools.ReturnInt(model.Kcla)},{SqlTools.ReturnString(model.Opis)},{SqlTools.ReturnDateTime(model.Data_czas)},{SqlTools.ReturnBool(model.Zatwierdzony)}, {SqlTools.ReturnInt(model.Id_planu)}); SELECT SCOPE_IDENTITY();";

                    SqlCommand command = new SqlCommand(query, Connection);
                    //SqlDataReader reader = command.ExecuteReader();
                    id = Convert.ToInt32(command.ExecuteScalar());

                    //reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }

            return id;
        }
        public static string Update(TreningNaLiscieModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"UPDATE TreningNaLiscie SET id_usera={model.Id_usera},id_trenera={SqlTools.ReturnInt(model.Id_trenera)},id_treningu={model.Id_treningu}, czas={SqlTools.ReturnTime(model.Czas)},kcla={SqlTools.ReturnInt(model.Kcla)},opis={SqlTools.ReturnString(model.Opis)},data_czas={SqlTools.ReturnDateTime(model.Data_czas)},zatwierdzony={SqlTools.ReturnBool(model.Zatwierdzony)}, id_planu={SqlTools.ReturnInt(model.Id_planu)} WHERE id = {SqlTools.ReturnString(model.Id)}";

                    SqlCommand command = new SqlCommand(query, Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }

            return "Ok";
        }
        public static string Delete(int id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();
                    //Utworzenie Użytkownika
                    string query = $"DELETE FROM TreningNaLiscie WHERE id={id};";

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
        public static string CheckedBoxChange(int id, int check)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();
                    string query = $"UPDATE TreningNaLiscie SET zatwierdzony = {check} WHERE id={id};";

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
        public static TreningNaLiscieModel GetOne(int id)
        {
            TreningNaLiscieModel model = new TreningNaLiscieModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from TreningNaLiscie WHERE id = {id}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model = Struktura(reader);
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                }
            }

            return model;
        }
        public static TreningNaLiscieModel GetOnePlan(int id)
        {
            TreningNaLiscieModel model = new TreningNaLiscieModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from TreningNaLiscie WHERE id_planu = {id}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model = Struktura(reader);
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                }
            }

            return model;
        }
        public static List<TreningNaLiscieModel> GetUserList(int Id_usera, DateTime date)
        {
            List<TreningNaLiscieModel> list = new List<TreningNaLiscieModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from TreningNaLiscie WHERE Id_usera = {Id_usera} AND CAST(data_czas AS DATE) = {SqlTools.ReturnDate(date)}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(Struktura(reader));
                    }

                    reader.Close();

                    foreach (var item in list)
                    {
                        TreningModel trening = new TreningModel();
                        command = new SqlCommand($"Select * from Trening WHERE id = {item.Id_treningu}", Connection);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            trening.Id = (int)reader[0];
                            try { trening.Id_konta = (int)reader[1]; } catch { trening.Id_konta = null; }
                            trening.Nazwa = (string)reader[2];
                            try { trening.Czas = new DateTime(1970, 1, 1) + (TimeSpan)reader[3]; } catch { trening.Czas = null; }
                            try { trening.Kcla = (int)reader[4]; } catch { trening.Kcla = null; }
                            try { trening.Opis = reader[5].ToString(); } catch { trening.Opis = null; }
                            trening.W_bazie_usera = (int)reader[6] > 0 ? true : false;
                        }

                        item.Trening = trening;

                        reader.Close();
                    }

                    Connection.Close();
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                }
            }

            return list;
        }
    }
}