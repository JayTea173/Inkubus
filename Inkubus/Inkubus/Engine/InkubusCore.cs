using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Inkubus
{
    using Engine.Input;
    using Engine.Graphics.Shaders;
    

    class InkubusCore : GameWindow
    {
        public const string versionString = "Alpha 0.0.0";
        public const string buildString = "180106";

        protected ShaderProgram someShaderProgram;
        double time = 0.0d;

        protected InputManager inputManager;

        public InkubusCore(int x, int y, int width, int height, GraphicsMode mode, GameWindowFlags flags, DisplayDevice device) : base(width, height, mode, "Inkubus ~~" + versionString, flags, device, 4, 4, GraphicsContextFlags.ForwardCompatible)
        {
            //WindowBorder = WindowBorder.Hidden;
            Load += OnWindowLoaded;
            Location = new Point(x, y);
            Closed += OnClosed;

            inputManager = new InputManager();
            Run(1.0d / 60.0d);
           

        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            ShaderManager.Instance.Destroy();
        }


        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            if ((e.Alt && e.Key == Key.F4) || e.Key == Key.Escape)
                Environment.Exit(0);
            
        }

        void OnWindowLoaded(object o, EventArgs args)
        {
            GL.ClearColor(0.0666f, 0f, 0f, 0f);

            someShaderProgram = ShaderManager.Instance.ReadShaderProgramFromFiles(new string[]
            {
                "default.frag",
                "default.vert"
            });
            someShaderProgram.Link();
        }
        protected override void OnResize(EventArgs args)
        {
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            time += e.Time;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            someShaderProgram.Use();


            Vector4 position;
            position.X = (float)Math.Sin(time) * 0.5f;
            position.Y = (float)Math.Cos(time) * 0.5f;
            position.Z = 0.0f;
            position.W = 1.0f;
            GL.VertexAttrib4(1, position);

            GL.DrawArrays(PrimitiveType.Points, 0, 1);
            GL.PointSize(10.0f);

            SwapBuffers();
        }


    }
}
