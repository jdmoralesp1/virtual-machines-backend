namespace PruebaTecnica.Domain.Utils
{
    public static class TimeUtil
    {
        public static string GetDateInFormatYYYYmmddHHmm(DateTime dateTime)
            => dateTime.ToString("yyyy-MM-dd HH:mm");
    }
}
