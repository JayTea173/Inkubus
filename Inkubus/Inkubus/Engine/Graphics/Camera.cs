using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Inkubus.Engine.Graphics
{
    class Camera
    {
        Matrix4 projectionMatrix;
        public Camera(int width, int height, float pixelUpscaling)
        {
            var aspectRatio = (float)width / height;
            /*projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                60.0f * ((float)Math.PI / 180f), // field of view angle, in radians
                aspectRatio,                // current window aspect ratio
                0.1f,                       // near plane
                4000f);                     // far plane*/

            projectionMatrix = Matrix4.CreateOrthographic(width/ pixelUpscaling, -height/ pixelUpscaling, -32f, 1000.0f);
           // projectionMatrix = Matrix4.CreateOrthographic(1, -1, -32.0f, 1000.0f);
        }

        public void Bind()
        {
            GL.UniformMatrix4(20, false, ref projectionMatrix);
        }
    }
}
