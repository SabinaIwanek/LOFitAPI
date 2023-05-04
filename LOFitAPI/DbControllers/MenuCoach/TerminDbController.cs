using LOFitAPI.DbModels.Menu;
using LOFitAPI.DbModels.MenuCoach;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;
using System;

namespace LOFitAPI.DbControllers.MenuCoach
{
    public static class TerminDbController
    {
        private static TerminModel Struktura(SqlDataReader reader)
        {
            TerminModel model = new TerminModel();

            model.Id = (int)reader[0];
            model.Id_trenera = (int)reader[1];
            model.Id_usera = (int)reader[2];
            model.Termin_od = (DateTime)reader[3];
            model.Termin_do = (DateTime)reader[4];
            model.Zatwierdzony = (int)reader[5] == 0 ? false : true;

            return model;
        }
        public static int Add(TerminModel model)
        {
            int id = 0;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"INSERT INTO Termin VALUES({model.Id_trenera}, {model.Id_usera}, {SqlTools.ReturnDateTime(model.Termin_od)}, {SqlTools.ReturnDateTime(model.Termin_do)}, 0); SELECT SCOPE_IDENTITY();";

                    SqlCommand command = new SqlCommand(query, Connection);
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
        public static string Update(TerminModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"UPDATE Termin SET  termin_od={SqlTools.ReturnDateTime(model.Termin_od)},termin_do={SqlTools.ReturnDateTime(model.Termin_do)},zatwierdzony={SqlTools.ReturnBool(model.Zatwierdzony)} WHERE id = {SqlTools.ReturnString(model.Id)}";

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
                    string query = $"Delete Termin WHERE id={id};";

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
        public static TerminModel GetOne(int id)
        {
            TerminModel model = new TerminModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Termin WHERE id = {id}", Connection);
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
        public static List<TerminModel> GetByDay(int idTrenera, DateTime date)
        {
            List<TerminModel> list = new List<TerminModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Termin WHERE id_trenera = {idTrenera} AND CAST(termin_od AS DATE) = {SqlTools.ReturnDate(date)}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(Struktura(reader));
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
        public static List<TerminModel> GetAll(int idTrenera)
        {
            List<TerminModel> list = new List<TerminModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Termin WHERE id_trenera = {idTrenera}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(Struktura(reader));
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
        public static List<TerminModel> GetAllUser(int idUsera)
        {
            List<TerminModel> list = new List<TerminModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Termin WHERE id_usera = {idUsera}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(Struktura(reader));
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
    }
}