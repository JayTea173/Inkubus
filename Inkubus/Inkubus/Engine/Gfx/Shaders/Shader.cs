using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Inkubus.Engine.Gfx.Shaders
{
    class Shader
    {
        protected int id;

        public int ID { get { return id; } }
        public Shader (string fileName, ShaderType shaderType)
        {
            string data = File.ReadAllText(fileName);
            id = GL.CreateShader(shaderType);
            GL.ShaderSource(id, data);
            GL.CompileShader(id);
            string errLog = GL.GetShaderInfoLog(id);
            if (!string.IsNullOrWhiteSpace(errLog))
            {
                Debug.WriteLine("GL.CompileShader [" + shaderType.ToString() + "] had info log: " + errLog);
            }
        }

    }
}
