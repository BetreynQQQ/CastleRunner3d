using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position;      
    }

   
    void Update()
    {
        Move();
       
        RangeDestroy();
    }

    private void Move()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        
    }  
    private void RangeDestroy()
    {        
        if(transform.position.z - offset.z > range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("obstacle"))
        {
            Destroy(gameObject);
        }
    }

}
