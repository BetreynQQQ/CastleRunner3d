using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnighteMove : MonoBehaviour
{
    private CharacterController controller;
    Vector3 pos;
    [SerializeField] float speed;
    Quaternion rotationY;

    void Start()
    {
        controller = GetComponent<CharacterController>();        
    }


    void FixedUpdate()
    {  
        if (controller.transform.position.x > 11)
        {
            rotationY = Quaternion.AngleAxis(-90, Vector3.up);
            transform.rotation = rotationY;
            pos.x = -speed;
        }
        if (controller.transform.position.x < -9)
        {
            rotationY = Quaternion.AngleAxis(90, Vector3.up);
            transform.rotation = rotationY;
            pos.x = speed;           
        }             
        controller.Move(pos * Time.fixedDeltaTime);
    }
}
