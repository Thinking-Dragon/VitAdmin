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

        public static string Encrypter(string toEncrypt)
        {
            byte[] data = Md5Hash.ComputeHash(Encoding.UTF8.GetBytes(toEncrypt));

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                stringBuilder.Append(data[i].ToString("x2"));

            return stringBuilder.ToString();
        }
        public static bool Verifier(string toEncrypt, string hash)
            => StringComparer.OrdinalIgnoreCase.Compare(Encrypter(toEncrypt), hash) == 0;
    }
}
