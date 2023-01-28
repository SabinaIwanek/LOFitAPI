using LOFitAPI.Controllers.PostModels.Registration;
using LOFitAPI.Enums;
using Microsoft.Data.SqlClient;
using System.Text;

namespace LOFitAPI.DbControllers.Accounts
{
    public class TrenerDbController
    {
        public static bool Create(TrenerPostModel form)
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
                    string query2 = "SELECT TOP 1 * FROM Trener ORDER BY data_zalozenia DESC";

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
                                    $"VALUES ('{form.Email}','{form.Haslo}',{2},{id})";

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
        private static string ReturnQuery(TrenerPostModel form)
        {
            StringBuilder insert = new StringBuilder("INSERT INTO Uzytkownik(plec");
            StringBuilder values = new StringBuilder($"VALUES ({form.Plec}");

            insert.Append(",imie");
            values.Append($",'{form.Imie}'");

            if (form.Nazwisko != null)
            {
                insert.Append(",nazwisko");
                values.Append($",'{form.Nazwisko}'");
            }

            if (form.Data_urodzenia != null)
            {
                insert.Append(",data_urodzenia");
                values.Append($",'{form.Data_urodzenia?.ToString("yyyy-MM-dd")}'");
            }

            if (form.Nr_telefonu != null)
            {
                insert.Append(",nr_telefonu");
                values.Append($",{form.Nr_telefonu}");
            }

            if (form.Miejscowosc != null)
            {
                insert.Append(",miejscowosc");
                values.Append($",'{form.Miejscowosc}'");
            }

            if (form.Typ_trenera == TypTrenera.Trener || form.Typ_trenera == TypTrenera.Oba)
            {
                insert.Append(",zatwierdzony_trener");
                values.Append($",{0}");
            }

            if (form.Typ_trenera == TypTrenera.Dietetyk || form.Typ_trenera == TypTrenera.Oba)
            {
                insert.Append(",zatwierdzony_dietetyk");
                values.Append($",{0}");
            }

            insert.Append(",data_zalozenia)");
            values.Append($",'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");

            insert.Append(values);

            return insert.ToString();
        }
    }
}
