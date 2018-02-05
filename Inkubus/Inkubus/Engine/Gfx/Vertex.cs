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
    public struct Vertex
    {
        public const int Size = (4 + 2) * 4;


        private readonly Vector4 position;
        private readonly Vector2 uv;

        public Vertex(Vector4 _position, Vector2 _uv)
        {
            position = _position;
            uv = _uv;
        }
    }
}
