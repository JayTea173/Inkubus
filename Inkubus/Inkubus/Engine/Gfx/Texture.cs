﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;

namespace Inkubus.Engine.Gfx
{
    class Texture : IDisposable
    {
        protected int width, height;
        protected Vector2 size;

        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
        }

        protected float[] pixeldata;
        protected int id;

        public Texture(string fileName)
        {
            Bitmap tmpbmp = (Bitmap)Image.FromFile(fileName);
            width = tmpbmp.Width;
            height = tmpbmp.Height;
            size = new Vector2(width, height);
            long arrSize = width * height * 4;
            pixeldata = new float[arrSize];


            int i = 0;
            BitmapData bmpdata = null;

            try
            {
                bmpdata = tmpbmp.LockBits(new Rectangle(0, 0, width, height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                      System.Drawing.Imaging.PixelFormat.Format32bppArgb);


                unsafe
                {
                    var ptr = (byte*)bmpdata.Scan0;
                    int remain = bmpdata.Stride - bmpdata.Width * 4;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //pixeldata[i++] = 1.0f;
                            //pixeldata[i++] = 1.0f;
                            //pixeldata[i++] = 1.0f;

                            pixeldata[i++] = (float)ptr[2] / 255.0f;
                            pixeldata[i++] = (float)ptr[1] / 255.0f;
                            pixeldata[i++] = (float)ptr[0] / 255.0f;
                            pixeldata[i++] = (float)ptr[3] / 255.0f;
                            //pixeldata[i++] = 1.0f;
                            //Debug.Write(remain + ": " + pixeldata[i-4] + ", \t\t" + pixeldata[i-3] + ",  \t\t" + pixeldata[i-2] + ",  \t\t" + pixeldata[i-1] + "\n");
                            ptr += 4;
                        }
                        ptr += remain;
                    }

                }

            }
            catch (System.Exception e)
            {
                Debug.WriteLine("Error loading texture: " + fileName + ": " + e.Message);
            }
            finally
            {
                tmpbmp.UnlockBits(bmpdata);
            }

            /*float maxAniso = 0f;
            GL.GetFloat((GetPName)All.TextureMaxAnisotropyExt, out maxAniso);
            GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)All.TextureMaxAnisotropyExt, maxAniso);
            */

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.ClampToEdge, 1);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, 2);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, 2);


            GL.CreateTextures(TextureTarget.Texture2D, 1, out id);
            GL.TextureStorage2D(id, 1, SizedInternalFormat.Rgba32f, width, height);

            GL.BindTexture(TextureTarget.Texture2D, id);
            GL.TextureSubImage2D(id, 0, 0, 0, width, height, OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.Float, pixeldata);


        }

        public virtual void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, id);

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                GL.DeleteTexture(id);
        }
    }
}
