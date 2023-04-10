using LOFitAPI.DbModels;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace LOFitAPI.DbControllers.ProffileMenu
{
    public static class PowiazanieDbController
    {
        public static string Add(PowiazanieModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"INSERT INTO Powiazanie VALUES({model.Id_trenera}, '{model.Id_usera}',{SqlTools.ReturnDateTime(model.Czas_od)},{SqlTools.ReturnDateTime(model.Czas_do)},{model.Zatwierdzone},{SqlTools.ReturnDateTime(model.Podglad_od_daty)})";

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
                    string query = $"UPDATE Powiazanie SET id_trenera={model.Id_trenera}, id_usera='{model.Id_usera}',czas_od={SqlTools.ReturnDateTime(model.Czas_od)},czas_do={SqlTools.ReturnDateTime(model.Czas_do)},zatwierdzone={model.Zatwierdzone}, podglad_od_daty={SqlTools.ReturnDateTime(model.Podglad_od_daty)} WHERE id = {SqlTools.ReturnString(model.Id)}";

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
        public static List<PowiazanieModel> GetListUser(int id_usera)
        {
            List<PowiazanieModel> list = new List<PowiazanieModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Powiazanie WHERE id_usera = {id_usera}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        PowiazanieModel model = new PowiazanieModel();

                        model.Id = (int)reader[0];
                        model.Id_trenera = (int)reader[1];
                        model.Id_usera = (int)reader[2];
                        model.Czas_od = DateTime.Parse((string)reader[3]);
                        model.Czas_do = DateTime.Parse((string)reader[4]);
                        model.Zatwierdzone = (int)reader[5];
                        model.Podglad_od_daty = DateTime.Parse((string)reader[6]);

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
        public static List<PowiazanieModel> GetListTrener(int id_trenera)
        {
            List<PowiazanieModel> list = new List<PowiazanieModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Powiazanie WHERE id_trenera = {id_trenera}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        PowiazanieModel model = new PowiazanieModel();

                        model.Id = (int)reader[0];
                        model.Id_trenera = (int)reader[1];
                        model.Id_usera = (int)reader[2];
                        model.Czas_od = DateTime.Parse((string)reader[3]);
                        model.Czas_do = DateTime.Parse((string)reader[4]);
                        model.Zatwierdzone = (int)reader[5];
                        model.Podglad_od_daty = DateTime.Parse((string)reader[6]);

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