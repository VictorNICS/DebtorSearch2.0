using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NICS.System.Security
{
  public static  class SecureConfig
    {
        public static string EncryptString(string message, string passphrase)
        {
            UTF8Encoding uTF8Encoding = new UTF8Encoding();
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] key = mD5CryptoServiceProvider.ComputeHash(uTF8Encoding.GetBytes(passphrase));
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider
            {
                Key = key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] bytes = uTF8Encoding.GetBytes(message);
            byte[] inArray;
            try
            {
                ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
                inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
            }
            finally
            {
                tripleDESCryptoServiceProvider.Clear();
                mD5CryptoServiceProvider.Clear();
            }
            return Convert.ToBase64String(inArray);
        }

        public static string DecryptString(string message, string passphrase)
        {
            UTF8Encoding uTF8Encoding = new UTF8Encoding();
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] key = mD5CryptoServiceProvider.ComputeHash(uTF8Encoding.GetBytes(passphrase));
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider
            {
                Key = key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] array = Convert.FromBase64String(message);
            byte[] bytes;
            try
            {
                ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
                bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
            }
            finally
            {
                tripleDESCryptoServiceProvider.Clear();
                mD5CryptoServiceProvider.Clear();
            }
            return uTF8Encoding.GetString(bytes);
        }

        public static string GetMd5Hash(string message)
        {

          
            byte[] encodedPassword = new UTF8Encoding().GetBytes(message);      
            byte[] hash = MD5.Create().ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            return encoded;
        }

    }
}
