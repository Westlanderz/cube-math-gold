using UnityEngine;

public static class Matrix4x4Extensions {
    public static float ConvertDegreesToRadians(this float degrees) {
        return ((float)Mathf.PI / (float)180) * degrees;
    }

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

    public static Matrix4x4 Get_TRS_Matrix(Vector3 position, Vector3 axis, float angle, Vector3 scale) {
        Matrix4x4 m = new Matrix4x4();
        m = GetTranslationMatrix(position).Multiply(SetRotation(axis, angle), GetScaleMatrix(scale));
        return m;
    }

    public static Matrix4x4 Get_TRS_Matrix(Vector3 position, Matrix4x4 rotaion, Vector3 scale) {
        Matrix4x4 m = new Matrix4x4();
        m = GetTranslationMatrix(position).Multiply(rotaion, GetScaleMatrix(scale));
        return m;
    }

    public static Matrix4x4 SetRotation(Vector3 axis, float angle) {
        // rotaion matrix from axis-angle
        Matrix4x4 R = new Matrix4x4();

        angle = ConvertDegreesToRadians(angle);

        float cosAngle = (float)Mathf.Cos(angle);
        float sinAngle = (float)Mathf.Sin(angle);

        axis = axis.normalized;

        R.m00 = cosAngle + axis.x * axis.x * (1 - cosAngle);
        R.m01 = axis.x * axis.y * (1 - cosAngle) - axis.z * sinAngle;
        R.m02 = axis.x * axis.z * (1 - cosAngle) + axis.y * sinAngle;
        R.m03 = 0;
        R.m10 = axis.y * axis.x * (1 - cosAngle) + axis.z * sinAngle;
        R.m11 = cosAngle + axis.y * axis.y * (1 - cosAngle);
        R.m12 = axis.y * axis.z * (1 - cosAngle) - axis.x * sinAngle;
        R.m13 = 0;
        R.m20 = axis.z * axis.x * (1 - cosAngle) - axis.y * sinAngle;
        R.m21 = axis.z * axis.y * (1 - cosAngle) + axis.x * sinAngle;
        R.m22 = cosAngle + axis.z * axis.z * (1 - cosAngle);
        R.m23 = 0;
        R.m30 = 0;
        R.m31 = 0;
        R.m32 = 0;
        R.m33 = 1;
        
        return R;
    }

    public static Vector3 ExtractPosition(this Matrix4x4 matrix) {
        Vector3 position;
        position.x = matrix.m03;
        position.y = matrix.m13;
        position.z = matrix.m23;
        return position;
    }

    public static Quaternion ExtractRotation(this Matrix4x4 m) {
        Vector3 forward;
        Vector3 up;
        forward.x = m.m02;
        forward.y = m.m12;
        forward.z = m.m22;
        up.x = m.m01;
        up.y = m.m11;
        up.z = m.m21;

        return Quaternion.LookRotation(forward, up);
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

    public static bool Equals(this Matrix4x4 matrix1, Matrix4x4 matrix2) {
                return (matrix1.m00 == matrix2.m00 && matrix1.m01 == matrix2.m01 && matrix1.m02 == matrix2.m02 && matrix1.m03 == matrix2.m03
                && matrix1.m10 == matrix2.m10 && matrix1.m11 == matrix2.m11 && matrix1.m12 == matrix2.m12 && matrix1.m13 == matrix2.m13
                && matrix1.m20 == matrix2.m20 && matrix1.m21 == matrix2.m21 && matrix1.m22 == matrix2.m22 && matrix1.m23 == matrix2.m23
                && matrix1.m30 == matrix2.m30 && matrix1.m31 == matrix2.m31 && matrix1.m32 == matrix2.m32 && matrix1.m33 == matrix2.m33);
    }

    public static Matrix4x4 Transpose(this Matrix4x4 matrix) {
        Matrix4x4 result;
        result.m00 = matrix.m00;
        result.m01 = matrix.m10;
        result.m02 = matrix.m20;
        result.m03 = matrix.m30;
        result.m10 = matrix.m01;
        result.m11 = matrix.m11;
        result.m12 = matrix.m21;
        result.m13 = matrix.m31;
        result.m20 = matrix.m02;
        result.m21 = matrix.m12;
        result.m22 = matrix.m22;
        result.m23 = matrix.m32;
        result.m30 = matrix.m03;
        result.m31 = matrix.m13;
        result.m32 = matrix.m23;
        result.m33 = matrix.m33;
        return result;
    }
}