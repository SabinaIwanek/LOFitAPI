using LOFitAPI.DbModels.Menu;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers.Menu
{
    public static class ProduktDbController
    {
        public static int Add(ProduktModel model)
        {
            int id = 0;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"INSERT INTO Produkt VALUES({SqlTools.ReturnInt(model.Id_usera)},{SqlTools.ReturnString(model.Nazwa)}, {SqlTools.ReturnInt(model.Ean)},{model.Gramy}, {model.Kcla},{SqlTools.ReturnInt(model.Bialko)},{SqlTools.ReturnInt(model.Tluszcze)},{SqlTools.ReturnInt(model.Wegle)}, {model.W_bazie_programu},{SqlTools.ReturnDateTime(DateTime.Now)})";

                    SqlCommand command = new SqlCommand(query, Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Close();

                    string query2 = $"SELECT TOP 1 * FROM Produkt WHERE id_usera ={model.Id_usera} ORDER BY data_zalozenia DESC";

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
        public static string Update(ProduktModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"UPDATE Produkt SET id_usera={SqlTools.ReturnInt(model.Id_usera)},nazwa={SqlTools.ReturnString(model.Nazwa)}, ean={SqlTools.ReturnInt(model.Ean)},gramy={model.Gramy}, kcla={model.Kcla},bialko={SqlTools.ReturnInt(model.Bialko)},tluszcze={SqlTools.ReturnInt(model.Tluszcze)},wegle={SqlTools.ReturnInt(model.Wegle)},w_bazie_programu={model.W_bazie_programu} WHERE id = {SqlTools.ReturnString(model.Id)}";

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
                    string query = $"DELETE FROM Produkt WHERE id={id};";

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
        public static ProduktModel GetOne(int id)
        {
            ProduktModel model = new ProduktModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Produkt WHERE id = {id}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Id = (int)reader[0];
                        model.Id_usera = (int)reader[1];
                        model.Nazwa = (string)reader[2];
                        try { model.Ean = (int)reader[3]; } catch { model.Ean = null; }
                        model.Gramy = (int)reader[4];
                        model.Kcla = (int)reader[5];
                        try { model.Bialko = (int)reader[6]; } catch { model.Bialko = null; }
                        try { model.Tluszcze = (int)reader[7]; } catch { model.Tluszcze = null; }
                        try { model.Wegle = (int)reader[8]; } catch { model.Wegle = null; }
                        model.W_bazie_programu = (int)reader[9];
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                { }
            }

            return model;
        }
        public static List<ProduktModel> GetUserList(int Id_usera)
        {
            List<ProduktModel> list = new List<ProduktModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Produkt WHERE Id_usera = {Id_usera}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ProduktModel model = new ProduktModel();

                        model.Id = (int)reader[0];
                        model.Id_usera = (int)reader[1];
                        model.Nazwa = (string)reader[2];
                        try { model.Ean = (int)reader[3]; } catch { model.Ean = null; }
                        model.Gramy = (int)reader[4];
                        model.Kcla = (int)reader[5];
                        try { model.Bialko = (int)reader[6]; } catch { model.Bialko = null; }
                        try { model.Tluszcze = (int)reader[7]; } catch { model.Tluszcze = null; }
                        try { model.Wegle = (int)reader[8]; } catch { model.Wegle = null; }
                        model.W_bazie_programu = (int)reader[9];

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
        public static List<ProduktModel> GetAppList()
        {
            List<ProduktModel> list = new List<ProduktModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Produkt WHERE w_bazie_programu=1", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ProduktModel model = new ProduktModel();

                        model.Id = (int)reader[0];
                        model.Id_usera = (int)reader[1];
                        model.Nazwa = (string)reader[2];
                        try { model.Ean = (int)reader[3]; } catch { model.Ean = null; }
                        model.Gramy = (int)reader[4];
                        model.Kcla = (int)reader[5];
                        try { model.Bialko = (int)reader[6]; } catch { model.Bialko = null; }
                        try { model.Tluszcze = (int)reader[7]; } catch { model.Tluszcze = null; }
                        try { model.Wegle = (int)reader[8]; } catch { model.Wegle = null; }
                        model.W_bazie_programu = (int)reader[9];

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
