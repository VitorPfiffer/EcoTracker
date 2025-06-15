namespace EcoTracker.Core.Data
{
    public static class ControllersInfo
    {
        public static bool HasVersioning { get; set; }

        public static string GetFinalUrl()
        {
            var url = "api/";

            if (HasVersioning)
                url += "v{version:apiVersion}/";

            return url;
        }
    }
}
