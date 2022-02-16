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
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = Random.Range(7,10);
    }

    
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (controller.transform.position.x > posMoveXmax)
        {
            rotationY = Quaternion.AngleAxis(RotationSpeed, Vector3.forward);
            pos.x = -speed;
        }
        if (controller.transform.position.x == -posMoveXmin)
        {
            rotationY = Quaternion.AngleAxis(-RotationSpeed, Vector3.forward);
            pos.x = speed;
        }
        transform.rotation *= rotationY;
        controller.Move(pos * Time.fixedDeltaTime);
    }
    
}
