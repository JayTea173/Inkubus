using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Inkubus.Engine.Gfx
{
    class Primitives
    {
        public static Vertex[] quad = new Vertex[]
        {
            /*new Vertex(new Vector4(-0.5f, -0.5f, 0.0f, 1.0f), new Vector2(0.0f, 0.0f)),
            new Vertex(new Vector4(0.5f, -0.5f, 0.0f, 1.0f), new Vector2(1.0f, 0.0f)),
            new Vertex(new Vector4(-0.5f, 0.5f, 0.0f, 1.0f), new Vector2(0.0f, 1.0f)),
            new Vertex(new Vector4(0.5f, 0.5f, 0.0f, 1.0f), new Vector2(1.0f, 1.0f)),*/
            new Vertex(new Vector4(-0.5f, -0.5f, 0.0f, 1.0f), new Vector2(0.0f, 0.0f)),
            new Vertex(new Vector4(0.5f, -0.5f, 0.0f, 1.0f), new Vector2(1.0f, 0.0f)),
            new Vertex(new Vector4(-0.5f, 0.5f, 0.0f, 1.0f), new Vector2(0.0f, 1.0f)),
            new Vertex(new Vector4(0.5f, 0.5f, 0.0f, 1.0f), new Vector2(1.0f, 1.0f)),

        };

        public static Vertex[] CreateSolidCube(float side)
        {
            side = side / 2f; // half side - and other half
            Vertex[] vertices = new Vertex []
            {
               new Vertex(new Vector4(-side, -side, -side, 1.0f),   Vector2.Zero),
               new Vertex(new Vector4(-side, -side, side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(-side, side, -side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(-side, side, -side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(-side, -side, side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(-side, side, side, 1.0f),     Vector2.Zero),

               new Vertex(new Vector4(side, -side, -side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(side, side, -side, 1.0f),     Vector2.Zero),
               new Vertex(new Vector4(side, -side, side, 1.0f),     Vector2.Zero),
               new Vertex(new Vector4(side, -side, side, 1.0f),     Vector2.Zero),
               new Vertex(new Vector4(side, side, -side, 1.0f),     Vector2.Zero),
               new Vertex(new Vector4(side, side, side, 1.0f),      Vector2.Zero),

               new Vertex(new Vector4(-side, -side, -side, 1.0f),   Vector2.Zero),
               new Vertex(new Vector4(side, -side, -side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(-side, -side, side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(-side, -side, side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(side, -side, -side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(side, -side, side, 1.0f),     Vector2.Zero),

               new Vertex(new Vector4(-side, side, -side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(-side, side, side, 1.0f),     Vector2.Zero),
               new Vertex(new Vector4(side, side, -side, 1.0f),     Vector2.Zero),
               new Vertex(new Vector4(side, side, -side, 1.0f),     Vector2.Zero),
               new Vertex(new Vector4(-side, side, side, 1.0f),     Vector2.Zero),
               new Vertex(new Vector4(side, side, side, 1.0f),      Vector2.Zero),

               new Vertex(new Vector4(-side, -side, -side, 1.0f),   Vector2.Zero),
               new Vertex(new Vector4(-side, side, -side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(side, -side, -side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(side, -side, -side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(-side, side, -side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(side, side, -side, 1.0f),     Vector2.Zero),

               new Vertex(new Vector4(-side, -side, side, 1.0f),    Vector2.Zero),
               new Vertex(new Vector4(side, -side, side, 1.0f),     Vector2.Zero),
               new Vertex(new Vector4(-side, side, side, 1.0f),     Vector2.Zero),
               new Vertex(new Vector4(-side, side, side, 1.0f),     Vector2.Zero),
               new Vertex(new Vector4(side, -side, side, 1.0f),     Vector2.Zero),
               new Vertex(new Vector4(side, side, side, 1.0f),      Vector2.Zero),
            };
            return vertices;
        }
    }

}
