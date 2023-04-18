using LOFitAPI.DbModels.Menu;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;

namespace LOFitAPI.DbControllers.Menu
{
    public static class ProduktNaLiscieDbController
    {
        public static string Add(ProduktNaLiscieModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"INSERT INTO ProduktNaLiscie VALUES({model.Id_produktu},{model.Id_usera},{SqlTools.ReturnString(model.Nazwa_dania)}, {model.Gramy},{SqlTools.ReturnDateTime(model.Data_czas)},{SqlTools.ReturnString(model.Opis_od_trenera)},{SqlTools.ReturnInt(model.Id_trenera)},{SqlTools.ReturnBool(model.Zatwierdzony)})";

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
        public static string Update(ProduktNaLiscieModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"UPDATE ProduktNaLiscie SET id_produktu={model.Id_produktu},id_usera={model.Id_usera},nazwa_dania={SqlTools.ReturnString(model.Nazwa_dania)},gramy={model.Gramy}, data_czas={SqlTools.ReturnDateTime(model.Data_czas)},opis_od_trenera={SqlTools.ReturnString(model.Opis_od_trenera)},id_trenera={SqlTools.ReturnInt(model.Id_trenera)},data_czas={SqlTools.ReturnDateTime(model.Data_czas)},zatwierdzony={SqlTools.ReturnBool(model.Zatwierdzony)} WHERE id = {SqlTools.ReturnString(model.Id)}";

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
                    string query = $"DELETE FROM ProduktNaLiscie WHERE id={id};";

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
        public static ProduktNaLiscieModel GetOne(int id)
        {
            ProduktNaLiscieModel model = new ProduktNaLiscieModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from ProduktNaLiscie WHERE id = {id}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Id = (int)reader[0];
                        model.Id_produktu = (int)reader[1];
                        model.Id_usera = (int)reader[2];
                        model.Nazwa_dania = (string)reader[3];
                        model.Gramy = (int)reader[4];
                        model.Data_czas = (DateTime)reader[5];
                        try { model.Opis_od_trenera = reader[6].ToString(); } catch { model.Opis_od_trenera = null; }
                        try { model.Id_trenera = (int)reader[7]; } catch { model.Id_trenera = null; }
                        model.Zatwierdzony = (bool)reader[8];
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                { }
            }

            return model;
        }
        public static List<ProduktNaLiscieModel> GetUserList(int Id_usera, DateTime date)
        {
            List<ProduktNaLiscieModel> list = new List<ProduktNaLiscieModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from ProduktNaLiscie WHERE Id_usera = {Id_usera} AND CAST(data_czas AS DATE) = {SqlTools.ReturnDate(date)}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ProduktNaLiscieModel model = new ProduktNaLiscieModel();

                        model.Id = (int)reader[0];
                        model.Id_produktu = (int)reader[1];
                        model.Id_usera = (int)reader[2];
                        model.Nazwa_dania = (string)reader[3];
                        model.Gramy = (int)reader[4];
                        model.Data_czas = (DateTime)reader[5];
                        try { model.Opis_od_trenera = reader[6].ToString(); } catch { model.Opis_od_trenera = null; }
                        try { model.Id_trenera = (int)reader[7]; } catch { model.Id_trenera = null; }
                        model.Zatwierdzony = (int)reader[8] > 0 ? true : false;

                        list.Add(model);
                    }

                    reader.Close();

                    foreach (var item in list)
                    {
                        ProduktModel produkt = new ProduktModel();
                        command = new SqlCommand($"Select * from Produkt WHERE id = {item.Id_produktu}", Connection);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            produkt.Id = (int)reader[0];
                            try { produkt.Id_konta = (int)reader[1]; } catch { produkt.Id_konta = null; }
                            produkt.Nazwa = (string)reader[2];
                            try { produkt.Ean = (int)reader[3]; } catch { produkt.Ean = null; }
                            produkt.Gramy = (int)reader[4];
                            produkt.Kcla = (int)reader[5];
                            try { produkt.Bialko = (int)reader[6]; } catch { produkt.Bialko = null; }
                            try { produkt.Tluszcze = (int)reader[7]; } catch { produkt.Tluszcze = null; }
                            try { produkt.Wegle = (int)reader[8]; } catch { produkt.Wegle = null; }
                            produkt.W_bazie_programu = (int)reader[9];
                        }

                        item.Produkt = produkt;

                        reader.Close();
                    }

                    Connection.Close();
                }
                catch (Exception ex)
                { }
            }

            return list;
        }
    }
}
