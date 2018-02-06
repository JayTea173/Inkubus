using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;


namespace Inkubus.Engine.Gfx.Text
{
    class BitmapFont
    {
        public static readonly string fontPath = /*Directory.GetParent(Environment.CurrentDirectory) + */"..\\data\\fonts\\";
        public string defaultCharset = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!\"§$%&/()=ß´?`*+'#,;.:-_^°";

        public BitmapFont(string fontFile, string charset = "")
        {
            if (charset == "")
                charset = defaultCharset;

            string fontFileName = Path.GetFileNameWithoutExtension(fontFile);
            int fontSize = 64;
            Font font;
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(fontPath + fontFile);
            FontFamily ff = new FontFamily(fontFileName, pfc);
            font = new Font(ff, fontSize);

            Bitmap bmp = new Bitmap("./" + fontFileName + ".bmp");


            Graphics g = Graphics.FromImage(bmp);

            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(charset, font);



            double nextPowr = (double)(CeilToNextPowerOfTwo((int)stringSize.Width)/2);
            bmp = ResizeImage(bmp, (int)nextPowr, /*font.Height*/(int)font.Height);


            g.Flush();
            g = Graphics.FromImage(bmp);
           


            RectangleF rect = new RectangleF(0f, 0f, (float)nextPowr, (float)font.Height);
            Rectangle recti = new Rectangle(0, 0, (int)nextPowr, font.Height);


            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            g.DrawRectangle(Pens.Black, recti);
            g.DrawString(charset, font, Brushes.Black, rect);

            float[] widths = new float[charset.Length];
            for (int i = 0; i < charset.Length; i++)
            {
                widths[i] = g.MeasureString(charset[i].ToString(), font).Width;
            }


            
            

            g.Flush();
            Bitmap distanceField = GetDistanceField(bmp);

            bmp.Save(fontFile + ".bmp");

            Console.WriteLine(font);
        }

        public static Bitmap GetDistanceField(Bitmap whiteMap)
        {
            Bitmap df = new Bitmap(whiteMap);

            return df;
        }

        public static int CeilToNextPowerOfTwo(int number)
        {
            int a = number;
            int powOfTwo = 1;

            while (a > 1)
            {
                a = a >> 1;
                powOfTwo = powOfTwo << 1;
            }
            if (powOfTwo != number)
            {
                powOfTwo = powOfTwo << 1;
            }
            return powOfTwo;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }



    }
}
