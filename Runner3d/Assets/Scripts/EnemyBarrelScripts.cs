using UnityEngine;

public class EnemyBarrelScripts : MonoBehaviour
{
    private CharacterController controller;
    Vector3 pos;
    Quaternion rotationY;
    float speed;
    [SerializeField] float RotationSpeed;
    [SerializeField] float posMoveXstart;
    [SerializeField] float posMoveXend;
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
        if (controller.transform.position.x > posMoveXend)
        {
            rotationY = Quaternion.AngleAxis(RotationSpeed, Vector3.forward);
            pos.x = -speed;
        }
        if (controller.transform.position.x == -posMoveXstart)
        {
            rotationY = Quaternion.AngleAxis(-RotationSpeed, Vector3.forward);
            pos.x = speed;
        }
        transform.rotation *= rotationY;
        controller.Move(pos * Time.fixedDeltaTime);
    }

}
