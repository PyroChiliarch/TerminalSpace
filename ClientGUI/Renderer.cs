using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

using ClientGUI.Helpers.Graphics;

namespace ClientGUI
{
    class Renderer
    {


        List<int> shaders = new List<int>();
        List<renderObject> renderList = new List<renderObject>();
        int defaultShader;


        //=============================================================================
        //Constructors
        //=============================================================================

        public Renderer ()
        {

        }







        //=============================================================================
        //Methods
        //=============================================================================


        internal void Load()
        {
            //=============================================================================
            //Setup Clearing
            GL.ClearColor(0, 1, 0, 0);

            //Setup Depth
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GL.DepthFunc(DepthFunction.Less);
            GL.DepthRange(0.0, 1.0);
            GL.ClearDepth(1.0);

            Console.WriteLine("Setup Clearing Functions");



            //=============================================================================
            //Load Shaders
            shaders.Add(Shaders.LoadShader(ShaderType.VertexShader, "Shaders/Standard.vert"));
            shaders.Add(Shaders.LoadShader(ShaderType.FragmentShader, "Shaders/Standard.frag"));

            defaultShader = Shaders.CreateProgram(shaders);

            Console.WriteLine("Shaders Loaded - Num: " + shaders.Count.ToString());


        }







        internal void RenderFrame(TerminalSpaceWindow window)
        {

            //Clear the screen
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(defaultShader);



            //Display the drawn image
            window.SwapBuffers();
        }

        
    }
}
