using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.IO;

namespace ClientGUI.Helpers.Graphics
{
    static class Shaders
    {



        public static int LoadShader(ShaderType eShaderType, string strShaderFilename)
        {
            if (!File.Exists(strShaderFilename))
            {
                Console.WriteLine("Could not find the file " + strShaderFilename, "Error");
                throw new Exception("Could not find the file " + strShaderFilename);
            }

            int shader = GL.CreateShader(eShaderType);
            using (var streamReader = new StreamReader(strShaderFilename))
            {
                GL.ShaderSource(shader, streamReader.ReadToEnd());
            }

            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out int status);
            if (status == 0)
            {
                GL.GetShader(shader, ShaderParameter.InfoLogLength, out _);

                GL.GetShaderInfoLog(shader, out string strInfoLog);

                string strShaderType;
                switch (eShaderType)
                {
                    case ShaderType.FragmentShader:
                        strShaderType = "fragment";
                        break;
                    case ShaderType.VertexShader:
                        strShaderType = "vertex";
                        break;
                    case ShaderType.GeometryShader:
                        strShaderType = "geometry";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("eShaderType");
                }
                Console.WriteLine("Compile failure in " + strShaderType + " shader:\n" + strInfoLog, "Error");
                throw new Exception("Compile failure in " + strShaderType + " shader:\n" + strInfoLog);
            }

            return shader;
        }










        public static int CreateProgram(List<int> shaderList)
        {
            int program = GL.CreateProgram();

            foreach (int shader in shaderList)
            {
                GL.AttachShader(program, shader);
            }

            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int status);
            if (status == 0)
            {
                GL.GetProgram(program, GetProgramParameterName.InfoLogLength, out _);

                GL.GetProgramInfoLog(program, out string strInfoLog);
                Console.WriteLine("Shader Linker failure: " + strInfoLog, "Error");
                throw new Exception("Shader Linker failure: " + strInfoLog);
            }

            foreach (int shader in shaderList)
            {
                GL.DetachShader(program, shader);
            }

            return program;
        }








    }
}
