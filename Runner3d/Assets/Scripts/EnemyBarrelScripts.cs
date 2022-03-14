using System;
using System.Collections;
using UnityEngine;

public class EnemyBarrelScripts : MonoBehaviour
{
    private CharacterController controller;
    Vector3 pos;
    Quaternion rotationY;
    float speed;
    [SerializeField] float RotationSpeed;
    [SerializeField] float posMoveXmax;
    [SerializeField] float posMoveXmin;
    [SerializeField] GameObject Fire;
    [SerializeField] GameObject Explosion;
    public static Action BoomEvent;


    void Start()
    {        
       // speed = Random.Range(7,10);
    }

    
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position.x > posMoveXmax)
        {
            rotationY = Quaternion.AngleAxis(RotationSpeed, Vector3.forward);
           // pos.x = -speed;
        }
        if (transform.position.x == -posMoveXmin)
        {
            rotationY = Quaternion.AngleAxis(-RotationSpeed, Vector3.forward);
           // pos.x = speed;
        }
        transform.rotation *= rotationY;
       // controller.Move(pos * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Fire.gameObject.SetActive(false);
            Explosion.gameObject.SetActive(true);
            StartCoroutine(DestroyFire());
            this.gameObject.SetActive(false);
            BoomEvent?.Invoke();
        }
    }

    private IEnumerator DestroyFire()
    {
        yield return new WaitForSeconds(0.3f);
        Explosion.gameObject.SetActive(false);
    }
}
