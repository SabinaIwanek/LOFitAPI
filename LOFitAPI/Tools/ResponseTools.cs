using LOFitAPI.DbModels;

namespace LOFitAPI.Tools
{
    public static class ResponseTools
    {
        public static List<PomiarModel> ReturnWeekMeasurement(List<PomiarModel> listFromSql, DateTime date)
        {
            List<PomiarModel> list = new List<PomiarModel>();

            for (int i = 0; i < 7; i++)
            {
                PomiarModel? model = listFromSql.FirstOrDefault(x => x.Data_pomiaru == date.AddDays(i));
                
                if(model == null)
                {
                    list.Add(new PomiarModel()
                    {
                        Data_pomiaru = date.AddDays(i)
                    });
                }
                else
                {
                    list.Add(model);
                }
            }

            return list;
        }
    }
}
