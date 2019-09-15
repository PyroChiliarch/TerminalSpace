using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

using ClientGUI.Helpers.Graphics;
using System.Drawing;

namespace ClientGUI
{
    class Renderer
    {

        //List of all the shaders
        readonly List<int> shaders = new List<int>();

        //List for all the shader programs
        readonly Dictionary<string, int> programList = new Dictionary<string, int>();

        

        //List of textures and Meshes
        Dictionary<string, Structs.TextureBufferInfo> textureLocationList;
        Dictionary<string, Structs.MeshBufferInfo> meshLocationList;

        readonly Camera camera = new Camera();

        int modelMatrixUni;
        int viewMatrixUni;
        int projectionMatrixUni;

        Matrix4 identMatrix = Matrix4.Identity;
        
        //int texUni;
        


       
        


        






        //=============================================================================
        //Constructors
        //=============================================================================

        public Renderer ()
        {

        }

        






        internal RenderObject CreateRenderObject(Vector3 pos, string mesh, string tex)
        {
            RenderObject obj = new RenderObject();
            obj.MeshInfo = meshLocationList[mesh];
            obj.TextureInfo = textureLocationList[tex];
            obj.Transform.Translate(pos);
            return obj;
        }






















        //=============================================================================
        //Methods
        //=============================================================================


        internal void Load()
        {
            //=============================================================================
            //Setup Clearing
            GL.ClearColor(0, 1, 0, 0);

            //=============================================================================
            //Setup Depth
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GL.DepthFunc(DepthFunction.Less);
            GL.DepthRange(0.0, 1.0);
            GL.ClearDepth(1.0);

            Console.WriteLine("Setup Clearing Functions");



            //=============================================================================
            //Load Shaders
            shaders.Add(FileOps.LoadShader(ShaderType.VertexShader, "Shaders/Standard.vert"));
            shaders.Add(FileOps.LoadShader(ShaderType.FragmentShader, "Shaders/Standard.frag"));

            programList.Add("default", Shaders.CreateProgram(shaders));

            Console.WriteLine("Shaders Loaded - Num: " + shaders.Count.ToString());
            Console.WriteLine("Programs Loaded - Num: " + programList.Count.ToString());

            //=============================================================================
            //Load Textures
            Structs.TextureData texture = FileOps.LoadTexture("Cube", "Textures/Cube.bmp");
            //TODO Load all Meshes, Textures
            //Auto Load every file in each folder automatically
            //Load texture to GPU
            textureLocationList = LoadTexturesToGPU(new Structs.TextureData[1]
            {
                texture,
            }
            ) ;


            //=============================================================================
            //Load Meshes
            Structs.MeshData mesh = FileOps.LoadMesh("Cube", "Meshes/Cube.obj");
            meshLocationList = LoadMeshesToGPU(new Structs.MeshData[1]
            {
                mesh
            });

            //=============================================================================
            //Setup Camera
            camera.Transform.Translate(new Vector3(0, 0.00f, 0f));
            camera.SetProjection(1f, 0.001f, 20f);

            


            
        }























        internal void RenderFrame(TerminalSpaceWindow window, Dictionary<uint, RenderObject> renderList)
        {

            //=============================================================================
            //Setup before drawing

            //Clear the screen
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Set the ShaderProgram
            GL.UseProgram(programList["default"]);

            //TODO: Abstract away Shader Program selection
            //Should only really need to be called when a new shader program is set
            //Get the IDs for the shader uniforms
            modelMatrixUni = GL.GetUniformLocation(programList["default"], "modelMatrix");
            viewMatrixUni = GL.GetUniformLocation(programList["default"], "viewMatrix");
            projectionMatrixUni = GL.GetUniformLocation(programList["default"], "projectionMatrix");
            //texUni = GL.GetUniformLocation(programList["default"], "tex");
            
            //Update matricies in the shader program
            Matrix4 viewMatrix = camera.GetViewMatrix();
            Matrix4 projectionMatrix = camera.GetProjectionMatrix();
            GL.UniformMatrix4(viewMatrixUni, false, ref viewMatrix);
            GL.UniformMatrix4(projectionMatrixUni, false, ref projectionMatrix);

            foreach (RenderObject renderObject in renderList.Values)
            {

                //Grab object information from arrays
                Matrix4 modelMatrix = renderObject.Transform.GetModelMatrix();
                int texture = renderObject.TextureInfo.TextureID;

                //Update Matrix (Specific to the object)
                GL.UniformMatrix4(modelMatrixUni, false, ref modelMatrix);

                //Set Texture
                GL.ActiveTexture(TextureUnit.Texture0 + 0);
                GL.BindTexture(TextureTarget.Texture2D, texture);

                //Draw Mesh
                GL.BindVertexArray(renderObject.MeshInfo.VertexArrayID);
                GL.DrawElements(
                    (BeginMode)PrimitiveType.Triangles,
                    renderObject.MeshInfo.AmountOfIndices,
                    DrawElementsType.UnsignedShort,
                    renderObject.MeshInfo.BufferOffset);
            }





            //Display the drawn image
            window.SwapBuffers();
        }






