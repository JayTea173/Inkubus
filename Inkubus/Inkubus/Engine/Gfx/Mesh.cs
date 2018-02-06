using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Inkubus.Engine.Gfx
{
    class Mesh : IDisposable
    {
        protected Vertex[] verts;

        private readonly int vertexArray;
        private readonly int vertexBuffer;
        private readonly int vertexCount;

        protected PrimitiveType primitiveType;

        

        public Mesh(Vertex[] verts, PrimitiveType primitiveType)
        {
            this.verts = verts;
            vertexCount = verts.Length;

            vertexArray = GL.GenVertexArray();
            vertexBuffer = GL.GenBuffer();

            GL.BindVertexArray(vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);

            // create first buffer: vertex
            GL.NamedBufferStorage(
                vertexBuffer,
                Vertex.Size * vertexCount,        // the size needed by this buffer
                verts,                           // data to initialize with
                BufferStorageFlags.MapWriteBit);    // at this point we will only write to the buffer


            GL.VertexArrayAttribBinding(vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 0);
            GL.VertexArrayAttribFormat(
                vertexArray,
                0,                      // attribute index, from the shader location = 0
                4,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                0);                     // relative offset, first item


            GL.VertexArrayAttribBinding(vertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 1);
            GL.VertexArrayAttribFormat(
                vertexArray,
                1,                      // attribute index, from the shader location = 1
                2,                      // size of attribute, vec2
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                16);                     // relative offset after a vec4

            // link the vertex array and buffer and provide the stride as size of Vertex
            GL.VertexArrayVertexBuffer(vertexArray, 0, vertexBuffer, IntPtr.Zero, Vertex.Size);

            this.primitiveType = primitiveType;
        }




        public void Render()
        {
            GL.DrawArrays(primitiveType, 0, vertexCount);
        }

        public void Bind()
        {
            GL.BindVertexArray(vertexArray);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                GL.DeleteVertexArray(vertexArray);
                GL.DeleteBuffer(vertexBuffer);
            }
        }
    }  
}
