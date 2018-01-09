using System;
using OpenTK;
using OpenTK.Graphics;

namespace Inkubus
{
    class Program
    {

        static void Main(string[] args)
        {
            DisplayDevice device = DisplayDevice.GetDisplay(0);
            
            InkubusCore core = new InkubusCore(0, 0, device.Width, device.Height, GraphicsMode.Default, GameWindowFlags.Fullscreen, device);

        }
    }
}
