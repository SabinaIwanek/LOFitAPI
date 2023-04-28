using LOFitAPI.Controllers.GetModel;
using LOFitAPI.Controllers.PostModels.Login;
using LOFitAPI.DbModels.Accounts;
using LOFitAPI.Enums;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers.Accounts
{
    public static class KontoDbController
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
                        if ((int)reader[4] == 1) type = 5;
                    }
                }

                reader.Close();
                Connection.Close();
            }

            return type;
        }
        public static bool ChangePassword(ChangePasswordPostModel form, int idKonta)
        {
            bool isOk = false;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                Connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM Konto WHERE id = {idKonta} AND haslo = '{form.OldPassword}'", Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    isOk= true;
                }

                reader.Close();

                SqlCommand command2 = new SqlCommand($"UPDATE Konto SET haslo = '{form.NewPassword}' WHERE id = {idKonta}", Connection);

                SqlDataReader reader2 = command2.ExecuteReader();

                reader2.Close();

                Connection.Close();
            }

            return isOk;
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
                    if ((DateTime)reader[7] >= DateTime.Now)
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
            int? userId = ReturnKontoId(email);

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
            if (email == null) return null;

            int? userId = null;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                Connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM Konto WHERE email = '{email}'", Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    userId = (int)reader[5];
                }

                reader.Close();
                Connection.Close();
            }

            return userId;
        }
        public static int? ReturnKontoId(string? email)
        {
            if (email == null) return null;

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
        public static int? ReturnKontoId(int id, int type)
        {
            int? kontoId = null;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                Connection.Open();

                SqlCommand command = new SqlCommand($"SELECT id FROM Konto WHERE id_uzytkownika = {id} AND typ_konta = {type}", Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    kontoId = (int)reader[0];
                }

                reader.Close();
                Connection.Close();
            }

            return kontoId;
        }
        public static int? ReturnUserType(string? email)
        {
            if (email == null) return null;

            int? accountType = null;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                Connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM Konto WHERE email = '{email}'", Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    accountType = (int)reader[3];
                }

                reader.Close();
                Connection.Close();
            }

            return accountType;
        }
        public static KontoModel GetOne(int id)
        {
            KontoModel model = new KontoModel();
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"SELECT * FROM Konto WHERE id ={id}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Id = (int)reader[0];
                        model.Email = (string)reader[1];
                        model.Haslo = (string)reader[2];
                        model.Typ_konta = (TypKonta)((int)reader[3]);
                        model.Zablokowane = (int)reader[4];
                        model.Id_uzytkownika = (int)reader[5];
                    }

                    reader.Close();
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();

                return new KontoModel();
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
        public static string DeleteKonto(int id, int type)
        {
            try
            {
                int? kontoId = ReturnKontoId(id, type);
                if (kontoId == null) return "";

                KontoModel model = GetOne((int)kontoId);

                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();
                    //Utworzenie Użytkownika
                    string query = $"DELETE FROM Konto WHERE id={kontoId};";

                    SqlCommand command = new SqlCommand(query, Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Close();

                    string query2 = $"DELETE FROM {model.Typ_konta} WHERE id={model.Id_uzytkownika};";

                    SqlCommand command2 = new SqlCommand(query2, Connection);
                    SqlDataReader reader2 = command2.ExecuteReader();

                    reader2.Close();

                    Connection.Close();
                }

                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public static string BlockKonto(int id, int state)
        {
            try
            {
                KontoModel model = GetOne(id);

                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();
                    //Utworzenie Użytkownika
                    string query = $"UPDATE Konto SET zablokowane = {state} where id = {id};";

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