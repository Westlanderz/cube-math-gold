using UnityEngine;

public static class TransformExtensions {
    public static void FromMatrix(this Transform transform, Matrix4x4 matrix) {
        transform.rotation = matrix.ExtractRotation();
        transform.position = matrix.ExtractPosition();
        transform.localScale = matrix.ExtractScale();
    }
}