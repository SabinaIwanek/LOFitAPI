using LOFitAPI.DbModels;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers
{
    public static class TreningDbController
    {
        public static int Add(TreningModel model)
        {
            int id = 0;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"INSERT INTO Trening VALUES({SqlTools.ReturnInt(model.Id_usera)},{SqlTools.ReturnString(model.Nazwa)}, {SqlTools.ReturnTime(model.Czas)},{SqlTools.ReturnString(model.Opis)}, {SqlTools.ReturnBool(model.W_bazie_usera)}, {SqlTools.ReturnDateTime(DateTime.Now)})";

                    SqlCommand command = new SqlCommand(query, Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Close();

                    string query2 = $"SELECT TOP 1 * FROM Trening WHERE id_usera ={model.Id_usera} ORDER BY data_zalozenia DESC";

                    SqlCommand command2 = new SqlCommand(query2, Connection);
                    SqlDataReader reader2 = command2.ExecuteReader();

                    while (reader2.Read())
                    {
                        id = (int)reader2[0];
                    }

                    reader2.Close();

                    Connection.Close();
                }
                catch (Exception ex)
                {
                    return id;
                }
            }

            return id;
        }
        public static string Update(TreningModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"UPDATE Trening SET id_usera={SqlTools.ReturnInt(model.Id_usera)},nazwa={SqlTools.ReturnString(model.Nazwa)}, czas={SqlTools.ReturnTime(model.Czas)},Opis={SqlTools.ReturnString(model.Opis)},w_bazie_usera={SqlTools.ReturnBool(model.W_bazie_usera)} WHERE id = {SqlTools.ReturnString(model.Id)}";

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
                    string query = $"DELETE FROM Trening WHERE id={id};";

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
        public static TreningModel GetOne(int id)
        {
            TreningModel model = new TreningModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Trening WHERE id = {id}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Id = (int)reader[0];
                        model.Id_usera = (int)reader[1];
                        model.Nazwa = (string)reader[2];
                        try { model.Czas = (DateTime)reader[3]; } catch { model.Czas = null; }
                        try { model.Opis = reader[4].ToString(); } catch { model.Opis = null; }
                        model.W_bazie_usera = (int)reader[5] > 0? true : false;
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                { }
            }

            return model;
        }
        public static List<TreningModel> GetUserList(int Id_usera)
        {
            List<TreningModel> list = new List<TreningModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Trening WHERE Id_usera = {Id_usera}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TreningModel model = new TreningModel();

                        model.Id = (int)reader[0];
                        model.Id_usera = (int)reader[1];
                        model.Nazwa = (string)reader[2];
                        try { model.Czas = (DateTime)reader[3]; } catch { model.Czas = null; }
                        try { model.Opis = reader[4].ToString(); } catch { model.Opis = null; }
                        model.W_bazie_usera = (int)reader[5] > 0 ? true : false;

                        list.Add(model);
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                { }
            }

            return list;
        }
        public static List<TreningModel> GetAppList()
        {
            List<TreningModel> list = new List<TreningModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Trening WHERE w_bazie_programu=1", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TreningModel model = new TreningModel();

                        model.Id = (int)reader[0];
                        model.Id_usera = (int)reader[1];
                        model.Nazwa = (string)reader[2];
                        try { model.Czas = (DateTime)reader[3]; } catch { model.Czas = null; }
                        try { model.Opis = reader[4].ToString(); } catch { model.Opis = null; }
                        model.W_bazie_usera = (int)reader[5] > 0 ? true : false;

                        list.Add(model);
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                { }
            }

            return list;
        }
    }
}
