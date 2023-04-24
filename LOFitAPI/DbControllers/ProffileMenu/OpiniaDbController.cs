using LOFitAPI.DbModels.ProfileMenu;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers.ProffileMenu
{
    public static class OpiniaDbController
    {
        public static string Add(OpiniaModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"INSERT INTO Opinia VALUES({model.Id_usera},{model.Id_trenera}, {SqlTools.ReturnString(model.Opis)},{model.Ocena}, {model.Zgloszona},{SqlTools.ReturnString(model.Opis_zgloszenia)})";

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
        public static string Update(OpiniaModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"UPDATE Opinia SET id_usera={model.Id_usera},id_trenera={model.Id_trenera}, opis={SqlTools.ReturnString(model.Opis)},ocena={model.Ocena}, zgloszona={model.Zgloszona},opis_zgloszenia={SqlTools.ReturnString(model.Opis_zgloszenia)} WHERE id = {SqlTools.ReturnString(model.Id)}";

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
                    string query = $"DELETE FROM Opinia WHERE id={id};";

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
        public static OpiniaModel GetOne(int id)
        {
            OpiniaModel model = new OpiniaModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    string query = $"Select * from Opinia WHERE id = {id}";

                    SqlCommand command = new SqlCommand(query, Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Id = (int)reader[0];
                        model.Id_usera = (int)reader[1];
                        model.Id_trenera = (int)reader[2];
                        try { model.Opis = reader[3].ToString(); } catch { model.Opis = null; }
                        model.Ocena = (int)reader[4];
                        model.Zgloszona = (int)reader[5];
                        model.Opis_zgloszenia = (string)reader[6];
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
        public static OpiniaModel GetMyOpinion(int id, int coachId)
        {
            OpiniaModel model = new OpiniaModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    string query = $"Select * from Opinia WHERE id_usera = {id} AND id_trenera = {coachId}";

                    SqlCommand command = new SqlCommand(query, Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Id = (int)reader[0];
                        model.Id_usera = (int)reader[1];
                        model.Id_trenera = (int)reader[2];
                        try { model.Opis = reader[3].ToString(); } catch { model.Opis = null; }
                        model.Ocena = (int)reader[4];
                        model.Zgloszona = (int)reader[5];
                        model.Opis_zgloszenia = (string)reader[6];
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
        public static List<OpiniaModel> GetList(int id_trenera)
        {
            List<OpiniaModel> list = new List<OpiniaModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Opinia WHERE id_trenera = {id_trenera}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        OpiniaModel model = new OpiniaModel();

                        model.Id = (int)reader[0];
                        model.Id_usera = (int)reader[1];
                        model.Id_trenera = (int)reader[2];
                        try { model.Opis = reader[3].ToString(); } catch { model.Opis = null; }
                        model.Ocena = (int)reader[4];
                        model.Zgloszona = (int)reader[5];
                        model.Opis_zgloszenia = (string)reader[6];

                        list.Add(model);
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                }
            }

            return list;
        }
        public static List<OpiniaModel> GetAll()
        {
            List<OpiniaModel> list = new List<OpiniaModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Opinia", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        OpiniaModel model = new OpiniaModel();

                        model.Id = (int)reader[0];
                        model.Id_usera = (int)reader[1];
                        model.Id_trenera = (int)reader[2];
                        try { model.Opis = reader[3].ToString(); } catch { model.Opis = null; }
                        model.Ocena = (int)reader[4];
                        model.Zgloszona = (int)reader[5];
                        model.Opis_zgloszenia = (string)reader[6];

                        list.Add(model);
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                }
            }

            return list;
        }

        // Admin
        public static List<OpiniaModel> GetWgType(int statusWeryfikacji)
        {
            List<OpiniaModel> list = new List<OpiniaModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Opinia WHERE zgloszona = {statusWeryfikacji}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        OpiniaModel model = new OpiniaModel();

                        model.Id = (int)reader[0];
                        model.Id_usera = (int)reader[1];
                        model.Id_trenera = (int)reader[2];
                        try { model.Opis = reader[3].ToString(); } catch { model.Opis = null; }
                        model.Ocena = (int)reader[4];
                        model.Zgloszona = (int)reader[5];
                        model.Opis_zgloszenia = (string)reader[6];

                        list.Add(model);
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                }
            }

            return list;
        }
        public static string ResetState(int id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();

                    string query = $"UPDATE Opinia SET zgloszona=0,opis_zgloszenia='' WHERE id = {id}";

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