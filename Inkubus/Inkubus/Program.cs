using System;
using OpenTK;
using OpenTK.Graphics;

namespace Inkubus
{
    using Engine;
    using Engine.IO;
    class Program
    {

        static void Main(string[] args)
        {
            DisplayDevice device = DisplayDevice.GetDisplay(0);

            ConfigReader.Read("config.txt");

            if (GameWindowSettings.width == 0)
                GameWindowSettings.width = device.Width;
            if (GameWindowSettings.height == 0)
                GameWindowSettings.height = device.Height;

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
