﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Inkubus.Engine.Gfx.Shaders
{
    class ShaderProgram
    {
        protected int id;
        public string name;

        public List<Shader> shaders;

        public ShaderProgram(string name, Shader[] shaders)
        {
            this.name = name;
            this.shaders = new List<Shader>(shaders);
            id = GL.CreateProgram();
            foreach (var shader in shaders)
                GL.AttachShader(id, shader.ID);
        }

        public void Destroy()
        {
            GL.DeleteProgram(id);
        }

        public int Link()
        {
            GL.LinkProgram(id);

            string errLog = GL.GetProgramInfoLog(id);
            if (!string.IsNullOrWhiteSpace(errLog))
                Debug.WriteLine("GL.LinkProgram had info log: " + errLog);   
            

            foreach (var shader in shaders)
                GL.DetachShader(id, shader.ID);
            return id;
        }

        public void Use()
        {
            GL.UseProgram(id);
        }
    }
}
