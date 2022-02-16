using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private float rotateSpeed;

    private void Start()
    {        
        rotateSpeed = Random.Range(40, 70);
    }

    private void Update()
    {
        Rotaion();
    }


    private void Rotaion()
    {
        transform.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))      
            Destroy(gameObject);  
    }
}
