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


        //Links multiple shaders together into a shader pipeline program
        public static int CreateProgram(List<int> shaderList)
        {
            //Initialise the new program
            int program = GL.CreateProgram();

            //Attach all the shaders to the new program
            foreach (int shader in shaderList)
            {
                GL.AttachShader(program, shader);
            }

            //Finish making the program
            GL.LinkProgram(program);




            //Check for errors
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int status);
            if (status == 0)
            {
                GL.GetProgram(program, GetProgramParameterName.InfoLogLength, out _);

                GL.GetProgramInfoLog(program, out string strInfoLog);
                Console.WriteLine("Shader Linker failure: " + strInfoLog, "Error");
                throw new Exception("Shader Linker failure: " + strInfoLog);
            }


            //Detach the shaders from the now complied program
            foreach (int shader in shaderList)
            {
                GL.DetachShader(program, shader);
            }

            //Return the ready to use program
            return program;
        }








    }
}
