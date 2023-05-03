using LOFitAPI.DbModels.Menu;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace LOFitAPI.DbControllers.Menu
{
    public static class ProduktNaLiscieDbController
    {
        private static ProduktNaLiscieModel Struktura(SqlDataReader reader)
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
            model.Zatwierdzony = (int)reader[8] == 0? false : true;
            try { model.Id_planu = (int)reader[9]; } catch { model.Id_planu = null; }

            return model;
        }
        private static ProduktModel StrukturaProd(SqlDataReader reader)
        {
            ProduktModel model = new ProduktModel();

            model.Id = (int)reader[0];
            try { model.Id_konta = (int)reader[1]; } catch { model.Id_konta = null; }
            model.Nazwa = (string)reader[2];
            try { model.Ean = (int)reader[3]; } catch { model.Ean = null; }
            model.Gramy = (int)reader[4];
            model.Kcla = (int)reader[5];
            try { model.Bialko = (int)reader[6]; } catch { model.Bialko = null; }
            try { model.Tluszcze = (int)reader[7]; } catch { model.Tluszcze = null; }
            try { model.Wegle = (int)reader[8]; } catch { model.Wegle = null; }
            model.W_bazie_programu = (int)reader[9];

            return model;
        }
        public static int Add(ProduktNaLiscieModel model)
        {
            int id = 0;

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"INSERT INTO ProduktNaLiscie VALUES({model.Id_produktu},{model.Id_usera},{SqlTools.ReturnString(model.Nazwa_dania)}, {model.Gramy},{SqlTools.ReturnDateTime(model.Data_czas)},{SqlTools.ReturnString(model.Opis_od_trenera)},{SqlTools.ReturnInt(model.Id_trenera)},{SqlTools.ReturnBool(model.Zatwierdzony)}, {SqlTools.ReturnInt(model.Id_planu)}); SELECT SCOPE_IDENTITY();";

                    SqlCommand command = new SqlCommand(query, Connection);
                    //SqlDataReader reader = command.ExecuteReader();
                    id = Convert.ToInt32(command.ExecuteScalar());

                    //reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }

            return id;
        }
        public static string Update(ProduktNaLiscieModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"UPDATE ProduktNaLiscie SET id_produktu={model.Id_produktu},id_usera={model.Id_usera},nazwa_dania={SqlTools.ReturnString(model.Nazwa_dania)},gramy={model.Gramy}, data_czas={SqlTools.ReturnDateTime(model.Data_czas)},opis_od_trenera={SqlTools.ReturnString(model.Opis_od_trenera)},id_trenera={SqlTools.ReturnInt(model.Id_trenera)},zatwierdzony={SqlTools.ReturnBool(model.Zatwierdzony)}, id_planu = {SqlTools.ReturnInt(model.Id_planu)} WHERE id = {SqlTools.ReturnString(model.Id)}";

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
        public static string CheckedBoxChange(int id, int check)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
                {
                    Connection.Open();
                    string query = $"UPDATE ProduktNaLiscie SET zatwierdzony = {check} WHERE id={id};";

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
                        model = Struktura(reader);
                    }
                    reader.Close();


                    ProduktModel produkt = new ProduktModel();
                    command = new SqlCommand($"Select * from Produkt WHERE id = {model.Id_produktu}", Connection);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        produkt = StrukturaProd(reader);
                    }

                    model.Produkt = produkt;

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
        public static List<ProduktNaLiscieModel> GetOnePlan(int id)
        {
            List<ProduktNaLiscieModel> list = new List<ProduktNaLiscieModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from ProduktNaLiscie WHERE id_planu = {id}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(Struktura(reader));
                    }

                    reader.Close();

                    foreach (var item in list)
                    {
                        ProduktModel produkt = new ProduktModel();
                        command = new SqlCommand($"Select * from Produkt WHERE id = {item.Id_produktu}", Connection);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            produkt = StrukturaProd(reader);
                        }

                        item.Produkt = produkt;

                        reader.Close();
                    }

                    Connection.Close();
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                }
            }

            return list;
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
                        list.Add(Struktura(reader));
                    }

                    reader.Close();

                    foreach (var item in list)
                    {
                        ProduktModel produkt = new ProduktModel();
                        command = new SqlCommand($"Select * from Produkt WHERE id = {item.Id_produktu}", Connection);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            produkt = StrukturaProd(reader);
                        }

                        item.Produkt = produkt;

                        reader.Close();
                    }

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
