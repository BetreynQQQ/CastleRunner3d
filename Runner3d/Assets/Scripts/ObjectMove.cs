using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{  
    [SerializeField] private float posMoveXmax;
    [SerializeField] private float posMoveXmin;
    private float speed;
    private float currentSpeed;


    void Start()
    {       
        speed = Random.Range(7, 10);
        EnemyBarrelScripts.BoomEvent += Boom;

    }

    private void Boom()
    {
        currentSpeed = 0;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position.x >= posMoveXmax)
        {
            currentSpeed = speed;
        }

        if (transform.position.x <= -posMoveXmin)
        {
            currentSpeed = -speed;
        }

        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        EnemyBarrelScripts.BoomEvent -= Boom;
    }
}
