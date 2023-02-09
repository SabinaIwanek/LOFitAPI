namespace LOFitAPI.Tools
{
    public static class SqlTools
    {
        public static string ReturnString(object? value) => value == null ? "NULL" : value.ToString();

        public static string ReturnDate(DateTime? date) => (date == null) ? "NULL" : date?.ToString("yyyy-MM-dd");
        public static string ReturnTime(DateTime? date) => (date == null) ? "NULL" : date?.ToString("HH:mm:ss");
        public static string ReturnDateTime(DateTime? date) => (date == null) ? "NULL" : date?.ToString("yyyy-MM-dd HH:mm:ss");

        public static string ReturnDecimal(object? value) => value == null ? "NULL" : value.ToString().Replace(',', '.');

    }
}
