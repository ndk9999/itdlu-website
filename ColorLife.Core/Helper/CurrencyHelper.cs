/*
 * Chuyển đổi số thành chữ theo dạng phát biểu của ngôn ngữ nói
  Copyright Nguyễn Phú Cường
  mail: cuongmedia@gmail.com
  phone: 0985791021
 */

using System;
using System.Collections.Generic;
using System.Text;
namespace ColorLife.Core.Helper
{

    public class CurrencyHelper
    {
        private static string[] strDonViLon = { "", "nghìn", "triệu", "tỷ" };
        private static string[] strDonViNho = { "linh", "lăm", "mười", "mươi", "mốt", "trăm" };
        private static string[] strSo = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

        
        private static string DocMotChuSo(string so)
        {
            return strSo[int.Parse(so)];
        }


        public static string DocChuSo(string num)
        {
            string sNum = num.ToString(), temp = "";
            int len = sNum.Length, nhomso;
            string str = "";
            int i = 1;
            while (i <= len)
            {
                str = str + " " + DocMotChuSo(sNum.Substring(i - 1, 1));
                nhomso = (int)((len - i) % 9);
                if (i == len) break;
                if (nhomso == 0)
                {
                    str += " tỷ";
                    for (int j = 0; j < 3; j++)
                    {
                        temp = sNum.Substring(i, 3);
                        if (temp == "000")
                            i += 3;
                    }

                }
                else
                    if (nhomso == 6)
                    {
                        str += " triệu";
                        for (int j = 0; j < 2; j++)
                        {
                            temp = sNum.Substring(i, 3);
                            if (temp == "000")
                                i += 3;
                        }
                    }
                    else
                        if (nhomso == 3) // Hàng nghìn
                        {
                            str += " nghìn";
                            temp = sNum.Substring(i, 3);
                            if (temp == "000")
                                i += 3;
                        }
                        else
                        {
                            nhomso = (int)((len - i) % 3);
                            if (nhomso == 2)
                                str += " trăm";
                            else
                                if (nhomso == 1)
                                    str += " mươi";
                        }

                i++;
            }
            str = str.Replace("không mươi không", "");
            str = str.Replace("không mươi ", "linh ");
            str = str.Replace("mươi không", "mươi");
            str = str.Replace("một mươi", "mười");
            str = str.Replace("mươi bốn", "mươi tư");
            str = str.Replace("linh bốn", "linh tư");
            str = str.Replace("mươi một", "mươi mốt");
            str = str.Replace("mươi năm", "mươi lăm");
            str = str.Replace("mười năm", "mười lăm");
            str = str.Trim();
            return str;
        }
        public static string DocTienTeVietNam(string num)
        {
            string str = "";
            str = DocChuSo(num) + " đồng";
            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}