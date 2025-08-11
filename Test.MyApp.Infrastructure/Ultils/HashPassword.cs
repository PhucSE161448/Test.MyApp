using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.MyApp.Infrastructure.Ultils
{
    public static class HashPassword
    {
        public static string HashString(string rawPassword)
        {
            byte[] bytes = System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(rawPassword));
            return Convert.ToBase64String(bytes);
        }

        public static bool VerifyPassword(string rawPassword, string storedHash)
        {
            string hashedPassword = HashString(rawPassword);
            return hashedPassword == storedHash;
        }
    }
}
