using UnityEngine;

float ConvertDegreesToRadians(float degrees) {
    return ((float)Math.PI / (float)180) * degrees;
}

public static class Matrix4x4Extensions {
    public static Matrix4x4 GetTranslationMatrix(Vector3 position) {
        return new Matrix4x4(new Vector4(1, 0, 0, 0),
                             new Vector4(0, 1, 0, 0),
                             new Vector4(0, 0, 1, 0),
                             new Vector4(position.x, position.y, position.z, 1));
    }

    public static Matrix4x4 GetScaleMatrix(Vector3 scale) {
        return new Matrix4x4(new Vector4(scale.x, 0, 0, 0),
                             new Vector4(0, scale.y, 0, 0),
                             new Vector4(0, 0, scale.z, 0),
                             new Vector4(0, 0, 0, 1));
    }

    public static Matrix4x4 GetRotationMatrix(Vector3 rotation) {
        rotation = new Vector3(ConvertDegreesToRadians(rotation.x), ConvertDegreesToRadians(rotation.y), ConvertDegreesToRadians(rotation.z));

        float cosX = (float)Mathf.Cos(rotation.x);
        float sinX = (float)Mathf.Sin(rotation.x);
        float cosY = (float)Mathf.Cos(rotation.y);
        float sinY = (float)Mathf.Sin(rotation.y);
        float cosZ = (float)Mathf.Cos(rotation.z);
        float sinZ = (float)Mathf.Sin(rotation.z);

        Matrix4x4 rotationMatrixX = new Matrix4x4(new Vector4(1, 0, 0, 0),
                                                  new Vector4(0, cosX, sinX, 0),
                                                  new Vector4(0, -sinX, cosX, 0),
                                                  new Vector4(0, 0, 0, 1));

        Matrix4x4 rotationMatrixY = new Matrix4x4(new Vector4(cosY, 0, -sinY, 0),
                                                  new Vector4(0, 1, 0, 0),
                                                  new Vector4(sinY, 0, cosY, 0),
                                                  new Vector4(0, 0, 0, 1));

        Matrix4x4 rotationMatrixZ = new Matrix4x4(new Vector4(cosZ, sinZ, 0, 0),
                                                  new Vector4(-sinZ, cosZ, 0, 0),
                                                  new Vector4(0, 0, 1, 0),
                                                  new Vector4(0, 0, 0, 1));

        return rotationMatrixX.Multiply(rotationMatrixY, rotationMatrixZ);
    }

    public static Matrix4x4 Get_TRS_Matrix(Vector3 position, Vector3 rotation, Vector3 scale) {
        return GetTranslationMatrix(position).Multiply(GetRotationMatrix(rotation), GetScaleMatrix(scale));
    }

    public static Vector3 ExtractPosition(this Matrix4x4 matrix) {
        Vector3 position;
        position.x = matrix.m03;
        position.y = matrix.m13;
        position.z = matrix.m23;
        return position;
    }

    public static Quaternion ExtractRotation(this Matrix4x4 m) {
        Quaternion q = new Quaternion();
        q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m.m00 + m.m11 + m.m22)) / 2;
        q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m.m00 - m.m11 - m.m22)) / 2;
        q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m.m00 + m.m11 - m.m22)) / 2;
        q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m.m00 - m.m11 + m.m22)) / 2;
        q.x *= Mathf.Sign(q.x * (m.m21 - m.m12));
        q.y *= Mathf.Sign(q.y * (m.m02 - m.m20));
        q.z *= Mathf.Sign(q.z * (m.m10 - m.m01));
        return q;
    }

    public static Vector3 ExtractScale(this Matrix4x4 matrix) {
        Vector3 scale;
        scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
        scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
        scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
        return scale;
    }

    public static Matrix4x4 Multiply(this Matrix4x4 matrix, Matrix4x4 other) {
        Matrix4x4 result;
        result.m00 = matrix.m00 * other.m00 + matrix.m01 * other.m10 + matrix.m02 * other.m20 + matrix.m03 * other.m30;
        result.m01 = matrix.m00 * other.m01 + matrix.m01 * other.m11 + matrix.m02 * other.m21 + matrix.m03 * other.m31;
        result.m02 = matrix.m00 * other.m02 + matrix.m01 * other.m12 + matrix.m02 * other.m22 + matrix.m03 * other.m32;
        result.m03 = matrix.m00 * other.m03 + matrix.m01 * other.m13 + matrix.m02 * other.m23 + matrix.m03 * other.m33;
        result.m10 = matrix.m10 * other.m00 + matrix.m11 * other.m10 + matrix.m12 * other.m20 + matrix.m13 * other.m30;
        result.m11 = matrix.m10 * other.m01 + matrix.m11 * other.m11 + matrix.m12 * other.m21 + matrix.m13 * other.m31;
        result.m12 = matrix.m10 * other.m02 + matrix.m11 * other.m12 + matrix.m12 * other.m22 + matrix.m13 * other.m32;
        result.m13 = matrix.m10 * other.m03 + matrix.m11 * other.m13 + matrix.m12 * other.m23 + matrix.m13 * other.m33;
        result.m20 = matrix.m20 * other.m00 + matrix.m21 * other.m10 + matrix.m22 * other.m20 + matrix.m23 * other.m30;
        result.m21 = matrix.m20 * other.m01 + matrix.m21 * other.m11 + matrix.m22 * other.m21 + matrix.m23 * other.m31;
        result.m22 = matrix.m20 * other.m02 + matrix.m21 * other.m12 + matrix.m22 * other.m22 + matrix.m23 * other.m32;
        result.m23 = matrix.m20 * other.m03 + matrix.m21 * other.m13 + matrix.m22 * other.m23 + matrix.m23 * other.m33;
        result.m30 = matrix.m30 * other.m00 + matrix.m31 * other.m10 + matrix.m32 * other.m20 + matrix.m33 * other.m30;
        result.m31 = matrix.m30 * other.m01 + matrix.m31 * other.m11 + matrix.m32 * other.m21 + matrix.m33 * other.m31;
        result.m32 = matrix.m30 * other.m02 + matrix.m31 * other.m12 + matrix.m32 * other.m22 + matrix.m33 * other.m32;
        result.m33 = matrix.m30 * other.m03 + matrix.m31 * other.m13 + matrix.m32 * other.m23 + matrix.m33 * other.m33;
        return result;
    }

    public static Matrix4x4 Multiply(this Matrix4x4 matrix1, Matrix4x4 matrix2, Matrix4x4 matrix3) {
        Matrix4x4 result;
        result = matrix1.Multiply(matrix2);
        return result.Multiply(matrix3);
    }
}