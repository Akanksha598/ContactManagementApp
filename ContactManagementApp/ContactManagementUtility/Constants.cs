namespace ContactManagementUtility
{
    public static class Constants
    {
        public static string Error = "Error";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        };

        public const string ContactApiUrl = "/api/v1/contactApi/";
    }
}