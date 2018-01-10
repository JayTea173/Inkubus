using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
//using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics.OpenGL;

namespace Inkubus.Engine.Graphics
{
    class Mesh
    {
        protected Vector4[] verts;
        protected Vector4[] normals;

        private int vertexArray;
        private int vertexBuffer;

        public Mesh()
        {

        }

        public void SetVertices(Vector4[] vertices)
        {
            verts = vertices;
        }

        public void SetNormals(Vector4[] normals)
        {
            this.normals = normals;
        }

        public void Build()
        {
            vertexArray = GL.GenVertexArray();
            vertexBuffer = GL.GenBuffer();

            GL.BindVertexArray(vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);

            
            GL.NamedBufferStorage(vertexBuffer, 4*4, verts, BufferStorageFlags.MapWriteBit);
            GL.VertexArrayAttribBinding(vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 0);
            GL.VertexArrayAttribFormat(vertexArray, 0, 4, VertexAttribType.Float, false, 0);
        }


    }

    
}
