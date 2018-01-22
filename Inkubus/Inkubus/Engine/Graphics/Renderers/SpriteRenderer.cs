using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Inkubus.Engine.GameObjects;

namespace Inkubus.Engine.Graphics.Renderers
{

    using Shaders;

    class SpriteRenderer : Renderer
    {

        protected static Mesh quadMesh;
        protected Sprite sprite;

        protected int currentFrame;
        protected int currentAngle;

        private float animationTime;

        protected ShaderProgram shader;

        static SpriteRenderer()
        {
            quadMesh = new Mesh(Primitives.quad, PrimitiveType.TriangleStrip);
        }

        public void SetFacingAngle(float rad)
        {
            currentAngle = ((int)(rad / Mathf.PI / 2f * sprite.Angles) + 4) % sprite.Angles;
            while (currentAngle < 0)
                currentAngle += sprite.Angles;
            InkubusCore.Instance.Title = "Facing: " + currentAngle + " angle: " + rad;
        }

        public virtual void Animate(Vector2 dir)
        {
            if (dir.X != 0.0f || dir.Y != 0.0f)
            {
                SetFacingAngle((float)Math.Atan2(dir.Y, -dir.X));
                animationTime += InkubusCore.deltaTime;
                currentFrame = sprite.GetFrameByTime(animationTime);
            }

            
        }

        public void SetFacingAngle(Vector2 dir)
        {
            if (dir.X != 0.0f || dir.Y != 0.0f)
                SetFacingAngle((float)Math.Atan2(dir.Y, -dir.X));
        }


        public SpriteRenderer(Sprite sprite, ShaderProgram shader)
        {
            this.sprite = sprite;
            this.shader = shader;
            scale = sprite.Size3;
        }



        public override void Render(Actor actor)
        {

            


            //rotation *= r1;
            Vector2 pos = actor.GetPosition();


            Matrix4 modelView = Matrix4.CreateRotationZ(actor.Angle) * Matrix4.CreateScale(scale) * Matrix4.CreateTranslation(pos.X, pos.Y, 0f);
            GL.UniformMatrix4(21, false, ref modelView);

            shader.Use();

            sprite.Bind(currentFrame, currentAngle);


            quadMesh.Bind();
            quadMesh.Render();

        }

        public override void Dispose()
        {
            base.Dispose();
            sprite.Dispose();
        }
    }
}
