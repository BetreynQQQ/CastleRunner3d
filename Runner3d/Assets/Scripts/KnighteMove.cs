using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnighteMove : MonoBehaviour
{
    private CharacterController controller;
    Vector3 pos;
    float speed;
    [SerializeField] float startMovePosition;
    [SerializeField] float endMovePosition;
    Quaternion rotationY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = Random.Range(5,8);
    }


    void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            Destroy(gameObject);
        }
    }
  
    private void Move()
    {
        if (controller.transform.position.x > startMovePosition)
        {
            rotationY = Quaternion.AngleAxis(-90, Vector3.up);
            transform.rotation = rotationY;
            pos.x = -speed;
        }
        if (controller.transform.position.x < endMovePosition)
        {
            rotationY = Quaternion.AngleAxis(90, Vector3.up);
            transform.rotation = rotationY;
            pos.x = speed;
        }
        controller.Move(pos * Time.fixedDeltaTime);
    }

}
