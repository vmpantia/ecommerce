using ECommerce.BAL.Contractors;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.BAL.Utilities
{
    public class PasswordUtil : IPasswordUtil
    {
        private readonly IConfiguration _config;
        public PasswordUtil(IConfiguration config) => _config = config;

        public string ParsePassword(string password, bool isEncrypt)
        {
            if (string.IsNullOrEmpty(password))
                return password;

            byte[] data = isEncrypt ? UTF32Encoding.UTF8.GetBytes(password) : Convert.FromBase64String(password);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_config["Password:Hash"]));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    if (isEncrypt)
                    {
                        ICryptoTransform encrypt = tripDes.CreateEncryptor();
                        byte[] resultsEncrypt = encrypt.TransformFinalBlock(data, 0, data.Length);
                        return Convert.ToBase64String(resultsEncrypt, 0, resultsEncrypt.Length);
                    }
                    ICryptoTransform decrypt = tripDes.CreateDecryptor();
                    byte[] resultsDecrypt = decrypt.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(resultsDecrypt);
                }
            }
        }
    }
}
