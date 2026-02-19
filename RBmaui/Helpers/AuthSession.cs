using Microsoft.Maui.Storage;

namespace RBmaui.Helpers
{
    public static class AuthSession
    {
        public static string Token => Preferences.Get("auth_token", "");
        public static string Email => Preferences.Get("user_email", "");
        public static string Role => Preferences.Get("user_role", "");

        public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(Token);

        public static void Save(string token, string email, string role)
        {
            Preferences.Set("auth_token", token);
            Preferences.Set("user_email", email);
            Preferences.Set("user_role", role);
        }

        public static void Clear()
        {
            Preferences.Remove("auth_token");
            Preferences.Remove("user_email");
            Preferences.Remove("user_role");
        }
    }
}
