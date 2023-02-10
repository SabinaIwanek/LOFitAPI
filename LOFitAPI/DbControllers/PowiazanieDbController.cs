using LOFitAPI.DbModels;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers
{
    public class PowiazanieDbController
    {
        public static string Add(PowiazanieModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"INSERT INTO Powiazanie VALUES({model.Id_trenera}, '{model.Id_usera}',{SqlTools.ReturnDateTime(model.Czas_od)},{SqlTools.ReturnDateTime(model.Czas_do)},{model.Zatwierdzone},{model.Podglad_pelny},{SqlTools.ReturnDateTime(model.Podglad_od_daty)})";

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
        public static string Update(PowiazanieModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"UPDATE Powiazanie SET id_trenera={model.Id_trenera}, id_usera='{model.Id_usera}',czas_od={SqlTools.ReturnDateTime(model.Czas_od)},czas_do={SqlTools.ReturnDateTime(model.Czas_do)},zatwierdzone={model.Zatwierdzone},podglad_pelny={model.Podglad_pelny}, podglad_od_daty={SqlTools.ReturnDateTime(model.Podglad_od_daty)} WHERE id = {SqlTools.ReturnString(model.Id)}";

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
                    string query = $"DELETE FROM Powiazanie WHERE id={id};";

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
        public static PowiazanieModel GetListUser(int id_usera)
        {
            PowiazanieModel model = new PowiazanieModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Powiazania WHERE id_usera = {id_usera}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Id = (int)reader[0];
                        model.Id_trenera = (int)reader[1];
                        model.Id_usera = (int)reader[2];
                        model.Czas_od = DateTime.Parse(reader[3].ToString());
                        model.Czas_do = DateTime.Parse(reader[4].ToString());
                        model.Zatwierdzone = (int)reader[5];
                        model.Podglad_pelny = (int)reader[6];
                        model.Podglad_od_daty = DateTime.Parse(reader[7].ToString());
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                { }
            }

            return model;
        }
        public static PowiazanieModel GetListTrener(int id_trenera)
        {
            PowiazanieModel model = new PowiazanieModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Powiazania WHERE id_trenera = {id_trenera}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Id = (int)reader[0];
                        model.Id_trenera = (int)reader[1];
                        model.Id_usera = (int)reader[2];
                        model.Czas_od = DateTime.Parse(reader[3].ToString());
                        model.Czas_do = DateTime.Parse(reader[4].ToString());
                        model.Zatwierdzone = (int)reader[5];
                        model.Podglad_pelny = (int)reader[6];
                        model.Podglad_od_daty = DateTime.Parse(reader[7].ToString());
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                { }
            }

            return model;
        }
    }
}
