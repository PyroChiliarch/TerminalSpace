using OpenTK;
using System;

namespace ClientGUI
{
    internal class RenderObject
    {
        


        public int textureID;
        public int meshID;



        public RenderTransform Transform;















        //=============================================================================
        //Constructors
        //=============================================================================


        public RenderObject()
        {
            //Sets everything to nill/nothing
            Transform = new RenderTransform(
                new Vector3(0, 0, 0), 
                new Quaternion(0, 0, 0, 1f), 
                1f);

            textureID = 0;
            meshID = 0;
        }




    }
}