using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour {

    private Vector3 startPosition = new Vector3(0, 0, 0);

    [SerializeField] private Vector3 targetPosition = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 targetScale = new Vector3(10, 10, 10);
    [SerializeField] private Matrix4x4 matrix = new Matrix4x4(new Vector4(1, 0, 0, 0), 
                                                              new Vector4(0, 1, 0, 0), 
                                                              new Vector4(0, 0, 1, 0), 
                                                              new Vector4(0, 0, 0, 1));
    
    [SerializeField] private Matrix4x4 target;
 
    // Start is called before the first frame update
    void Awake() {
        transform.position = startPosition;
    }

    // Update is called once per frame
    void FixedUpdate() {
        target = matrix.Get_TRS_Matrix(targetPosition, targetRotation, targetScale);
        transform.FromMatrix(matrix);
    }
}
