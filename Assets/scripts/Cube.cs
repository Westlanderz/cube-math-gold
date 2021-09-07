using UnityEngine;

public class Cube : MonoBehaviour {

    private Vector3 startPosition = new Vector3(0, 0, 0);
    public Matrix4x4 matrix;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = startPosition;
        }
        
        transform.FromMatrix(matrix);
    }
}
public static class Matrix4x4Extensions {
    public static Vector3 ExtractPosition(this Matrix4x4 matrix) {
        Vector3 position;
        position.x = matrix.m03;
        position.y = matrix.m13;
        position.z = matrix.m23;
        return position;
    }

    public static Quaternion ExtractRotation(this Matrix4x4 matrix) {
        Quaternion rotation = new Quaternion();
        rotation.w = Mathf.Sqrt(Mathf.Max(0, 1 + matrix.m00 + matrix.m11 + matrix.m22)) / 2;
        rotation.x = Mathf.Sqrt(Mathf.Max(0, 1 + matrix.m00 - matrix.m11 - matrix.m22)) / 2;
        rotation.y = Mathf.Sqrt(Mathf.Max(0, 1 - matrix.m00 + matrix.m11 - matrix.m22)) / 2;
        rotation.z = Mathf.Sqrt(Mathf.Max(0, 1 - matrix.m00 - matrix.m11 + matrix.m22)) / 2;
        rotation.x *= Mathf.Sign(rotation.x * (matrix.m21 - matrix.m12));
        rotation.y *= Mathf.Sign(rotation.y * (matrix.m02 - matrix.m20));
        rotation.z *= Mathf.Sign(rotation.z * (matrix.m10 - matrix.m01));
        return rotation;
    }

    public static Vector3 ExtractScale(this Matrix4x4 matrix) {
        Vector3 scale;
        scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
        scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
        scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
        return scale;
    }
}

public static class TransformExtensions {
    public static void FromMatrix(this Transform transform, Matrix4x4 matrix) {
        transform.rotation = matrix.ExtractRotation();
        transform.position = matrix.ExtractPosition();
        // transform.localScale = matrix.ExtractScale();
    }
}