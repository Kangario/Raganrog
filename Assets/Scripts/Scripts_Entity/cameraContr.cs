using UnityEngine;
using System.Collections;
public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    [SerializeField] private GameObject curentObj;
    private RotationAxes axes = RotationAxes.MouseXAndY;
    [Header("Чувствительность мыши")]
    [SerializeField] private float sensitivityHor = 1.0f;
    [SerializeField] private float sensitivityVert = 1.0f;
     private float minimumVert = -45.0f;
     private float maximumVert = 45.0f;
     private float _rotationX = 0;
    private void Start()
    {
      Cursor.lockState = CursorLockMode.Locked; 
    }
    private void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(curentObj.transform.position.x,
            curentObj.transform.position.y, curentObj.transform.position.z);
    }
}
