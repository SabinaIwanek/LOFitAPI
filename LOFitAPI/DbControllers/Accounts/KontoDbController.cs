using LOFitAPI.Controllers.PostModels.Login;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers.Accounts
{
    public class KontoDbController
    {
        public static bool IsOkLogin(LoginPostModel form)
        {
            if (form == null || form.Email == null || form.Email == string.Empty) return false;

            if (form.IsCode)
            {
                if (form.Code == null || form.Code == string.Empty) return false;
                ChangeForgottenPassword(form);
            }

            if (form.Password == null || form.Password == string.Empty) return false;

            return IsOkPassword(form);
        }

        private static bool IsOkPassword(LoginPostModel form)
        {
            bool isOk = false;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Konto WHERE email = '{form.Email}' AND haslo = '{form.Password}'", Connection);
                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[2].ToString() == form.Password)
                        isOk = true;
                }

                reader.Close();
                Connection.Close();
            }

            return isOk;
        }

        private static bool ChangeForgottenPassword(LoginPostModel form)
        {
            bool isOk = false;
            int? userId;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Konto WHERE email = '{form.Email}' AND kod_jednorazowy = '{form.Code}'", Connection);
                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    userId = (int)reader[0];
                    if ((DateTime)reader[6] >= DateTime.Now)
                        isOk = true;
                }

                reader.Close();


                Connection.Close();
            }

            return isOk;
        }
    }
}
