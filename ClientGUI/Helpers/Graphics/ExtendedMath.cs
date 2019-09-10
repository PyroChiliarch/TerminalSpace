using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace ClientGUI.Helpers.Graphics
{
    public static class ExtendedMath
    {



        //=============================================================================
        //Quaternion Methods
        //=============================================================================




        /// <summary>
        /// Rotates a vector using a quaternion
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="quat"></param>
        /// <returns></returns>
        public static Vector3 RotateVecByQuat(Vector3 vec, Quaternion quat)
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




        /// <summary>
        /// Converts a quaternion to an AxisAngle\n
        /// (x, y, z, angle)\n
        /// Angle is returned in radians!
        /// </summary>
        /// <param name="quat"></param>
        /// <returns></returns>
        public static Vector4 QuatToAxisAngle(Quaternion quat)
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



    }
}
