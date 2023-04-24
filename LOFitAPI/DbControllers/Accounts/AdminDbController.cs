using LOFitAPI.Controllers.PostModels.Registration;
using LOFitAPI.DbModels.Accounts;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers.Accounts
{
    public static class AdminDbController
    {
        public static bool Add(AdminPostModel form)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    //Utworzenie Użytkownika
                    string query = $"INSERT INTO Administrator(imie,nazwisko,data_zalozenia) VALUES ('{form.Imie}','{form.Nazwisko}',{SqlTools.ReturnDateTime(DateTime.Now)})";

                    SqlCommand command = new SqlCommand(query, Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Close();

                    //Odczyt id utworzonego użytkownika
                    string query2 = "SELECT TOP 1 * FROM Administrator ORDER BY data_zalozenia DESC";

                    SqlCommand command2 = new SqlCommand(query2, Connection);
                    SqlDataReader reader2 = command2.ExecuteReader();

                    int id = -1;

                    while (reader2.Read())
                    {
                        id = (int)reader2[0];
                    }

                    reader2.Close();

                    //Utworzenie konta
                    string query3 = $"INSERT INTO Konto VALUES ('{form.Email}','{form.Haslo}',{0},{0},{id},NULL,NULL)";

                    SqlCommand command3 = new SqlCommand(query3, Connection);
                    SqlDataReader reader3 = command3.ExecuteReader();

                    reader3.Close();

                    Connection.Close();
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();

                    return false;
                }
            }

            return true;
        }
        public static AdministratorModel GetOne(int id)
        {
            AdministratorModel model = new AdministratorModel();
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"SELECT * FROM Administrator WHERE id ={id}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        model.Id = (int)reader[0];
                        model.Imie = (string)reader[1];
                        model.Nazwisko = (string)reader[2];
                    }

                    reader.Close();
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();

                return new AdministratorModel();
            }

            return model;
        }
        public static List<AdministratorModel> GetAll()
        {
            List<AdministratorModel> list = new List<AdministratorModel>();

            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"SELECT * FROM Administrator", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        AdministratorModel model = new AdministratorModel();

                        model.Id = (int)reader[0];
                        model.Imie = (string)reader[1];
                        model.Nazwisko = (string)reader[2];

                        list.Add(model);
                    }

                    reader.Close();
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();

                return new List<AdministratorModel>();
            }

            return list;
        }
    }
}