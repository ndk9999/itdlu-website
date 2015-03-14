/* FileName: StringHelper.cs
Project Name: ColorLife
Date Created: 8/5/2014 7:29:00 AM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 

*/

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Xml;
namespace ColorLife.Core.Helper
{
    public static  class StringHelper
    {
        public static string ReadFileToString(this string path)
        {
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(path))
            {
                sb.Append(sr.ReadToEnd());
            }
            return sb.ToString();
        }
        public static bool IsValidEmail(this string email)
        {
            bool result = false;
            if (String.IsNullOrEmpty(email))
                return result;
            email = email.Trim();
            result = Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return result;
        }
        public static bool IsValidPassword(this string password)
        {
            bool result = false;
            if (string.IsNullOrEmpty(password))
                return result;
            password = password.Trim();
            result = Regex.IsMatch(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&*]).{6,20}.*$");
            return result;
        }
        public static string UrlEncode(this string stringValue)
        {
            return HttpUtility.UrlEncode(stringValue);
        }

        public static string UrlDecode(this string stringValue)
        {
            return HttpUtility.UrlDecode(stringValue);
        }
        public static string RandomString(int len)
        {
            string allowedChars = "";
            allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string randomString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                randomString += temp;
            }
            return randomString;
        }
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }       
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);
        private static string MyTruncateString(string str, int maxLength)
        {
            if (str.Length < maxLength)
                return str;

            var words = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words[0].Length > maxLength)
                throw new ArgumentException("Từ đầu tiên dài hơn chuỗi cần cắt");
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if ((sb + word).Length > maxLength)
                    return string.Format("{0}...", sb.ToString().TrimEnd(' '));
                sb.Append(word + " ");
            }
            return string.Format("{0}...", sb.ToString().TrimEnd(' '));
        }
        public static string TruncateString(this string stringValue, int maxLength)
        {
            return MyTruncateString(stringValue, maxLength);
        }
        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }
        public static string TruncateDescription(this string stringData, int maxLength)
        {
            string plainText = StripTagsRegexCompiled(stringData);
            return MyTruncateString(plainText, maxLength);
        }
        /// <summary>
        /// Ham xoa khoang trang trong chuoi
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static string RemoveWhitespace(this string stringValue)
        {
            string strPattern = @"[\s]+";
            Regex RegExp = new Regex(strPattern);
            return RegExp.Replace(stringValue, "").Trim();
        }
        private static string[] VietNamChar =
             { 
           "aAeEoOuUiIdDyY", 
           "áàạảãâấầậẩẫăắằặẳẵ", 
           "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", 
           "éèẹẻẽêếềệểễ", 
           "ÉÈẸẺẼÊẾỀỆỂỄ", 
           "óòọỏõôốồộổỗơớờợởỡ", 
           "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", 
           "úùụủũưứừựửữ", 
           "ÚÙỤỦŨƯỨỪỰỬỮ", 
           "íìịỉĩ", 
           "ÍÌỊỈĨ", 
           "đ", 
           "Đ", 
           "ýỳỵỷỹ", 
           "ÝỲỴỶỸ"        
             };
        public static string ReplaceUnicode(this string strInput)
        {
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    strInput = strInput.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            return strInput;
        }

        /// <summary>
        /// Ham xoa dau Unicode
        /// </summary>
        /// <param name="content"></param>
        /// <returns>Noi dung da duoc xoa dau</returns>
        public static string RemoveDiacritics(this string content)
        {
            String normalizedString = content.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }
        /// <summary>
        /// Ham xoa dau Unicode URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Url alias</returns>
        public static string RemoveDiacriticsURL(this string url)
        {
            string str = RemoveDiacritics(url).ToLower();
            // return str.Replace(" ", "-");
            string strPattern = @"[\s\:\'\""\,\+\&\?\\\!]+";
            Regex RegExp = new Regex(strPattern);
            return RegExp.Replace(str, "-").Trim();
        }
        public static string GetImageSrcFromContent(this string content)
        {
            string s = "";
            string pattern = "<img([^s]|s[^r]|sr[^c]|src[^=]|src=[^'\"])*src=['\"](?<SRC>[^'\"]*)['\"]";
            MatchCollection m = Regex.Matches(content, pattern);
           
            foreach (Match mm in m)
            {
                //  Response.Write(mm.Groups["SRC"].Value + "");
                s += mm.Groups["SRC"].Value + "|";
            }
            return s;
        }

        public static string GetImageUrlFromContent(this string content)
        {
            //string pattern = "<img[^<]+";            
            //Regex ImageRegex = new Regex(pattern);
            //Match m = ImageRegex.Match(content);
            //if (m.Success)
            //    //  return m.Value.Substring(10, m.Value.Length - 10);
            //    return m.Value;
            //  return "";


            // Get Url Only
            string matchString = Regex.Match(content, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
            return matchString;
        }
        public static string GetYoutubeID(this string url)
        {
            string Youtube = @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)";
            string Vimeo = @"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)";

            Match youtubeMatch = Regex.Match(url, Youtube);
            // Match vimeoMatch = VimeoVideoRegex.Match(url);

            string id = string.Empty;

            if (youtubeMatch.Success)
                id = youtubeMatch.Groups[1].Value;

            // if (vimeoMatch.Success)
            // id = vimeoMatch.Groups[1].Value;
            return id;
        }
        public static bool IsValidXml(this string xmlString)
        {
            Regex tagsWithData = new Regex("<\\w+>[^<]+</\\w+>");

            //Light checking
            if (string.IsNullOrEmpty(xmlString) || tagsWithData.IsMatch(xmlString) == false)
            {
                return false;
            }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlString);
                return true;
            }
            catch  
            {
                return false;
            }
        }
    }
}
