using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    
    [SerializeField] private float rotateSpeed = 10f;

    void Update()
    {
        RotaionWeapon();
    }   

    private void RotaionWeapon()
    {
        transform.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);
        
    }   
}
