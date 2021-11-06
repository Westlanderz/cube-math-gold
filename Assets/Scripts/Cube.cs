using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Cube : MonoBehaviour {

    private Vector3 startPosition = new Vector3(0, 0, 0);
    private Matrix4x4 target;
    private Matrix4x4 identity = Matrix4x4.identity;
    private int speed = 30;
    [SerializeField] private Matrix4x4 rotationMatrix = Matrix4x4.identity;
    [SerializeField] private Vector3 targetPosition = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 targetAxis = new Vector3(0, 0, 0);
    [SerializeField] private float targetAngle = 0f;
    [SerializeField] private Vector3 targetScale = new Vector3(10, 10, 10);
    [SerializeField] private GameObject parentOfInputFields;
    [SerializeField] private Button inputSet;

    private Vector3 inputAxis;
    private float inputAngle;


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

        inputSet.onClick.AddListener(SetAxisAngleUI);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(rotationMatrix.Equals(Matrix4x4.identity)) {
            target = Matrix4x4Extensions.Get_TRS_Matrix(targetPosition, targetAxis, targetAngle, targetScale);
        } else {
            target = Matrix4x4Extensions.Get_TRS_Matrix(targetPosition, rotationMatrix, targetScale);
        }
        transform.FromMatrix(target);
        target = target.Transpose();
    }

    void Update() {
        HandleInput();
        UpdateUI();
    }

    void UpdateUI() {
        for(int i = 0; i < matrixUI.Count; i++) {
            Text text = matrixUI[i].GetComponent<Text>();
            string str = target[i/4, i%4].ToString();
            text.text = str;
        }
    }

    void HandleInput() {
        inputAxis = new Vector3(0, 0, 0);
        if(Input.GetKey(KeyCode.Z)) {
            inputAxis.x = +1;
        }
        if(Input.GetKey(KeyCode.C)) {
            inputAxis.x = -1;
        }
        if(Input.GetKey(KeyCode.A)) {
            inputAxis.y = +1;
        }
        if(Input.GetKey(KeyCode.D)) {
            inputAxis.y = -1;
        }
        if(Input.GetKey(KeyCode.Q)) {
            inputAxis.z = +1;
        }
        if(Input.GetKey(KeyCode.E)) {
            inputAxis.z = -1;
        }

        if(inputAxis.GetLength() > 0) {
            inputAngle += speed * Time.deltaTime;
            targetAxis = Vector3Extensions.NormalizedVector(inputAxis);
            targetAngle = inputAngle;
        }
    }

    void SetAxisAngleUI() {
        int i = 0;
        string[] inputs = new string[4];
        InputField[] inputfields = parentOfInputFields.GetComponentsInChildren<InputField>();
        
        foreach(InputField input in inputfields) {
            inputs[i] = input.text;
            i++;
        }

        float x, y, z;
        float.TryParse(inputs[0], out x);
        float.TryParse(inputs[1], out y);
        float.TryParse(inputs[2], out z);
        targetAxis = new Vector3(x, y, z);

        float.TryParse(inputs[3], out targetAngle);
    }
}
