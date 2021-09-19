using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Cube : MonoBehaviour {

    private Vector3 startPosition = new Vector3(0, 0, 0);
    private Matrix4x4 target;
    [SerializeField ]private Matrix4x4 matrix = Matrix4x4.identity;

    [SerializeField] private Vector3 targetPosition = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 targetScale = new Vector3(10, 10, 10);


    private List<GameObject> matrixUI = new List<GameObject>();
 
    // Start is called before the first frame update
    void Awake() {
        transform.position = startPosition;
        target = Matrix4x4.identity;
        matrixUI.Add(GameObject.Find("matrix00"));
        matrixUI.Add(GameObject.Find("matrix01"));
        matrixUI.Add(GameObject.Find("matrix02"));
        matrixUI.Add(GameObject.Find("matrix03"));
        matrixUI.Add(GameObject.Find("matrix10"));
        matrixUI.Add(GameObject.Find("matrix11"));
        matrixUI.Add(GameObject.Find("matrix12"));
        matrixUI.Add(GameObject.Find("matrix13"));
        matrixUI.Add(GameObject.Find("matrix20"));
        matrixUI.Add(GameObject.Find("matrix21"));
        matrixUI.Add(GameObject.Find("matrix22"));
        matrixUI.Add(GameObject.Find("matrix23"));
        matrixUI.Add(GameObject.Find("matrix30"));
        matrixUI.Add(GameObject.Find("matrix31"));
        matrixUI.Add(GameObject.Find("matrix32"));
        matrixUI.Add(GameObject.Find("matrix33"));
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(matrix.Equals(Matrix4x4.identity)) {
            target = matrix.Get_TRS_Matrix(targetPosition, targetRotation, targetScale);
        } else {
            target = matrix;
        }
        transform.FromMatrix(target);
        target = target.Transpose();
        for(int i = 0; i < matrixUI.Count; i++) {
            Text text = matrixUI[i].GetComponent<Text>();
            string str = target[i/4, i%4].ToString();
            text.text = str;
        }
    }
}
