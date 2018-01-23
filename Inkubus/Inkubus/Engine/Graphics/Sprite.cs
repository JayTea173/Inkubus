using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Inkubus.Engine.Graphics
{
    class Sprite : IDisposable { 

        protected Texture texture;



        protected Matrix4 rotation;
        protected Vector3 position;


        

        protected Vector2 size;

        public Vector2 Size
        {
            get
            {
                return size;
            }
        }

        public Vector3 Size3
        {
            get
            {
                return new Vector3(size.X, size.Y, 1.0f);
            }
        }

        protected int frames;

        public int Frames
        {
            get
            {
                return frames;
            }
        }

        protected int angles;

        public int Angles
        {
            get
            {
                return angles;
            }
        }

        protected float fps;

        public float FPS
        {
            get
            {
                return fps;
            }
        }

        protected bool loopAnimation = true;

        public Sprite(string textureFileName, float sizeX, float sizeY, float fps)
        {
            texture = new Texture(textureFileName);
            rotation = Matrix4.Identity;
            position = Vector3.Zero;

            size = new Vector2(sizeX, sizeY);
            frames = texture.Width / (int)sizeX;
            angles = texture.Height / (int)sizeY;
            this.fps = fps;
        }



        public void Rotate(float angle)
        {
            rotation *= Matrix4.CreateRotationZ(angle * Mathf.ToDeg);
        }

        public void RotateTo(float angle)
        {
            rotation = Matrix4.CreateRotationZ(angle * Mathf.ToDeg);
        }

        public void Translate(Vector3 translation)
        {
            position += translation;
        }

        public void Translate(float x, float y, float z)
        {
            position.X += x;
            position.Y += y;
            position.Z += z;
        }

        public void SetPosition(Vector3 translation)
        {
            position = translation;
        }

        public void SetPosition(float x, float y, float z)
        {
            position.X = x;
            position.Y = y;
            position.Z = z;
        }

        public void Dispose()
        {
            texture.Dispose();
        }

        public void Bind(int currentFrame, int currentAngle)
        {
            texture.Bind();
            GL.Uniform2(22, size);
            GL.Uniform1(23, currentFrame);
            GL.Uniform1(24, currentAngle);
        }

        public void SetLooping(bool _loopAnimation)
        {
            loopAnimation = _loopAnimation;
        }
    }
}
