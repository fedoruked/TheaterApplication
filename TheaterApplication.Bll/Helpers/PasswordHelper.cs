using System;
using System.Security.Cryptography;
using System.Text;
using TheaterApplication.Bll.Helpers.Interfaces;

namespace TheaterApplication.Bll.Helpers
{
    public class PasswordHelper : IPasswordHelper
    {
        public string Encrypt(string password)
        {
            string result = null;

            if (!string.IsNullOrEmpty(password))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = new SHA256Managed().ComputeHash(bytes);

                result = BytesToString(hash);
            }

            return result;
        }

        private string BytesToString(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (byte x in bytes)
            {
                sb.Append(String.Format("{0:x2}", x));
            }

            return sb.ToString();
        }
    }
}
