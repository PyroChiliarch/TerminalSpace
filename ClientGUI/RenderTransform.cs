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

            position += RotateVecByQuat(translation, rotation);

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

            position += RotateVecByQuat(translation, rotation);

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
        //Math Methods
        //=============================================================================


        private static Vector3 RotateVecByQuat(Vector3 vec, Quaternion quat)
        {
            //http://math.stackexchange.com/questions/40164/how-do-you-rotate-a-vector-by-a-unit-quaternion
            //
            //conjugate is the mirror? of a quaternion, and is a quaternion itself
            //a pure quaternion is a point in space, also W=0, so we can turn translation to a pure quat
            //
            //where p = pure quaternion, q* = quaternion conjugate, v = pure quat based on vector, q = quaternion
            //p = q*vq
            //


            Quaternion conjugate = quat;
            conjugate.Conjugate();

            Quaternion pureQuat = new Quaternion(vec, 0f);

            Quaternion newVec = conjugate * pureQuat * quat;

            return new Vector3(newVec.X, newVec.Y, newVec.Z);
        }










        //Returns Radians
        private static Vector4 QuatToAxisAngle(Quaternion quat)
        {
            //http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToAngle/


            quat.Normalize();

            double angle = 2 * Math.Acos(quat.W);
            double s = Math.Sqrt(1 - (quat.W * quat.W));

            double x;
            double y;
            double z;

            if (s < 0.001) //If s is almost 0 we dont need to normalize
            {
                x = quat.X;
                y = quat.Y;
                z = quat.Z;
            }
            else
            {
                x = quat.X / s;
                y = quat.Y / s;
                z = quat.Z / s;
            }



            return new Vector4((float)x, (float)y, (float)z, (float)angle);

        }
















        //=============================================================================
        //Retrieve Info Methods
        //=============================================================================

        public virtual Matrix4 GetModelMatrix()
        {
            Matrix4 modelMatrix = Matrix4.CreateFromQuaternion(rotation) * Matrix4.CreateTranslation(position) * Matrix4.CreateScale(scale);
            return modelMatrix;
        }


        public Matrix4 GetCameraModelMatrix()
        {
            //The camera has to move position then rotate, unlike a normal object
            //This is why override
            Matrix4 modelMatrix = Matrix4.CreateTranslation(position) * Matrix4.CreateFromQuaternion(rotation) * Matrix4.CreateScale(scale);
            return modelMatrix;
        }


    }
}