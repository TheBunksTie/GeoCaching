using System;
using System.Security.Cryptography;
using System.Text;

namespace Swk5.GeoCaching.BusinessLogic {
    public static class PasswordEncrypter {
        private static readonly SHA1CryptoServiceProvider CryptoService = new SHA1CryptoServiceProvider();

        public static string Encrypt(this string password) {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = CryptoService.ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}