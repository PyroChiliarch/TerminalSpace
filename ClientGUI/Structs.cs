using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using OpenTK;


namespace ClientGUI
{
    
    public static class Structs
    {


        //=============================================================================
        //Raw Data Structs
        //=============================================================================

        /// <summary>
        /// Holds Raw Mesh Data
        /// Veticie pos, Veticie color, vertex UVs, and elementData for draw plus their lengths
        /// </summary>
        public struct MeshData
        {
            public string name;                 //Name of the mesh
            public Vector3[] vertexPosData;     //Vertex Positions
            public Vector4[] vertexColorData;   //Colour of vertex
            public ushort[] vertexIndiceData;   //Triangle Vertex Pointts
            public Vector2[] uvData;            //UV Positions

            public MeshData(string _name, Vector3[] _vertexPosData, Vector4[] _vertexColorData, ushort[] _vertexIndiceData, Vector2[] _uvData)
            {
                name = _name;
                vertexPosData = _vertexPosData;
                vertexColorData = _vertexColorData;
                vertexIndiceData = _vertexIndiceData;
                uvData = _uvData;

            }
        }






        /// <summary>
        /// Holds raw Image Data
        /// its name for referencing in Dictionarys, filePath and The Bitmap itself
        /// </summary>
        public struct TextureData
        {
            public string name;
            public string filePath;
            public Bitmap bitmap;


            public TextureData(string _name, string _filePath, Bitmap _bitmap)
            {
                name = _name;
                filePath = _filePath;
                bitmap = _bitmap;
            }
        }





        //=============================================================================
        //Buffer Mapping Structs
        //=============================================================================


        /// <summary>
        /// Maps a meshName to a buffer on the GPU and the offset and everything needed for drawing
        /// To be used for drawing meshes
        /// This data should be cached inside the model
        /// </summary>
        public struct MeshBufferInfo
        {

            //All the information needed to point to a mesh stored  in the graphics card
            public int VertexArrayID;
            public int bufferID;
            public int BufferOffset;
            public int indexBufferID;
            public int AmountOfIndices;

            //Constructor
            public MeshBufferInfo(int _vertexArrayID, int _bufferID, int _bufferOffset, int _indexBufferID, int _amountOfIndices)
            {
                VertexArrayID = _vertexArrayID;
                bufferID = _bufferID;
                BufferOffset = _bufferOffset;
                indexBufferID = _indexBufferID;
                AmountOfIndices = _amountOfIndices;
            }
        }







        /// <summary>
        /// Maps a textureName to a texture on the GPU
        /// This data should be cached
        /// </summary>
        public struct TextureBufferInfo
        {
            public int TextureID;

            public TextureBufferInfo(int _textureID)
            {
                TextureID = _textureID;
            }
        }









        


        
    }
}
