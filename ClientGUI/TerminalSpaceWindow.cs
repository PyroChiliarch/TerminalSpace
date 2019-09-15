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

        //List of all objects to be drawn
        Dictionary<uint, RenderObject> objectList = new Dictionary<uint, RenderObject>();


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

            Renderer.RenderFrame(this, objectList);
            
            


            
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            
            //TODO: Bug
            //Can cause out of range exception when resizing
            //Not repeatable
            //Title = RenderFrequency.ToString().Substring(0, 5);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);


            Renderer.Resize(Height, Width);
            

        }

         
        protected void NetworkReceivedMsg (string message)
        {
            Console.WriteLine("Received " + message);

            String[] msg = message.Split(':');

            
            if (msg[0] == "UPD")
            {


                if (msg[1] == "new")
                {
                    string[] pos = msg[3].Split(',');

                    Vector3 vec = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));

                    if (!objectList.ContainsKey(uint.Parse(msg[2])))
                    {
                        objectList.Add(uint.Parse(msg[2]), Renderer.CreateRenderObject(vec, "Cube", "Cube"));
                    }
                    
                }

                if (msg[1] == "mov")
                {
                    string[] pos = msg[3].Split(',');
                    Vector3 vec = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));

                    objectList[uint.Parse(msg[2])].Transform.position = vec;
                }

                if (msg[1] == "rem")
                {
                    objectList.Remove(uint.Parse(msg[2]));
                }


                if (msg[1] == "clr")
                {
                    objectList = new Dictionary<uint, RenderObject>();
                }

            }

            
        }


    }
}
