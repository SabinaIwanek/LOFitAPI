using LOFitAPI.Controllers.GetModel;
using LOFitAPI.Controllers.PostModels.Login;
using LOFitAPI.DbModels;
using LOFitAPI.Enums;
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
        public static KontoModel GetOne(int id)
        {
            KontoModel model = new KontoModel();
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
                        model.Email = reader[1].ToString();
                        model.Haslo = reader[2].ToString();
                        model.Typ_konta = (TypKonta)((int)reader[3]);
                        model.Id_uzytkownika = (int)reader[4];
                    }

                    reader.Close();
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return model;
        }
        public static string DeleteKonto(int id)
        {
            try
            {
                KontoModel model = GetOne(id);

                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();
                    //Utworzenie Użytkownika
                    string query = $"DELETE FROM Konto WHERE id={id}; DELETE FROM {model.Typ_konta} WHERE id={model.Id_uzytkownika};";

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
    }
}