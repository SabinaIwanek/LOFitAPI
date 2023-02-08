using LOFitAPI.Controllers.PostModels.Registration;
using LOFitAPI.Enums;
using LOFitAPI.Models;
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
            StringBuilder insert = new StringBuilder("INSERT INTO Trener(plec");
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

            if (form.Typ_trenera == 0)
            {
                insert.Append(",zatwierdzony_trener");
                values.Append($",{0}");
                insert.Append(",zatwierdzony_dietetyk");
                values.Append($",{3}");
            }

            if (form.Typ_trenera == 1)
            {
                insert.Append(",zatwierdzony_trener");
                values.Append($",{3}");
                insert.Append(",zatwierdzony_dietetyk");
                values.Append($",{0}");
            }

            if (form.Typ_trenera == 2)
            {
                insert.Append(",zatwierdzony_trener");
                values.Append($",{0}");
                insert.Append(",zatwierdzony_dietetyk");
                values.Append($",{0}");
            }

            insert.Append(",data_zalozenia)");
            values.Append($",'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");

            insert.Append(values);

            return insert.ToString();
        }

        public static List<TrenerModel> GetAll()
        {
            List<TrenerModel> list = new List<TrenerModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                Connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM Trener", Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TrenerModel model = new TrenerModel();

                    model.Id = (int)reader[0];
                    model.Imie = reader[1].ToString();
                    model.Nazwisko = reader[2].ToString();
                    model.Plec = (int)reader[3];
                    try { model.Data_urodzenia = (DateOnly)reader[4]; } catch { model.Data_urodzenia = null; }
                    try { model.Nr_telefonu = (int)reader[5]; } catch { model.Nr_telefonu = null; }
                    model.Opis_profilu = reader[6].ToString();
                    model.Miejscowosc = reader[7].ToString();
                    try { model.Cena_treningu = (decimal)reader[8]; } catch { model.Cena_treningu = null; }
                    try { model.Czas_treningu = (TimeOnly)reader[9]; } catch { model.Czas_treningu = null; }
                    try { model.Cena_dieta = (decimal)reader[10]; } catch { model.Cena_dieta = null; }
                    try { model.Czas_dieta = (TimeOnly)reader[11]; } catch { model.Czas_dieta = null; }
                    model.Zatwierdzony_dietetyk = (int)reader[12];
                    model.Zatwierdzony_trener = (int)reader[13];

                    list.Add(model);
                }

                reader.Close();
                Connection.Close();
            }

            return list;
        }
    }
}
