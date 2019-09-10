using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace ClientGUI
{
    class Renderer
    {




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
            GL.ClearColor(0, 1, 0, 0);
        }







        internal void RenderFrame(TerminalSpaceWindow window)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);




            window.SwapBuffers();
        }

        
    }
}
