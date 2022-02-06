using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;

    private Vector3 offset;
    private bool isBack;


    void Start()
    {
        offset = transform.position;
    }

   
    void Update()
    {
       
        if (!isBack)
            Move(speed);
        else
            Move(-speed/2);

        RangeDestroy();
    }

    private void Move(float Speed)
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);        
    }  
    private void RangeDestroy()
    {        
        if(transform.position.z - offset.z > range)
        {
            isBack = true;
        }
        
        if(transform.position.z < offset.z && isBack)
        {
            Destroy(gameObject);
        }
    }
   
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("obstacle"))
        {
            isBack = true;
        }
        if (hit.gameObject.CompareTag("Player"))
        {
            isBack = false;
            Destroy(gameObject);
        }
            
    }

}
