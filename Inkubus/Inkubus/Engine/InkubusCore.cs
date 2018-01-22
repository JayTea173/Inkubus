using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Inkubus
{
    using Engine.Input;
    using Engine.Input.Controllers;
    using Engine.GameObjects;
    using Engine.Graphics;
    using Engine.Graphics.Shaders;
    using Engine.Graphics.Renderers;


    class InkubusCore : GameWindow
    {
        public const string versionString = "Alpha 0.0.0";
        public const string buildString = "180106";

        protected ShaderProgram someShaderProgram;



        double time = 0.0d;

        protected Camera camera;
        protected InputManager inputManager;
        protected GameRenderer renderer;

        protected Character infector;

        protected CharacterController controller;


        public static float deltaTime = 0.0f;
        public static double dDeltaTime = 0.0d;

        public static InkubusCore Instance;

        public InkubusCore(int x, int y, int width, int height, GraphicsMode mode, GameWindowFlags flags, DisplayDevice device) : base(width, height, mode, "Inkubus ~~" + versionString, flags, device, 4, 4, GraphicsContextFlags.ForwardCompatible)
        {
            Instance = this;
            //WindowBorder = WindowBorder.Hidden;
            Load += OnWindowLoaded;
            Location = new Point(x, y);
            Closed += OnClosed;

            inputManager = new InputManager();
            Run(1.0d / 60.0d);




        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            
            Exit();
        }

        public override void Exit()
        {
            infector.Dispose();
            ShaderManager.Instance.Destroy();

            base.Exit();
        }


        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            inputManager.OnKeyDown(e);
            

        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);
            inputManager.OnKeyUp(e);
        }

        void OnWindowLoaded(object o, EventArgs args)
        {
            Debug.WriteLine("OpenGL Version: " + GL.GetString(StringName.Version));

            this.VSync = VSyncMode.On;

            camera = new Camera(Width, Height, 1f);

            GL.ClearColor(0.0666f, 0f, 0f, 0f);


            /*sprite = new Sprite("Infector_Walk.png", 64, 64, 15);
            spriteRenderer = new SpriteRenderer(sprite);
            actor = new Actor();*/

            ShaderManager.Instance.ReadShaderProgramFromFiles(new string[]
            {
                "default.frag",
                "default.vert"
            });

            infector = new Character(new Sprite("Infector_Walk.png", 64, 64, 15), 0);

            controller = new CharacterController(infector);
            controller.RegisterEventHandlers(inputManager);

            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.Enable(EnableCap.PolygonOffsetFill);
            GL.Enable(EnableCap.Blend);
            GL.DepthMask(false);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
            GL.PointSize(3);

            GL.Disable(EnableCap.CullFace);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.SrcAlpha);

        }
        protected override void OnResize(EventArgs args)
        {
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            deltaTime = (float)e.Time;
            dDeltaTime = e.Time;
            time += e.Time;

            inputManager.DigestAll();


            infector.Update();
            

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

           

            camera.Bind();

            infector.Render();
           

            
            //sprite.Rotate(deltaTime * 10.0f);
            //sprite.Rotate(deltaTime * 90f);



            SwapBuffers();
        }


    }
}
