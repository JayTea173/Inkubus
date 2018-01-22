using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Inkubus.Engine.Graphics.Shaders
{
    class ShaderManager
    {
        public ShaderManager() { programs = new List<ShaderProgram>(); }
        
        private readonly static ShaderManager instance = new ShaderManager();

        public static ShaderManager Instance
        {
            get { return instance; }
        }

        public void Destroy()
        {
            foreach (var program in programs)
                program.Destroy();
        }

        protected List<ShaderProgram> programs;

        ///<summary>
        ///will search for shaderFileName within ../data/shaders/
        ///</summary>
        public Shader ReadFromFile(string shaderFileName)
        {
            if (shaderFileName.EndsWith(".vert"))
                return new Shader("../data/shaders/" + shaderFileName, ShaderType.VertexShader);
            else if (shaderFileName.EndsWith(".frag"))
                return new Shader("../data/shaders/" + shaderFileName, ShaderType.FragmentShader);
            else
                return null;
        }

        public ShaderProgram ReadShaderProgramFromFiles(string[] shaderFiles, bool link = true)
        {
            List<Shader> shaders = new List<Shader>();
            foreach (var shaderFile in shaderFiles)
                shaders.Add(ReadFromFile(shaderFile));

            ShaderProgram program = new ShaderProgram("default", shaders.ToArray());
            programs.Add(program);

            if (link)
                program.Link();

            return program;
        }

        public ShaderProgram GetShaderProgramById(int id)
        {
            return programs[id];
        }



    }
}
