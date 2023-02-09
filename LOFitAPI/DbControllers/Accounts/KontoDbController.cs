using LOFitAPI.Controllers.GetModel;
using LOFitAPI.Controllers.PostModels.Login;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers.Accounts
{
    public class KontoDbController
    {
        public static int IsOkLogin(LoginPostModel form)
        {
            if (form.IsCode)
            {
                if (form.Code == null) return 4;
                ChangeForgottenPassword(form);
            }

            return IsOkPassword(form);
        }
        private static int IsOkPassword(LoginPostModel form)
        {
            int type = 4; //error

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Konto WHERE email = '{form.Email}' AND haslo = '{form.Password}'", Connection);
                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[2].ToString() == form.Password)
                    {
                        type = (int)reader[3];
                    }
                }

                reader.Close();
                Connection.Close();
            }

            return type;
        }
        private static bool ChangeForgottenPassword(LoginPostModel form)
        {
            int? userId = null;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                Connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM Konto WHERE email = '{form.Email}' AND kod_jednorazowy = {form.Code}", Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if ((DateTime)reader[6] >= DateTime.Now)
                        userId = (int)reader[0];
                }

                reader.Close();

                if (userId == null) return false;

                SqlCommand command2 = new SqlCommand($"UPDATE Konto SET haslo = '{form.Password}' WHERE id = {userId}", Connection);

                SqlDataReader reader2 = command2.ExecuteReader();

                reader2.Close();

                Connection.Close();
            }

            return userId != null;
        }
        public static bool SendForgottenCode(string email)
        {
            int? userId = ReturnUserId(email);

            if (userId == null) return false;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                Connection.Open();

                int code = CodeGenerator.Generate();

                SqlCommand command2 = new SqlCommand($"UPDATE Konto SET kod_jednorazowy = '{code}', " +
                                                    $"data_czas_kodu = '{DateTime.Now.AddMinutes(10).ToString("yyyy-MM-dd HH:mm:ss")}' " +
                                                    $"WHERE id = {userId}", Connection);

                SqlDataReader reader2 = command2.ExecuteReader();

                reader2.Close();

                Connection.Close();

                SendMail.Send(email, code);
            }

            return userId != null;
        }
        public static int? ReturnUserId(string? email)
        {
            if(email == null) return null;

            int? userId = null;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                Connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM Konto WHERE email = '{email}'", Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    userId = (int)reader[0];
                }

                reader.Close();
                Connection.Close();
            }

            return userId;
        }
    }
}

