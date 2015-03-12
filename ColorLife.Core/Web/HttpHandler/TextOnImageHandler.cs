using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace ColorLife.Core.HttpHandler
{
    public class TextOnImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {

            string path = context.Request["img"];
            string text = context.Request["text"];
            string fileNameIn = context.Server.MapPath(path);
            Bitmap myBitmap = new Bitmap(fileNameIn);

            Graphics myGraphics = Graphics.FromImage(myBitmap);

            StringFormat myStringFormat = new StringFormat();
            myStringFormat.Alignment = StringAlignment.Near;

            myGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Font myFont = new Font("Tahoma", 15, FontStyle.Italic);
            Color fontColor = Color.Black;
            SolidBrush myBrush = new SolidBrush(fontColor);

            myGraphics.DrawString(text, myFont, myBrush, new Point(10, 10), myStringFormat);
            context.Response.ContentType = "image/jpeg";
            myBitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
