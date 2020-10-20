namespace TaskManagerApp.ApiHelpers
{
    public static class ApiPath
    {
        public static string Get(string controller) => $"{controller}/Get";
        
        public static string Get(string controller, int id) => $"{controller}/Get/{id}";
        
        public static string GetDropDownList(string controller, int id) => $"{controller}/GetAllForDropDownList/{id}";

        public static string Create(string controller) => $"{controller}/Create";

        public static string BatchCreate(string controller) => $"{controller}/BatchCreate";

        public static string Update(string controller) => $"{controller}/Update";

        public static string BatchUpdate(string controller) => $"{controller}/BatchUpdate";
    }
}
