namespace LOFitAPI.Tools
{
    public static class SqlTools
    {
        public static string ReturnString(object? value) => value == null ? "NULL" : $"'{value}'";

        public static string ReturnDate(DateTime? date) => (date == null) ? "NULL" : $"'{date?.ToString("yyyy-MM-dd")}'";
        public static string ReturnTime(DateTime? date) => (date == null) ? "NULL" : $"'{date?.ToString("HH:mm:ss")}'";
        public static string ReturnDateTime(DateTime? date) => (date == null) ? "NULL" : $"'{date?.ToString("yyyy-MM-dd HH:mm:ss")}'";

        public static string ReturnDecimal(object? value) => value == null ? "NULL" : $"{((string)value).Replace(',', '.')}";
        public static string ReturnInt(object? value) => value == null ? "NULL" : $"{value}";
        public static string ReturnBool(bool? value) => value == null ? "NULL" : value == true? "1" : "0";

    }
}