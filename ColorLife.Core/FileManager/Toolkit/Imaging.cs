using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;

namespace ColorLife.Core.FileManager
{

    public class Imaging
    {
        #region "Constructor(s)"
        private Imaging()
        {
        }
        #endregion

        #region "Helper Methods"

        public static Color[] GetColors(bool includeSystemColors)
        {
            KnownColor[] tempColors = (KnownColor[])Enum.GetValues(typeof(KnownColor));

            ArrayList colors = new ArrayList();

            for (int loopCount = 0; loopCount <= tempColors.Length - 1; loopCount++)
            {
                if ((!Color.FromKnownColor(tempColors[loopCount]).IsSystemColor | includeSystemColors) & !(Color.FromKnownColor(tempColors[loopCount]).Name == "Transparent"))
                {
                    colors.Add(Color.FromKnownColor(tempColors[loopCount]));
                }
            }
            return (Color[])colors.ToArray(typeof(Color));
        }

        public static string[] GetRotateTypes()
        {
            string[] tempResult = Enum.GetNames(typeof(RotateFlipType));
            Array.Sort(tempResult);
            return (tempResult);
        }

        public static FontFamily[] GetFontFamilies()
        {
            ArrayList fonts = new ArrayList();

            for (int loopCount = 0; loopCount <= FontFamily.Families.Length - 1; loopCount++)
            {
                fonts.Add(FontFamily.Families[loopCount]);
            }
            return (FontFamily[])fonts.ToArray(typeof(FontFamily));
        }

        #endregion

        #region "Text on Image Methods"

        public static void AddTextToImage(string fileNameIn, Font myFont, Color fontColor, Point textLocation, string textToAdd)
        {
            AddTextToImage(fileNameIn, fileNameIn, myFont, fontColor, textLocation, textToAdd);
        }


        public static void AddTextToImage(string fileNameIn, string fileNameOut, Font myFont, Color fontColor, Point textLocation, string textToAdd)
        {
            Graphics myGraphics = null;
            Bitmap myBitmap = null;

            try
            {
                myBitmap = new Bitmap(fileNameIn);

                myGraphics = Graphics.FromImage(myBitmap);

                StringFormat myStringFormat = new StringFormat();
                myStringFormat.Alignment = StringAlignment.Near;

                myGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                SolidBrush myBrush = new SolidBrush(fontColor);

                myGraphics.DrawString(textToAdd, myFont, myBrush, new Point(textLocation.X, textLocation.Y), myStringFormat);
                myBitmap.Save(fileNameOut, ImageFormat.Jpeg);
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (myGraphics != null)
                {
                    myGraphics.Dispose();
                }
                if (myBitmap != null)
                {
                    myBitmap.Dispose();
                }
            }
        }

        #endregion

        #region "Cropping Methods"

        public static void CropImage(string fileNameIn, Rectangle theRectangle)
        {
            CropImage(fileNameIn, fileNameIn, theRectangle);
        }

        public static void CropImage(string fileNameIn, string fileNameOut, Rectangle theRectangle)
        {
            Bitmap myBitmap = null;
            Bitmap myBitmapCropped = null;
            Graphics myGraphics = null;

            try
            {
                myBitmap = new Bitmap(fileNameIn);

                myBitmapCropped = new Bitmap(theRectangle.Width, theRectangle.Height);
                myGraphics = Graphics.FromImage(myBitmapCropped);

                myGraphics.DrawImage(myBitmap, new Rectangle(0, 0, myBitmapCropped.Width, myBitmapCropped.Height), theRectangle.Left, theRectangle.Top, theRectangle.Width, theRectangle.Height, GraphicsUnit.Pixel);

                myBitmap.Dispose();
                myBitmapCropped.Save(fileNameOut, ImageFormat.Jpeg);

            }
            catch  
            {
                throw;
            }
            finally
            {
                if (myBitmap != null)
                {
                    myBitmap.Dispose();
                }
                if (myBitmapCropped != null)
                {
                    myBitmapCropped.Dispose();
                }
                if (myGraphics != null)
                {
                    myGraphics.Dispose();
                }
            }
        }


        public static void DrawRectangle(string fileNameIn, Rectangle theRectangle, Color myColor)
        {
            DrawRectangle(fileNameIn, fileNameIn, theRectangle, myColor);
        }


        public static void DrawRectangle(string fileNameIn, string fileNameOut, Rectangle theRectangle, Color myColor)
        {
            Graphics myGraphics = null;
            Bitmap myBitmap = null;

            try
            {
                myBitmap = new Bitmap(fileNameIn);
                myGraphics = Graphics.FromImage(myBitmap);

                Pen myPen = new Pen(myColor, 1);
                myGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                myGraphics.DrawRectangle(myPen, theRectangle);
                myPen.Dispose();

                myBitmap.Save(fileNameOut, ImageFormat.Jpeg);
            }
            catch  
            {
                throw;
            }
            finally
            {
                if (myBitmap != null)
                {
                    myBitmap.Dispose();
                }
                if (myGraphics != null)
                {
                    myGraphics.Dispose();
                }
            }
        }


        #endregion

        #region "Rotating Methods"

        public static void RotateImage(string fileNameIn, string fileNameOut, RotateFlipType theRotateFlipType)
        {
            using (Bitmap myBitmap = new Bitmap(fileNameIn))
            {
                myBitmap.RotateFlip(theRotateFlipType);
                myBitmap.Save(fileNameOut, ImageFormat.Jpeg);
            }
        }

        public static void RotateImage(string fileNameIn, RotateFlipType theRotateFlipType)
        {
            RotateImage(fileNameIn, fileNameIn, theRotateFlipType);
        }

        #endregion

