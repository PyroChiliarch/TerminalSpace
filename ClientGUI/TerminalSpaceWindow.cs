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

        readonly Renderer Renderer = new Renderer();
        readonly InputHandler InputHandler = new InputHandler();
        readonly NetConnection NetConnection = new NetConnection();

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
            NetConnection.MessageReceived += NetworkReceivedMsg;
            NetConnection.Connect("127.0.0.1", 8);
            NetConnection.Login("PyroGUI", "1234");

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            Renderer.RenderFrame(this);
            
            


            
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            
            //TODO: Bug
            //Can cause out of range exception when resizing
            //Not repeatable
            Title = RenderFrequency.ToString().Substring(0, 5);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);


            Renderer.Resize(Height, Width);
            

        }

         
        protected void NetworkReceivedMsg (string message)
        {
            Console.WriteLine("Received " + message);
        }


    }
}
