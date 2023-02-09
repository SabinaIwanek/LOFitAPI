namespace LOFitAPI.Tools
{
    public static class SqlTools
    {
        public static string ReturnString(object? value) => value == null ? "NULL" : value.ToString();

        public static string ReturnDate(DateTime? date) => (date == null) ? "NULL" : $"{date?.Year}-{date?.Month}-{date?.Day}";

        public static string ReturnDecimal(object? value) => value == null ? "NULL" : value.ToString().Replace(',', '.');

    }
}
