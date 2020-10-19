using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TheaterApplication.Bll.Helpers.Interfaces;
using TheaterApplication.Bll.Models;
using TheaterApplication.Utils.Settings;

namespace TheaterApplication.Bll.Helpers
{
    public class TokenHelper: ITokenHelper
    {
        private readonly TokenSettings _settings;

        public TokenHelper(TokenSettings settings)
        {
            _settings = settings;
        }

        public UserWithTokenData DecryptData(string encriptedJson)
        {
            var json = Decrypt(encriptedJson);
            var result = JsonConvert.DeserializeObject<UserWithTokenData>(json);

            return result;
        }

        public string EncyptData(UserWithTokenData userWithTokenData)
        {
            var jsonToken = JsonConvert.SerializeObject(userWithTokenData, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            string result;

            byte[] clearBytes = Encoding.Unicode.GetBytes(jsonToken);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_settings.EncriptionPassword, _settings.EncriptionSalt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    result = Convert.ToBase64String(ms.ToArray());
                }
            }

            return result;
        }

        private string Decrypt(string encryptedText)
        {
            string result;
            encryptedText = encryptedText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(
                    _settings.EncriptionPassword, _settings.EncriptionSalt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }

                    result = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return result;
        }
    }
}
