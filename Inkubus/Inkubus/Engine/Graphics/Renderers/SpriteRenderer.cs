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
    using Animation;

    class SpriteRenderer : Renderer
    {

        protected static Mesh quadMesh;
        protected Sprite sprite;

        protected int currentFrame;
        protected int currentAngle;

        private float animationTime;

        protected ShaderProgram shader;

        protected SpriteAnimationList animations;

        public event AnimationEventFunction onAnimationDone;

        public SpriteAnimationList Animations
        {
            get
            {
                return animations;
            }
        }

        protected SpriteAnimation currentAnimation;

        public SpriteAnimation CurrentAnimation
        {
            get
            {
                return currentAnimation;
            }
            set
            {
                if (currentAnimation != value)
                {
                    currentAnimation = value;

                    SetSprite(value.spriteSheetVariants[0]);
                    sprite.SetLooping(currentAnimation.loops);
                }
            }
        }


        static SpriteRenderer()
        {
            quadMesh = new Mesh(Primitives.quad, PrimitiveType.TriangleStrip);
        }

        public void SetFacingAngle(float rad)
        {
            currentAngle = ((int)(rad / Mathf.PI / 2f * sprite.Angles) + 4) % sprite.Angles;
            while (currentAngle < 0)
                currentAngle += sprite.Angles;

        }

        public virtual void Animate(Vector2 faceDir, Vector2 moveDir)
        {
            if (CurrentAnimation.name == AnimationName.Attack)
            {
                if (AdvanceAnimation())
                {
                    var args = new AnimationEventArgs();
                    args.renderer = this;
                    onAnimationDone.Invoke(args);
                }

            }

            if (currentAnimation.name != AnimationName.Attack)
            { //when not stuck in attack animation
                if (moveDir != Vector2.Zero)
                    CurrentAnimation = animations.Get(AnimationName.Walk);
                else
                    CurrentAnimation = animations.Get(AnimationName.Idle);

                if (sprite == null)
                    return;


                if (faceDir.X != 0.0f || faceDir.Y != 0.0f)
                {
                    SetFacingAngle((float)Math.Atan2(faceDir.Y, -faceDir.X));
                    AdvanceAnimation();
                    //InkubusCore.Instance.Title = "facing: " + currentAngle + " frame: " + currentFrame + ", dir: " + dir.X.ToString("0.00") + ", " + dir.Y.ToString("0.00");
                }
            }


        }

        protected bool AdvanceAnimation()
        {
            animationTime += InkubusCore.deltaTime;
            currentFrame = CurrentAnimation.GetFrameByTime(animationTime);
            if (!CurrentAnimation.loops)
                if (CurrentAnimation.IsDone(currentFrame))
                {
                    return true;
                }

            return false;
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
            SetAnimation(AnimationName.Idle);
        }

        public SpriteRenderer(ShaderProgram shader, string textureDir, int spriteSizeX, int spriteSizeY)
        {
            this.shader = shader;
            animations = new SpriteAnimationList(textureDir, spriteSizeX, spriteSizeY);
            SetAnimation(AnimationName.Idle);
        }

        public void SetSprite(Sprite _sprite, bool _loopAnimation = true)
        {
            sprite = _sprite;
            scale = sprite.Size3;
            sprite.SetLooping(_loopAnimation);
            currentFrame = CurrentAnimation.GetFrameByTime(animationTime);

        }

        public void SetAnimation(AnimationName animationName, bool resetAnimationToStart = false)
        {
            if (resetAnimationToStart)
                animationTime = 0f;
            CurrentAnimation = animations.Get(animationName);

        }


        public override void Render(Actor actor)
        {
            if (sprite == null)
                return;



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
            if (sprite != null)
                sprite.Dispose();
        }
    }
}
