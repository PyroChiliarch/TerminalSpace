using OpenTK;
using System;

namespace ClientGUI
{
    internal class RenderObject
    {
        


        public int textureID;
        public int meshID;



        public RenderTransform transform;

        

        public RenderObject()
        {
            //Sets everything to nill/nothing
            transform.position = new Vector3(0, 0, 0);
            transform.rotation = new Quaternion(0, 0, 0, 1f);
            transform.scale = 1f;
            textureID = 0;
            meshID = 0;
        }



       


        public virtual Matrix4 GetModelMatrix()
        {
            Matrix4 modelMatrix = Matrix4.CreateFromQuaternion(transform.rotation) * Matrix4.CreateTranslation(transform.position) * Matrix4.CreateScale(transform.scale);
            return modelMatrix;
        }

        public void Translate(Vector3 translation)
        {

            transform.position += Helpers.Graphics.ExtendedMath.RotateVecByQuat(translation, transform.rotation);

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

            transform.position += Helpers.Graphics.ExtendedMath.RotateVecByQuat(translation, transform.rotation);

        }

        public void Scale(float scale)
        {
            throw new NotImplementedException();
        }

        public void Rotate(Quaternion newAngle)
        {
            transform.rotation *= newAngle;
            transform.rotation.Normalize();
        }
        
    }
}