using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAUnityLib
{
    private static Quaternion bufQuat = new Quaternion(0f, 0f, 0f, 0f);

    public static Vector3 RotateVector3(Vector3 vec, Quaternion quat)
    {
        bufQuat.x = vec.x;
        bufQuat.y = vec.y;
        bufQuat.z = vec.z;

        bufQuat = quat * bufQuat * Quaternion.Inverse(quat);

        return new Vector3(bufQuat.x, bufQuat.y, bufQuat.z);
    }
}