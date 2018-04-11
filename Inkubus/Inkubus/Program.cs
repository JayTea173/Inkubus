using System;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;

namespace Inkubus
{
    using Engine;
    using Engine.IO;
    using Engine.Gfx.Text;
    class Program
    {

        static void Main(string[] args)
        {

            //BitmapFont someFont = new BitmapFont("arial.ttf");
            //Console.ReadKey();
            
            ConfigReader.Read("config.txt");

            DisplayDevice device = DisplayDevice.GetDisplay((DisplayIndex)GameWindowSettings.displayID);

            if (GameWindowSettings.width == 0)
                GameWindowSettings.width = device.Width;
            if (GameWindowSettings.height == 0)
                GameWindowSettings.height = device.Height;

            Debug.WriteLine("Using gfx device: " + device.ToString());
            InkubusCore core = new InkubusCore((GameWindowSettings.fullscreen) ? 0 : (device.Width / 2 - GameWindowSettings.width / 2),
                (GameWindowSettings.fullscreen) ? 0 : (device.Height / 2 - GameWindowSettings.height / 2),
                GameWindowSettings.width, 
                GameWindowSettings.height, 
                GraphicsMode.Default, 
                GameWindowSettings.fullscreen ? GameWindowFlags.Fullscreen : GameWindowFlags.FixedWindow, 
                device);
                

        }
    }
}
