using LOFitAPI.DbModels;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers
{
    public class ZgloszenieDbController
    {
        public static string Add(ZgloszenieModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"INSERT INTO Zgloszenie VALUES({model.Id_trenera},{model.Id_usera},'{model.Opis}')";

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
        public static string Update(ZgloszenieModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"UPDATE Zgloszenie SET id_trenera={model.Id_trenera}, id_usera={model.Id_usera},opis='{model.Opis}' WHERE id = {model.Id}";

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
                    string query = $"DELETE FROM Zgloszenie WHERE id={id};";

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
        public static ZgloszenieModel GetOne(int id)
        {
            ZgloszenieModel model = new ZgloszenieModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Zgloszenie WHERE id = {id}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Id = (int)reader[0];
                        model.Id_trenera = (int)reader[1];
                        model.Nazwa = reader[2].ToString();
                        model.Organizacja = reader[3].ToString();
                        model.Data_certyfikatu = DateTime.Parse(reader[4].ToString());
                        model.Kod_certyfikatu = reader[5].ToString();
                        model.Zatwierdzony = (int)reader[6];
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                { }
            }

            return model;
        }
        public static List<ZgloszenieModel> GetList(int id_trenera)
        {
            List<ZgloszenieModel> list = new List<ZgloszenieModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Zgloszenie WHERE id_trenera = {id_trenera}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ZgloszenieModel model = new ZgloszenieModel();

                        model.Id = (int)reader[0];
                        model.Id_trenera = (int)reader[1];
                        model.Id_usera = (int)reader[2];
                        model.Opis = reader[3].ToString();

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
        public static List<ZgloszenieModel> GetAll()
        {
            List<ZgloszenieModel> list = new List<ZgloszenieModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Zgloszenie", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ZgloszenieModel model = new ZgloszenieModel();

                        model.Id = (int)reader[0];
                        model.Id_trenera = (int)reader[1];
                        model.Id_usera = (int)reader[2];
                        model.Opis = reader[3].ToString();

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
