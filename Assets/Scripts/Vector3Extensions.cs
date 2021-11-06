using System;
using UnityEngine;

public static class Vector3Extensions {
    public static Vector3 NormalizedVector(Vector3 v) {
        float length = v.GetLength();
        return v.DivideByScalar(length);
    }

    public static Vector3 DivideByScalar(this Vector3 v, float scalar) {
        return new Vector3(v.x / scalar, v.y / scalar, v.z / scalar);
    }

    public static float GetLength(this Vector3 v) {
        return MathF.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
    }
}
