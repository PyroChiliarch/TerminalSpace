using OpenTK;
using System;

namespace ClientGUI
{
    internal class RenderObject
    {

        public RenderTransform Transform;

        public Structs.MeshBufferInfo MeshInfo;
        public Structs.TextureBufferInfo TextureInfo;



        















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

            
        }




    }
}