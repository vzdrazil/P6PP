namespace ReservationSystem.Shared;

public static class ServiceEndpoints
{
    public static class UserService
    {
        // USERS
        private const string BaseUrl = "https://localhost:5189";
        
        public static string CreateUser => $"{BaseUrl}/api/user";
        public static string GetUserById(int id) => $"{BaseUrl}/api/user/{id}";
        
        // ROLES
        public static string GetRoleById(int id) => $"{BaseUrl}/api/role/{id}";
        public static string CreateRole => $"{BaseUrl}/api/role";
    }

    public static class AuthService
    {
        private const string BaseUrl = "https://localhost:8005";
        
        public static string Login => $"{BaseUrl}/api/auth/login";
        public static string Register => $"{BaseUrl}/api/auth/register";
    }
    
}