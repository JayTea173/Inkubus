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

            InkubusCore core = new InkubusCore(0, 0, GameWindowSettings.width, GameWindowSettings.height, GraphicsMode.Default, GameWindowFlags.Fullscreen, device);

        }
    }
}
