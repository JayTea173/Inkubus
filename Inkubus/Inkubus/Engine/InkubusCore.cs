﻿using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Inkubus
{
    using Engine.Input;

    class InkubusCore : GameWindow
    {
        public const string versionString = "Alpha 0.0.0";
        public const string buildString = "180106";

        protected InputManager inputManager;

        public InkubusCore(int x, int y, int width, int height, GraphicsMode mode, GameWindowFlags flags, DisplayDevice device) : base(width, height, mode, "Inkubus ~~" + versionString, flags, device, 4, 4, GraphicsContextFlags.ForwardCompatible)
        {
            WindowBorder = WindowBorder.Hidden;
            Load += OnWindowLoaded;
            RenderFrame += Render;
            Location = new Point(x, y);

            inputManager = new InputManager();
            Run(1.0d / 60.0d);
            

        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            if (e.Alt && e.Key == Key.F4)
                Environment.Exit(0);
            
        }

        void OnWindowLoaded(object o, EventArgs args)
        {
            GL.ClearColor(0.0666f, 0f, 0f, 0f);
        }
        protected override void OnResize(EventArgs args)
        {
            GL.Viewport(0, 0, Width, Height);
        }

        void Render(object o, EventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            SwapBuffers();
        }


    }
}
