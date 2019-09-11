using OpenTK;
using System;

namespace ClientGUI
{
    internal class RenderTransform
    {
        public Vector3 position;
        public Quaternion rotation;
        public float scale;





        //=============================================================================
        //Constructors
        //=============================================================================

        public RenderTransform (Vector3 pos, Quaternion rot, float sca)
        {
            position = pos;
            rotation = rot;
            scale = sca;
        }

















        //=============================================================================
        //Modification Methods
        //=============================================================================


        public void Translate(Vector3 translation)
        {

            position += Helpers.Graphics.ExtendedMath.RotateVecByQuat(translation, rotation);

        }

        public void RelativeTranslate(OpenTK.Vector3 translation)
        {

            //http://math.stackexchange.com/questions/40164/how-do-you-rotate-a-vector-by-a-unit-quaternion
            //
            //conjugate is the mirror? of a quaternion, and is a quaternion itself
            //a pure quaternion is a point in space, also W=0, so we can turn translation to a pure quat
            //
            //where p = pure quaternion, q* = quaternion conjugate, v = pure quat based on vector, q = quaternion
            //p = q*vq
            //

            position += Helpers.Graphics.ExtendedMath.RotateVecByQuat(translation, rotation);

        }

        public void Scale(float scale)
        {
            throw new NotImplementedException();
        }

        public void Rotate(Quaternion newAngle)
        {
            rotation *= newAngle;
            rotation.Normalize();
        }




















        //=============================================================================
        //Retrieve Info Methods
        //=============================================================================

        public virtual Matrix4 GetModelMatrix()
        {
            Matrix4 modelMatrix = Matrix4.CreateFromQuaternion(rotation) * Matrix4.CreateTranslation(position) * Matrix4.CreateScale(scale);
            return modelMatrix;
        }



    }
}