using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

using System.Drawing;

namespace ClientGUI
{
    class TerminalSpaceWindow : GameWindow
    {

        Renderer Renderer = new Renderer();
        InputHandler InputHandler = new InputHandler();


        //=============================================================================
        //Constructors
        //=============================================================================

        public TerminalSpaceWindow (int width, int height) : base (width, height)
        {
           
        }










        //=============================================================================
        //Overrides
        //=============================================================================

        protected override void OnLoad(EventArgs arg)
        {
            base.OnLoad(arg);

            Renderer.Load();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            Renderer.RenderFrame(this);
            
            


            
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
               
            Title = RenderFrequency.ToString().Substring(0, 5);
        }


        




    }
}
