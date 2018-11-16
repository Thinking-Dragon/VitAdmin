using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Helper
{
    public static class Crypto
    {
        private static MD5 Md5Hash { get; set; } = MD5.Create();

        /// <summary>
        /// Encrypte une chaine de caractères
        /// </summary>
        /// <param name="toEncrypt">Chaine à encrypter</param>
        /// <returns>La version encryptée de la chaine</returns>
        public static string Encrypter(string toEncrypt)
        {
            byte[] data = Md5Hash.ComputeHash(Encoding.UTF8.GetBytes(toEncrypt));

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                stringBuilder.Append(data[i].ToString("x2"));

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Vérifie si une chaine de caractères correspond à une chaine encryptée
        /// </summary>
        /// <param name="toEncrypt">Chaine à comparer</param>
        /// <param name="hash">Version encryptée</param>
        /// <returns>Si la chaine correspond à celle qui est encryptée</returns>
        public static bool Verifier(string toEncrypt, string hash)
            => StringComparer.OrdinalIgnoreCase.Compare(Encrypter(toEncrypt), hash) == 0;
    }
}
