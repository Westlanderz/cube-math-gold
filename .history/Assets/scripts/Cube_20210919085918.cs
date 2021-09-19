using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour {

    private Vector3 startPosition = new Vector3(0, 0, 0);

    [SerializeField] private Vector3 targetPosition = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 targetScale = new Vector3(10, 10, 10);
    
    /*
        10 0 0 0
        0 10 0 0
        0 0 10 0
        0 0 0 1

        diagonal: is the scale part, leave unchanged for the same size cube
    */
    [SerializeField] private Matrix4x4 matrix;
 
    // Start is called before the first frame update
    void Awake() {
        transform.position = startPosition;
        currentPosition = startPosition;
    }

    // Update is called once per frame
    void FixedUpdate() {
        matrix.Get_TRS_Matrix(targetPosition, targetRotation, targetScale);
        transform.FromMatrix(matrix);
    }
}
