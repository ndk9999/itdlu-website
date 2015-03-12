using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Net;
using ColorLife.Core.Helper;
namespace ColorLife.Core.WebService
{
    public class WeatherService
    {       
        public class MauDuBao
        {
            public string ThanhPho { get; set; }
            public string Ngay1 { get; set; }
            public string Ngay1DuLieu { get; set; }
            public string Ngay2 { get; set; }
            public string Ngay2DuLieu { get; set; }
            public string Ngay3 { get; set; }
            public string Ngay3DuLieu { get; set; }
            public MauDuBao()
            {
                ThanhPho = "";
                Ngay1 = "";
                Ngay1DuLieu = "";
                Ngay2 = "";
                Ngay2DuLieu = "";
                Ngay3 = "";
                Ngay3DuLieu = "";
            }
            public MauDuBao(string thanhpho)
            {
                ThanhPho = thanhpho;
                Ngay1 = "";
                Ngay1DuLieu = "";
                Ngay2 = "";
                Ngay2DuLieu = "";
                Ngay3 = "";
                Ngay3DuLieu = "";
            }
            public MauDuBao(string thanhpho, string ngay1, string ngay2, string ngay3, string dulieungay1, string dulieungay2, string dulieungay3)
            {
                ThanhPho = thanhpho;
                Ngay1 =  ngay1;
                Ngay1DuLieu = dulieungay1;
                Ngay2 =   ngay2;
                Ngay2DuLieu = dulieungay2;
                Ngay3 =   ngay3;
                Ngay3DuLieu = dulieungay3;
            }
        }       
        
        public static List<MySelectItem> GetAllCity()
        {
            // Trang chinh http://hcm.24h.com.vn/
            return HtmlAgilityPackHelper.GetDataSelectItem("http://hcm.24h.com.vn/ttcb/thoitiet/thoitiet.php", "//select[@name='sel_tinh_thanh']//option");
        }

        public static List<MauDuBao> DuBaoKetQua3Ngay()
        {
           // string url_old = "http://hcm.24h.com.vn/ttcb/thoitiet/thoi-tiet-tpho-chi-minh";
            string url = "http://hcm.24h.com.vn/ttcb/thoitiet/thoitiet.php";

            List<MauDuBao> list = new List<MauDuBao>();
            List<string> dstp = HtmlAgilityPackHelper.GetDataString(url, "//table/tbody/tr");
            List<string> bangay = HtmlAgilityPackHelper.GetDataString(url, "//td/b");//Lấy 3 ngày hôm nay, ngày mai, ngày kia
            List<string> bathu = HtmlAgilityPackHelper.GetDataString(url, "//td/i");//Lấy 3 thứ 6, thứ 7, chủ nhật   int i = 2;
            int i = 2;
            foreach (string s in dstp)
            {

                List<string> tt = HtmlAgilityPackHelper.GetDataString(url, "//tr[" + i + "]/td/div/span");//Lấy chi tiết thông tin: 5oC-18oC + Có mưa, mưa rào nhẹ
                MauDuBao m = new MauDuBao(s.ToString(), bangay[1] + " " + bathu[0], bangay[2] + " " + bathu[1], bangay[3] + " " + bathu[2], tt[0] + " " + tt[1], tt[2] + " " + tt[3], tt[4] + " " );
                list.Add(m);
                i++;
            }
            return list;
        }
        public static MauDuBao DuBaoKetQua3Ngay(string thanhPho)
        {       // /ttcb/thoitiet/thoi-tiet-tpho-chi-minh
            string url = "http://hcm.24h.com.vn/" + thanhPho;

            ColorLife.Core.WebService.WeatherService.MauDuBao m = null;
            List<string> dstp = HtmlAgilityPackHelper.GetDataString(url, "//table/tbody/tr");
            List<string> tp = HtmlAgilityPackHelper.GetDataString(url, "//tr[2]/td/div/h3/a/strong");
            List<string> bangay = HtmlAgilityPackHelper.GetDataString(url, "//td/b");//Lấy 3 ngày hôm nay, ngày mai, ngày kia
            List<string> bathu = HtmlAgilityPackHelper.GetDataString(url, "//td/i");//Lấy 3 thứ 6, thứ 7, chủ nhật
            int i = 2;
            foreach (string s in dstp)
            {
                List<string> tt = HtmlAgilityPackHelper.GetDataString(url, "//tr[" + i + "]/td/div/span");//Lấy chi tiết thông tin: 5oC-18oC + Có mưa, mưa rào nhẹ
                //  m = new MauDuBao(s.ToString(), bangay[1] + " " + bathu[0], bangay[2] + " " + bathu[1], bangay[3] + " " + bathu[2], tt[0] + " " + tt[1], tt[2] + " " + tt[3], tt[4] + " " + tt[5]);

                //string  abc = tp[0].ToString() + bangay[0] + bangay[1] + " " + bathu[0] + bangay[2] + " " + bathu[1] + bangay[3] + " " + bathu[2] + tt[0] + " " + tt[1] + tt[2] + " " + tt[3] + tt[4] + " " + tt[5];
                m = new ColorLife.Core.WebService.WeatherService.MauDuBao
                {
                    ThanhPho = tp[0],
                    Ngay1 = bangay[1] + " " + bathu[0],
                    Ngay1DuLieu = tt[0] + " " + tt[1],

                    Ngay2 = bangay[2] + " " + bathu[1],
                    Ngay2DuLieu = tt[2] + " " + tt[3],

                    Ngay3 = bangay[3] + " " + bathu[2],
                    Ngay3DuLieu = tt[4] + " " + tt[5],
                };
                // i++;
            }
            return m;
        }
    }
}