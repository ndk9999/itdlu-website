using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ColorLife.Core.HttpHandler
{
    public class HttpImageHandler : IHttpHandler
    {
        int Width = 0;
        int Height = 0;

        public void ProcessRequest(System.Web.HttpContext context)
        {
            if (context.Request.Url.OriginalString.StartsWith("http://") == false)
            {

            }

            if (context.Request.Params["h"] != null)
            {
                try
                {
                    Height = int.Parse(context.Request.Params["h"]);
                }
                catch
                {
                    Height = 0;
                }
            }

            if (context.Request.Params["w"] != null)
            {
                try
                {
                    Width = int.Parse(context.Request.Params["w"]);
                }
                catch
                {
                    Width = 0;
                }
            }

            if (Width <= 0 && Height <= 0)
            {

                context.Response.Clear();
                context.Response.ContentType =
                getContentType(context.Request.PhysicalPath);
                context.Response.WriteFile(context.Request.PhysicalPath);
                context.Response.End();

            }
            else
            {
                context.Response.Clear();
                context.Response.ContentType =
                getContentType(context.Request.PhysicalPath);
                byte[] buffer =
                getResizedImage(context.Request.PhysicalPath, Width, Height);
                context.Response.OutputStream.Write
                (buffer, 0, buffer.Length);
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        byte[] getResizedImage(String path, int width, int height)
        {
            Bitmap imgIn = new Bitmap(path);
            double y = imgIn.Height;
            double x = imgIn.Width;

            double factor = 1;
            if (width > 0)
            {
                factor = width / x;
            }
            else if (height > 0)
            {
                factor = height / y;
            }
            System.IO.MemoryStream outStream =
            new System.IO.MemoryStream();
            Bitmap imgOut =
            new Bitmap((int)(x * factor), (int)(y * factor));
            Graphics g = Graphics.FromImage(imgOut);
            g.Clear(Color.White);
            g.DrawImage(imgIn, new Rectangle(0, 0, (int)(factor * x),
            (int)(factor * y)),
            new Rectangle(0, 0, (int)x, (int)y), GraphicsUnit.Pixel);

            imgOut.Save(outStream, getImageFormat(path));
            return outStream.ToArray();
        }

        string getContentType(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return "Image/bmp";
                case ".gif": return "Image/gif";
                case ".jpg": return "Image/jpeg";
                case ".png": return "Image/png";
                default: break;
            }
            return "";
        }

        ImageFormat getImageFormat(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".jpg": return ImageFormat.Jpeg;
                case ".png": return ImageFormat.Png;
                default: break;
            }
            return ImageFormat.Jpeg;
        }
    }
}