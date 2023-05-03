using LOFitAPI.DbModels.MenuCoach;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers.MenuCoach
{
    public static class PlanTygodniowyDbController
    {
        private static PlanTygodniowyModel Struktura(SqlDataReader reader)
        {
            PlanTygodniowyModel model = new PlanTygodniowyModel();

            model.Id = (int)reader[0];
            model.Id_trenera = (int)reader[1];
            model.Typ = (int)reader[2];
            model.Nazwa = (string)reader[3];
            model.Dzien1 = (int)reader[4];
            model.Dzien2 = (int)reader[5];
            model.Dzien3 = (int)reader[6];
            model.Dzien4 = (int)reader[7];
            model.Dzien5 = (int)reader[8];
            model.Dzien6 = (int)reader[9];
            model.Dzien7 = (int)reader[10];

            return model;
        }
        public static string Add(PlanTygodniowyModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"INSERT INTO PlanTygodniowy VALUES({model.Id_trenera}, {model.Typ}, {SqlTools.ReturnString(model.Nazwa)}, {model.Dzien1}, {model.Dzien2}, {model.Dzien3}, {model.Dzien4}, {model.Dzien5}, {model.Dzien6}, {model.Dzien7})";

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
        public static string Update(PlanTygodniowyModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"UPDATE Pomiar SET nazwa ={SqlTools.ReturnString(model.Nazwa)} WHERE id = {model.Id}";

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
                    string query = $"Delete PlanTygodniowy WHERE id={id};";

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
        public static PlanTygodniowyModel GetOne(int id)
        {
            PlanTygodniowyModel model = new PlanTygodniowyModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from PlanTygodniowy WHERE id = {id}", Connection);
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
        public static List<PlanTygodniowyModel> GetByType(int idTrenera, int typ)
        {
            List<PlanTygodniowyModel> list = new List<PlanTygodniowyModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from PlanTygodniowy WHERE id_trenera = {idTrenera} AND typ = {typ}", Connection);
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