        #region "Resize Methods"

        public static void ResizeImage(string fileNameIn, int maxWidth, int maxHeight)
        {
            ResizeImage(fileNameIn, fileNameIn, maxWidth, maxHeight, ImageFormat.Jpeg);
        }            
        public static void ResizeImage(string fileNameIn, int maxWidthOrWidth)
        {
            ResizeImage(fileNameIn, fileNameIn, maxWidthOrWidth, ImageFormat.Jpeg);
        }

        public static void ResizeImage(string fileNameIn, string fileNameOut, Size theSize)
        {
            ResizeImage(fileNameIn, fileNameOut, theSize, ImageFormat.Jpeg);
        }


        public static void ResizeImage(string fileNameIn, string fileNameOut, int maxWidthOrHeight, ImageFormat theImageFormat)
        {
          //  bool portrait = false;
            Bitmap bmpSource = null;

            bmpSource = new Bitmap(fileNameIn);

            Size originalSize = bmpSource.Size;
            Size newSize = new Size(0, 0);

            bmpSource.Dispose();

            decimal maxHeightDecimal = Convert.ToDecimal(maxWidthOrHeight);
            decimal maxWidthAsDecimal = Convert.ToDecimal(maxWidthOrHeight);

            decimal resizeFactor = default(decimal);

            if ((originalSize.Height > originalSize.Width))
            {
                resizeFactor = Convert.ToDecimal(decimal.Divide(originalSize.Height, maxHeightDecimal));
                newSize.Height = maxWidthOrHeight;
                newSize.Width = Convert.ToInt32(originalSize.Width / resizeFactor);
            }
            else
            {
                resizeFactor = Convert.ToDecimal(decimal.Divide(originalSize.Width, maxWidthAsDecimal));
                newSize.Width = maxWidthOrHeight;
                newSize.Height = Convert.ToInt32(originalSize.Height / resizeFactor);
            }

            ResizeImage(fileNameIn, fileNameOut, newSize, theImageFormat);
        }


        public static void ResizeImage(string fileNameIn, string fileNameOut, int maxWidth, int maxHeight, ImageFormat theImageFormat)
        {
            Size originalSize = GetImageSize(fileNameIn);
            Size newSize = new Size(0, 0);

            decimal resizeFactor = System.Math.Max(Convert.ToDecimal(decimal.Divide(originalSize.Height, maxHeight)), Convert.ToDecimal(decimal.Divide(originalSize.Width, maxWidth)));

            newSize.Height = Convert.ToInt32(originalSize.Height / resizeFactor);
            newSize.Width = Convert.ToInt32(originalSize.Width / resizeFactor);

            ResizeImage(fileNameIn, fileNameOut, newSize, theImageFormat);
        }

        public static void ResizeImage(string fileNameIn, string fileNameOut, Size theSize, ImageFormat theImageFormat)
        {
            Bitmap mySourceBitmap = null;
            Bitmap myTargetBitmap = null;
            Graphics myGraphics = null;

            try
            {
                mySourceBitmap = new Bitmap(fileNameIn);

                int newWidth = theSize.Width;
                int newHeight = theSize.Height;

                myTargetBitmap = new Bitmap(newWidth, newHeight);

                myGraphics = Graphics.FromImage(myTargetBitmap);

                myGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                myGraphics.DrawImage(mySourceBitmap, new Rectangle(0, 0, newWidth, newHeight));
                mySourceBitmap.Dispose();

                myTargetBitmap.Save(fileNameOut, theImageFormat);
            }
            catch
            {
                throw;
            }
            finally
            {
                if ((myGraphics != null))
                {
                    myGraphics.Dispose();
                }
                if (mySourceBitmap != null)
                {
                    mySourceBitmap.Dispose();
                }
                if (myTargetBitmap != null)
                {
                    myTargetBitmap.Dispose();
                }
            }
        }


        #endregion

        #region "Other Imaging Methods"

        public static Size GetImageSize(string fileNameIn)
        {

            Bitmap myBitmap = null;
            Size theSize = Size.Empty;

            try
            {
                myBitmap = new Bitmap(fileNameIn);
                theSize = myBitmap.Size;
            }
            catch
            {
            }
            finally
            {
                if (myBitmap != null)
                {
                    myBitmap.Dispose();
                }
            }
            return theSize;
        }

        public static ImageFormat GetImageFormat(string fileNameIn)
        {

            Bitmap bmpSource = null;
            ImageFormat theFormat = null;

            try
            {
                bmpSource = new Bitmap(fileNameIn);
                theFormat = bmpSource.RawFormat;
            }
            catch
            {
                throw;
            }
            finally
            {
                bmpSource.Dispose();
            }
            return theFormat;
        }

        public static string GetImageHash(string fileNameIn)
        {
            Bitmap myBitmap = null;
            try
            {
                myBitmap = new Bitmap(fileNameIn);
                MemoryStream stream = new MemoryStream();
                myBitmap.Save(stream, ImageFormat.Bmp);
                byte[] bytes = stream.ToArray();
                byte[] HashVal = (new MD5CryptoServiceProvider()).ComputeHash(bytes);

                return Convert.ToBase64String(HashVal);
            }
            catch (OutOfMemoryException oomEx)
            {
                throw new ArgumentException("Path does not contain a valid image");
            }
            catch (ArgumentException aEx)
            {
                throw new ArgumentException("This is not a valid path");
            }
            catch (FileNotFoundException fnfEx)
            {
                throw new ArgumentException("File not found");
            }
            catch 
            {
                throw;
            }
            finally
            {
                if (myBitmap != null)
                {
                    myBitmap.Dispose();
                }
            }
        }
        #endregion

    }
}