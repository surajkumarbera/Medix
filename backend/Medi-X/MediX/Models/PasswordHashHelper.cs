using System.Web.Helpers;

namespace MediX.Models
{
    public static class PasswordHashHelper
    {
        public static string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public static bool MatchHashedPassword(string hashedPssword, string plainPassword)
        {
            return Crypto.VerifyHashedPassword(hashedPssword, plainPassword);
        }
    }
}