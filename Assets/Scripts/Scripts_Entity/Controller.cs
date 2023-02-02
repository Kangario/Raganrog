using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Объекты")]
    [SerializeField] private GameObject curentObject; 
    [SerializeField] private GameObject cameraObject;
    [Header("Скорость")]
    [SerializeField] private float speed;
    [Header("Высота прыжка")]
    [SerializeField] private float jumpHeight;
    [Header("Ускорение")]
    [SerializeField] private float accelerationTime;
    private Rigidbody rb;
    private float acceleration;
    private bool isGround;
    private float time;
    private bool key;
    private bool forward;
    private void Start()
    {
        rb =curentObject.GetComponent<Rigidbody>();
        acceleration = speed / accelerationTime;
    }

    private float CheckSpeed(float tempSpeed)
    {
        time += Time.deltaTime * accelerationTime;

        if ((acceleration * time) < speed)
        {       
            tempSpeed +=  acceleration * (time);
        }else if ((acceleration * time) >= speed)
        {
            tempSpeed = speed;
            
        }
        
        return tempSpeed;
    }

    private void RotateeObject()
    {
        curentObject.transform.eulerAngles = new Vector3(cameraObject.transform.eulerAngles.x,
            cameraObject.transform.eulerAngles.y, cameraObject.transform.eulerAngles.z);
    }
    private void jump()
    {
        if (isGround)
        {
            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
        }
    }
   private void FixedUpdate()
    {
     #region[Передвижение]
        float tempSpeed = 0;
        float tempSpeedAD = 0;
        if (Input.GetKey(KeyCode.W))
        {
            if (forward == false)
            {

                time = 0;
            }
            RotateeObject(); 
            tempSpeed = CheckSpeed(tempSpeed);
            key = true;
            forward = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (forward == true)
            {
                time = 0;
            }
            tempSpeed = CheckSpeed(tempSpeed);
            tempSpeed = -tempSpeed;
            forward = false;
            key = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            tempSpeedAD = CheckSpeed(tempSpeedAD);
            tempSpeedAD = -tempSpeedAD;
            key = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            tempSpeedAD = CheckSpeed(tempSpeedAD);
            key = true;
        }
        if (Input.GetKey(KeyCode.Space)) {
            jump();
        }
        if ( key != true)
        {
            time = 0;
            key = false;
        }
        gameObject.transform.Translate(tempSpeedAD, 0, tempSpeed);
        key = false;
        #endregion

      
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isGround = false;
        }
    }
}
