using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public class SaltedHash
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        public SaltedHash(string password)
        {
            var saltBytes = new byte[32];
            new Random().NextBytes(saltBytes);
            Salt = Convert.ToBase64String(saltBytes);
            var passwordWithSalBytes = Concat(password, saltBytes);
            Hash = ComputeHash(passwordWithSalBytes);
        }

        private byte[] Concat(string password, byte[] saltBytes)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var byteArrayResult = new byte[passwordBytes.Length + saltBytes.Length];

            for (var i = 0; i < passwordBytes.Length; i++)
                byteArrayResult[i] = passwordBytes[i];
            for (var i = 0; i < saltBytes.Length; i++)
                byteArrayResult[passwordBytes.Length + i] = saltBytes[i];

            return byteArrayResult;
        }

        static string ComputeHash(byte[] bytes)
        {
            using (var sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(bytes));
            }
        }

        public bool Verify(string salt, string hash, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var passwordAndSaltBytes = Concat(password, saltBytes);
            var hashAttempt = ComputeHash(passwordAndSaltBytes);
            return hash == hashAttempt;
        }
    }
}
