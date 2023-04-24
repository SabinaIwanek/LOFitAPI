using LOFitAPI.DbModels.ProfileMenu;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers.ProffileMenu
{
    public static class CertyfikatDbController
    {
        public static string Add(CertyfikatModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"INSERT INTO Certyfikat VALUES({model.Id_trenera}, '{model.Nazwa}','{model.Organizacja}',{SqlTools.ReturnDate(model.Data_certyfikatu)},'{model.Kod_certyfikatu}',{model.Zatwierdzony})";

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
        public static string Update(CertyfikatModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"UPDATE Certyfikat SET id_trenera={model.Id_trenera}, nazwa='{model.Nazwa}',organizacja='{model.Organizacja}',data_certyfikatu={SqlTools.ReturnDate(model.Data_certyfikatu)},kod_certyfikatu='{model.Kod_certyfikatu}',zatwierdzony={model.Zatwierdzony} WHERE id = {SqlTools.ReturnString(model.Id)}";

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
        public static string SetState(int id, int state)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();

                    string query = $"UPDATE Certyfikat SET zatwierdzony={state} WHERE id = {id}";

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
        public static string Delete(int id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();
                    //Utworzenie Użytkownika
                    string query = $"DELETE FROM Certyfikat WHERE id={id};";

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
        public static CertyfikatModel GetOne(int id)
        {
            CertyfikatModel model = new CertyfikatModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Certyfikat WHERE id = {id}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Id = (int)reader[0];
                        model.Id_trenera = (int)reader[1];
                        model.Nazwa = (string)reader[2];
                        model.Organizacja = (string)reader[3];
                        model.Data_certyfikatu = (DateTime)reader[4];
                        model.Kod_certyfikatu = reader[5].ToString();
                        model.Zatwierdzony = (int)reader[6];
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
        public static List<CertyfikatModel> GetList(int id_trenera)
        {
            List<CertyfikatModel> list = new List<CertyfikatModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Certyfikat WHERE id_trenera = {id_trenera}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CertyfikatModel model = new CertyfikatModel();

                        model.Id = (int)reader[0];
                        model.Id_trenera = (int)reader[1];
                        model.Nazwa = (string)reader[2];
                        model.Organizacja = (string)reader[3];
                        model.Data_certyfikatu = (DateTime)reader[4];
                        model.Kod_certyfikatu = reader[5].ToString();
                        model.Zatwierdzony = (int)reader[6];

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
        public static List<CertyfikatModel> GetAll()
        {
            List<CertyfikatModel> list = new List<CertyfikatModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Certyfikat", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CertyfikatModel model = new CertyfikatModel();

                        model.Id = (int)reader[0];
                        model.Id_trenera = (int)reader[1];
                        model.Nazwa = (string)reader[2];
                        model.Organizacja = (string)reader[3];
                        model.Data_certyfikatu = (DateTime)reader[4];
                        model.Kod_certyfikatu = reader[5].ToString();
                        model.Zatwierdzony = (int)reader[6];

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
        public static List<CertyfikatModel> GetWgType(int statusWeryfikaci)
        {
            List<CertyfikatModel> list = new List<CertyfikatModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Certyfikat WHERE zatwierdzony = {statusWeryfikaci}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CertyfikatModel model = new CertyfikatModel();

                        model.Id = (int)reader[0];
                        model.Id_trenera = (int)reader[1];
                        model.Nazwa = (string)reader[2];
                        model.Organizacja = (string)reader[3];
                        model.Data_certyfikatu = (DateTime)reader[4];
                        model.Kod_certyfikatu = reader[5].ToString();
                        model.Zatwierdzony = (int)reader[6];

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
    }
}