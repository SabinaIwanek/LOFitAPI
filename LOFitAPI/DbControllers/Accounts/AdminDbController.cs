using LOFitAPI.Controllers.PostModels.Registration;
using Microsoft.Data.SqlClient;
using System.Text;

namespace LOFitAPI.DbControllers.Accounts
{
    public class AdminDbController
    {
        public static bool Create(AdminPostModel form)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    //Utworzenie Użytkownika
                    string query = ReturnQuery(form);

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
                    string query3 = $"INSERT INTO Konto (email,haslo,typ_konta,id_uzytkownika)" +
                                    $"VALUES ('{form.Email}','{form.Haslo}',{0},{id})";

                    SqlCommand command3 = new SqlCommand(query3, Connection);
                    SqlDataReader reader3 = command3.ExecuteReader();

                    reader3.Close();

                    Connection.Close();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }
        private static string ReturnQuery(AdminPostModel form)
        {
            StringBuilder insert = new StringBuilder("INSERT INTO Admin(imie,nazwisko,data_zalozenia)");
            StringBuilder values = new StringBuilder($"VALUES ('{form.Imie}','{form.Nazwisko}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");

            insert.Append(values);

            return insert.ToString();
        }
    }
}
