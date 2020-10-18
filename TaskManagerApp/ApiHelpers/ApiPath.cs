namespace TaskManagerApp.ApiHelpers
{
    public static class ApiPath
    {
        public static string Get(string controller) => $"{controller}/Get";

        public static string Get(string controller, int id) => $"{controller}/Get/{id}";

        public static string Create(string controller) => $"{controller}/Create";

        public static string Update(string controller) => $"{controller}/Update";
    }
}
