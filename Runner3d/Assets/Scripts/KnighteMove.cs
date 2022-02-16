using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnighteMove : MonoBehaviour
{
    private float speed;
    private float quaternion;
    [SerializeField] private float maxMovePosition;
    [SerializeField] private float minMovePosition;

    private void Start()
    {      
        speed = Random.Range(5,9);
    }

    private void FixedUpdate()
    {
        if (transform.position.x > maxMovePosition)
        {
            quaternion = -90;
        }
        if (transform.position.x < minMovePosition)
        {
            quaternion = 90;
        }
        Move(quaternion);
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }
    }

    private void Move(float Rotate)
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.rotation = Quaternion.AngleAxis(Rotate,Vector3.up);
    }

}
