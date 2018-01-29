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
    using Engine;
    using Engine.IO;
    using Engine.Input;
    using Engine.Input.Controllers;
    using Engine.GameObjects;
    using Engine.Graphics;
    using Engine.Graphics.Animation;
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

        protected CharacterManager characterManager;

        protected CharacterController controller;

        protected World world;


        public static float deltaTime = 0.0f;
        public static double dDeltaTime = 0.0d;

        public static InkubusCore Instance;

        public InkubusCore(int x, int y, int width, int height, GraphicsMode mode, GameWindowFlags flags, DisplayDevice device) : base(width, height, mode, "Inkubus", flags, device, 4, 4, GraphicsContextFlags.ForwardCompatible)
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
            characterManager.Dispose();
            ShaderManager.Instance.Destroy();

            base.Exit();
        }


        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Escape || (e.Key == Key.F4 && e.Alt))
                Environment.Exit(0);

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

            camera = new Camera(Width, Height, 2f);

            GL.ClearColor(0.0666f, 0f, 0f, 0f);


            /*sprite = new Sprite("Infector_Walk.png", 64, 64, 15);
            spriteRenderer = new SpriteRenderer(sprite);
            actor = new Actor();*/

            ShaderManager.Instance.ReadShaderProgramFromFiles(new string[]
            {
                "default.frag",
                "default.vert"
            });


            characterManager = new CharacterManager();

            Character infector = new Character("Hunter", "Hunter", 0, 192, 192);

            //Setup "infector" character
            //infector.Deserialize("..\\data\\characters\\Hunter\\hunter.dat");

            Blueprint.Read<Character>(ref infector, "..\\data\\characters\\Hunter\\hunter.dat");
            //infector.TurnRate = 270.0f;
            //infector.MovementSpeed = 28.0f;
            var r = infector.GetRenderer();
            r.onAnimationDone += infector.OnAttackAnimationEnd;
            r.Animations.Get(AnimationName.Attack).AddFlag(ActorFlags.CantMove);

            Blueprint.Write<Character>(infector, "..\\data\\characters\\Hunter\\hunter.dat");
            //infector.Serialize("..\\data\\characters\\Hunter\\hunter.dat");
            characterManager.Add(infector);

            

            world = new Engine.GameObjects.World(8, 8);
            world.Fill(new WorldTile(new Engine.Graphics.Sprite("..\\data\\textures\\World\\Dirt02.png", 64f, 64f, 1f), r.Shader));
            

            controller = new CharacterController(infector, camera);
            controller.RegisterEventHandlers(inputManager);

            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
            GL.PointSize(3);

            GL.DepthMask(true);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);
            GL.Disable(EnableCap.CullFace);


            //GL.Disable(EnableCap.CullFace);


            /* GL.Enable(EnableCap.Blend);
             GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
             GL.Enable(EnableCap.DepthTest);
             GL.Enable(EnableCap.CullFace);*/

        }
        protected override void OnResize(EventArgs args)
        {
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = "Inkubus ~~" + versionString + " @" + Width + "x" + Height + " - " + (1f / e.Time).ToString(".0") + " fps";

            deltaTime = (float)e.Time;
            dDeltaTime = e.Time;
            time += e.Time;

            inputManager.DigestAll();

            characterManager.Update();
            

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            controller.Update();
            camera.Bind();

            world.Render();
            characterManager.Render();
           

            
            //sprite.Rotate(deltaTime * 10.0f);
            //sprite.Rotate(deltaTime * 90f);



            SwapBuffers();
        }


    }
}
