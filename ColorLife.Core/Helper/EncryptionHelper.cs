/*
 * Created by Le Thanh Tuan
 * Email: tuanitpro@gmail.com
 * Skype: tuan.itpro
 * Facebook: http://facebook.com/tuanitpro
*/


using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
namespace ColorLife.Core.Helper
{
    public static class EncryptionHelper
    {
        public static string EncodePassword(this string stringValue)
        {
            stringValue = FormsAuthentication.HashPasswordForStoringInConfigFile(stringValue, "MD5");
            string key = "azsxc65dc26006ca53138c3f2906c4758eaf1902";
            key = FormsAuthentication.HashPasswordForStoringInConfigFile(key, "SHA1");

            string lastText = stringValue + key;
            lastText = FormsAuthentication.HashPasswordForStoringInConfigFile(lastText, "SHA1");
            return lastText;
        }

        public static string EncodeMD5Gravartar(this string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string EncodeMD5(this string stringValue)
        {
            Byte[] original, encode, encode2;
            MD5 md5 = new MD5CryptoServiceProvider();
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            original = ASCIIEncoding.Default.GetBytes(stringValue);
            encode = md5.ComputeHash(original);
            encode2 = sha1.ComputeHash(encode);
            var sb = new StringBuilder();
            for (int i = 0; i < encode2.Length; i++)
                sb.Append(encode2[i].ToString("x2").ToUpper());
            return sb.ToString();
        }
        public static string EncodeMD52(this string stringValue)
        {
            Byte[] original;
            Byte[] encode;
            MD5 md5 = new MD5CryptoServiceProvider();
            original = ASCIIEncoding.Default.GetBytes(stringValue);
            encode = md5.ComputeHash(original);
            return BitConverter.ToString(encode);
        }
        public static string EncodeSHA1(this string stringValue)
        {
            Byte[] original;
            Byte[] encode;
            SHA1 md5 = new SHA1CryptoServiceProvider();
            original = ASCIIEncoding.Default.GetBytes(stringValue);
            encode = md5.ComputeHash(original);
            return BitConverter.ToString(encode);
        }
        public static string EncodeSHA256(this string stringValue)
        {
            Byte[] original;
            Byte[] encode;
            SHA256 md5 = new SHA256CryptoServiceProvider();
            original = ASCIIEncoding.Default.GetBytes(stringValue);
            encode = md5.ComputeHash(original);
            return BitConverter.ToString(encode);
        }
        public static string EncodeSHA384(this string stringValue)
        {
            Byte[] original;
            Byte[] encode;
            SHA384 md5 = new SHA384CryptoServiceProvider();
            original = ASCIIEncoding.Default.GetBytes(stringValue);
            encode = md5.ComputeHash(original);
            return BitConverter.ToString(encode);
        }
        public static string EncodeSHA512(this string stringValue)
        {
            Byte[] original;
            Byte[] encode;
            SHA512 md5 = new SHA512CryptoServiceProvider();
            original = ASCIIEncoding.Default.GetBytes(stringValue);
            encode = md5.ComputeHash(original);
            return BitConverter.ToString(encode);
        }
    }
}
