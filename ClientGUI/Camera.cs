using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace ClientGUI
{
    /// <summary>
    /// Source of the players view in the window
    /// </summary>
    class Camera : RenderObject
    {
        public Matrix4 projectionMatrix;



        //=============================================================================
        //Constructors
        //=============================================================================

        public Camera()
        {
            SetProjection(1f, 0.001f, 20f);
        }

        public Camera(float zoom, float clipNear, float clipFar)
        {
            SetProjection(zoom, clipNear, clipFar);
        }





        //=============================================================================
        //Modification Methods
        //=============================================================================


        public void SetProjection(float newZoom, float newClipPlaneNear, float newClipPlaneFar)
        {

            projectionMatrix = new Matrix4();
            projectionMatrix = Matrix4.Identity;

            projectionMatrix.M11 = newZoom;
            projectionMatrix.M22 = newZoom;
            projectionMatrix.M33 = (newClipPlaneFar + newClipPlaneNear) / (newClipPlaneNear - newClipPlaneFar);
            projectionMatrix.M43 = (2 * newClipPlaneFar * newClipPlaneNear) / (newClipPlaneNear - newClipPlaneFar);
            projectionMatrix.M34 = 1f;

        }


        public void SetProjectionAspect(float newZoom, float newClipPlaneNear, float newClipPlaneFar, int width, int height)
        {

            projectionMatrix = new Matrix4();
            projectionMatrix = Matrix4.Identity;

            projectionMatrix.M11 = newZoom / (width / (float)height);
            projectionMatrix.M22 = newZoom;
            projectionMatrix.M33 = (newClipPlaneFar + newClipPlaneNear) / (newClipPlaneNear - newClipPlaneFar);
            projectionMatrix.M43 = (2 * newClipPlaneFar * newClipPlaneNear) / (newClipPlaneNear - newClipPlaneFar);
            projectionMatrix.M34 = 1f;

            

        }


        //=============================================================================
        //Matrix Methods
        //=============================================================================


        //Returns Get Model Matrix
        public Matrix4 GetViewMatrix()
        {
            return Transform.GetCameraModelMatrix();
        }




        




        public Matrix4 GetProjectionMatrix()
        {
            return projectionMatrix;
        }


    }
}
