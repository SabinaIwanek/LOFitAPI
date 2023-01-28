namespace LOFitAPI.Tools
{
    public static class CodeGenerator
    {
        public static int Generate()
        {
            int code = 0;

            while (code < 100000 || code > 999999)
            {
                Random r = new Random();
                code = r.Next(100000, 999999);
            }

            return code;
        }
    }
}
