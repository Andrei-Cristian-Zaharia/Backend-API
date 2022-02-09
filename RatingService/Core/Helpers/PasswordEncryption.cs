using Core.Features;
using DTO.Models;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers
{
    /// <summary>
    /// Contains methods to encrypt and decrypt passwords.
    /// </summary>
    public static class PasswordEncryption
    {
        /// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="encryptionSettings"></param>
        /// <returns></returns>
        public static User EncryptPassword(this User user, IEncryptionSettings encryptionSettings)
        {
            var password = user.Password;

            if (string.IsNullOrEmpty(password)) return user;
            try
            {
                var key = Encoding.UTF8.GetBytes(encryptionSettings.Key);

                using (var aesAlg = Aes.Create())
                {
                    using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                    {
                        using (var msEncrypt = new MemoryStream())
                        {
                            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(password);
                            }

                            var iv = aesAlg.IV;

                            var decryptedContent = msEncrypt.ToArray();

                            var result = new byte[iv.Length + decryptedContent.Length];

                            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                            var str = Convert.ToBase64String(result);
                            var fullCipher = Convert.FromBase64String(str);

                            user.Password = str;

                            return user;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Decrypts the password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="encryptionSettings"></param>
        /// <returns></returns>
        public static string Decrypt(string password, IEncryptionSettings encryptionSettings)
        {
            if (string.IsNullOrEmpty(password)) return password;
            try
            {
                password = password.Replace(" ", "+");
                var fullCipher = Convert.FromBase64String(password);

                var iv = new byte[16];
                var cipher = new byte[fullCipher.Length - iv.Length];

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
                var key = Encoding.UTF8.GetBytes(encryptionSettings.Key);

                using (var aesAlg = Aes.Create())
                {
                    using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                    {
                        string result;
                        using (var msDecrypt = new MemoryStream(cipher))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    result = srDecrypt.ReadToEnd();
                                }
                            }
                        }

                        return result;
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}