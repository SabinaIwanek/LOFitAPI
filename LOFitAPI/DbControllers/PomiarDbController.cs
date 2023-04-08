using LOFitAPI.DbModels;
using LOFitAPI.Tools;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace LOFitAPI.DbControllers
{
    public static class PomiarDbController
    {
        public static string Add(PomiarModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"INSERT INTO Pomiar VALUES({SqlTools.ReturnString(model.Id_usera)}, {SqlTools.ReturnDate(model.Data_pomiaru)}, {SqlTools.ReturnDecimal(model.Waga)}, {SqlTools.ReturnDecimal(model.Procent_tluszczu)},{SqlTools.ReturnDecimal(model.Biceps)},{SqlTools.ReturnDecimal(model.Klatka_piersiowa)},{SqlTools.ReturnDecimal(model.Pod_klatka_piersiowa)},{SqlTools.ReturnDecimal(model.Talia)},{SqlTools.ReturnDecimal(model.Pas)},{SqlTools.ReturnDecimal(model.Posladki)},{SqlTools.ReturnDecimal(model.Udo)},{SqlTools.ReturnDecimal(model.Kolano)},{SqlTools.ReturnDecimal(model.Lydka)})";

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
        public static string Update(PomiarModel model)
        {
            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();
                    string query = $"UPDATE Pomiar SET waga ={SqlTools.ReturnDecimal(model.Waga)}, procent_tluszczu ={SqlTools.ReturnDecimal(model.Procent_tluszczu)}, biceps ={SqlTools.ReturnDecimal(model.Biceps)}, klatka_piersiowa={SqlTools.ReturnDecimal(model.Klatka_piersiowa)}, pod_klatka_piersiowa ={SqlTools.ReturnDecimal(model.Pod_klatka_piersiowa)}, talia ={SqlTools.ReturnDecimal(model.Talia)}, pas ={SqlTools.ReturnDecimal(model.Pas)}, posladki={SqlTools.ReturnDecimal(model.Posladki)}, udo={SqlTools.ReturnDecimal(model.Udo)}, kolano={SqlTools.ReturnDecimal(model.Kolano)},lydka={SqlTools.ReturnDecimal(model.Lydka)} WHERE id = {SqlTools.ReturnString(model.Id)}";

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
        public static PomiarModel GetOne(DateTime date, int idUsera)
        {
            PomiarModel model = new PomiarModel();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Pomiar WHERE id_usera = {idUsera} AND data_pomiaru = {SqlTools.ReturnDate(date)}", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Id = (int)reader[0];
                        model.Id_usera = (int)reader[1];
                        model.Data_pomiaru = DateTime.Parse((string)reader[2]);
                        try { model.Waga = (decimal)reader[3]; } catch { model.Waga = null; }
                        try { model.Procent_tluszczu = (decimal)reader[4]; } catch { model.Procent_tluszczu = null; }
                        try { model.Biceps = (decimal)reader[5]; } catch { model.Biceps = null; }
                        try { model.Klatka_piersiowa = (decimal)reader[6]; } catch { model.Klatka_piersiowa = null; }
                        try { model.Pod_klatka_piersiowa = (decimal)reader[7]; } catch { model.Pod_klatka_piersiowa = null; }
                        try { model.Talia = (decimal)reader[8]; } catch { model.Talia = null; }
                        try { model.Pas = (decimal)reader[9]; } catch { model.Pas = null; }
                        try { model.Posladki = (decimal)reader[10]; } catch { model.Posladki = null; }
                        try { model.Udo = (decimal)reader[11]; } catch { model.Udo = null; }
                        try { model.Kolano = (decimal)reader[12]; } catch { model.Kolano = null; }
                        try { model.Lydka = (decimal)reader[13]; } catch { model.Lydka = null; }
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex) 
                { }
            }

            return model;
        }
        public static List<PomiarModel> GetWeek(DateTime date, int idUsera)
        {
            List<PomiarModel> list = new List<PomiarModel>();

            using (SqlConnection Connection = new SqlConnection(Config.DbConnection))
            {
                try
                {
                    Connection.Open();

                    SqlCommand command = new SqlCommand($"Select * from Pomiar WHERE id_usera = {idUsera} AND data_pomiaru BETWEEN {SqlTools.ReturnDate(date)} AND {SqlTools.ReturnDate(date.AddDays(6))} ORDER BY data_pomiaru", Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        PomiarModel model = new PomiarModel();

                        model.Id = (int)reader[0];
                        model.Id_usera = (int)reader[1];
                        model.Data_pomiaru = DateTime.Parse((string)reader[2]);
                        try { model.Waga = (decimal)reader[3]; } catch { model.Waga = null; }
                        try { model.Procent_tluszczu = (decimal)reader[4]; } catch { model.Procent_tluszczu = null; }
                        try { model.Biceps = (decimal)reader[5]; } catch { model.Biceps = null; }
                        try { model.Klatka_piersiowa = (decimal)reader[6]; } catch { model.Klatka_piersiowa = null; }
                        try { model.Pod_klatka_piersiowa = (decimal)reader[7]; } catch { model.Pod_klatka_piersiowa = null; }
                        try { model.Talia = (decimal)reader[8]; } catch { model.Talia = null; }
                        try { model.Pas = (decimal)reader[9]; } catch { model.Pas = null; }
                        try { model.Posladki = (decimal)reader[10]; } catch { model.Posladki = null; }
                        try { model.Udo = (decimal)reader[11]; } catch { model.Udo = null; }
                        try { model.Kolano = (decimal)reader[12]; } catch { model.Kolano = null; }
                        try { model.Lydka = (decimal)reader[13]; } catch { model.Lydka = null; }

                        list.Add(model);
                    }

                    reader.Close();
                    Connection.Close();
                }
                catch (Exception ex)
                { }
            }

            return ResponseTools.ReturnWeekMeasurement(list, date);
        }
    }
}