        //Called when resizing the gamewindow
        internal void Resize(int height, int width)
        {
            //Correct camera projection
            camera.SetProjectionAspect(1f, 0.001f, 20f, width, height);

            //Correct the drawing viewport
            GL.Viewport(0, 0, width, height);
        }







        //=============================================================================
        //GPU Uploading Methods

        public Dictionary<string, Structs.MeshBufferInfo> LoadMeshesToGPU(Structs.MeshData[] meshData)
        {
            Dictionary<string, Structs.MeshBufferInfo> meshBufferIDs = new Dictionary<string, Structs.MeshBufferInfo>();

            for (int i = 0; i < meshData.Length; i++)
            {
                Console.WriteLine("Mesh Load: " + meshData[i].name);

                //Make buffers
                int vertexArray = GL.GenVertexArray();
                int vertexBuffer = GL.GenBuffer();
                int indexBuffer = GL.GenBuffer();



                //Update Dictionary
                meshBufferIDs.Add(
                    meshData[i].name,
                    new Structs.MeshBufferInfo(
                        vertexArray,
                        vertexBuffer,
                        0,
                        indexBuffer,
                        meshData[i].vertexIndiceData.Length));


                //Fill buffers
                //Bind the VAO
                GL.BindVertexArray(vertexArray);
                {
                    GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
                    {
                        //Misc Variables that will be helpful
                        int vertexPosSize = (Vector3.SizeInBytes * meshData[i].vertexPosData.Length);
                        int vertexColorSize = (Vector4.SizeInBytes * meshData[i].vertexColorData.Length);
                        int vertexUVSize = (Vector2.SizeInBytes * meshData[i].uvData.Length);

                        int vertexBufferSize = (vertexPosSize + vertexColorSize + vertexUVSize);


                        int vertexIndexSize = (2 * meshData[i].vertexIndiceData.Length); //ushort is 2 bytes in size


                        //the vertex buffer will hold bulk information about vertices
                        //pos, color and UV coords

                        //Input Vertex Pos
                        GL.BufferData(
                            BufferTarget.ArrayBuffer,
                            (IntPtr)(vertexBufferSize),
                            meshData[i].vertexPosData,
                            BufferUsageHint.StaticDraw);

                        //Input Color Data
                        GL.BufferSubData(
                            BufferTarget.ArrayBuffer,
                            (IntPtr)(vertexPosSize),
                            (IntPtr)(vertexColorSize),
                            meshData[i].vertexColorData);

                        //Input UV Data
                        GL.BufferSubData(
                            BufferTarget.ArrayBuffer,
                            (IntPtr)(vertexPosSize + vertexColorSize),
                            (IntPtr)(vertexUVSize),
                            meshData[i].uvData);



                        //The element array buffer is used to draw polygons and specifies which vertices to use
                        GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);

                        //Add polygon Indices
                        GL.BufferData(
                            BufferTarget.ElementArrayBuffer,
                            (IntPtr)(vertexIndexSize),
                            meshData[i].vertexIndiceData,
                            BufferUsageHint.StaticDraw);


                        //Now that the Data is in 
                        //Tell GPU how to use it
                        GL.EnableVertexAttribArray(0);
                        GL.EnableVertexAttribArray(1);
                        GL.EnableVertexAttribArray(2);
                        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                        GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 0, vertexPosSize);
                        GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, vertexPosSize + vertexColorSize);
                    }
                }
                GL.BindVertexArray(0);

            }

            //Return the location of the texture that were loaded
            return meshBufferIDs;
        }






















        /// <summary>
        /// Loads its own Textures to the GPU
        /// </summary>
        public Dictionary<string, Structs.TextureBufferInfo> LoadTexturesToGPU(Structs.TextureData[] textureData)
        {
            Dictionary<string, Structs.TextureBufferInfo> textureIDs = new Dictionary<string, Structs.TextureBufferInfo>();

            for (int i = 0; i < textureData.Length; i++)
            {
                //Make space on graphics card for texture
                int textureID = GL.GenTexture();

                //Add the ID to the dictionary
                textureIDs.Add(
                    textureData[i].name,
                    new Structs.TextureBufferInfo(textureID));

                //Pass texture to graphics card
                //Start working with texture
                GL.ActiveTexture(TextureUnit.Texture0 + 0); //? needed?
                GL.BindTexture(TextureTarget.Texture2D, textureID);

                //Set the settings for GPU Texture, Nearest means minecraft like
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Nearest);

                var bmp = textureData[i].bitmap;
                var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                //Pass texture data to GPU texture
                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    textureData[i].bitmap.Width,
                    textureData[i].bitmap.Height,
                    0,
                    PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    data.Scan0);
                bmp.UnlockBits(data);


                //Stop working with texture
                GL.BindTexture(TextureTarget.Texture2D, 0);


                Console.WriteLine("Tex Load: " + textureData[i].name + " At " + textureID);
                //Now the bitmap is on the graphics card and it is in the dictionary for easy reference
            }

            //Return the location of the textures that were loaded
            return textureIDs;
        }












    }
}
