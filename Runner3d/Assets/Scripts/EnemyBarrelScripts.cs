using UnityEngine;

public class EnemyBarrelScripts : MonoBehaviour
{
    private CharacterController controller;
    Vector3 pos;
    Quaternion rotationY;
    [SerializeField] float speed;
    [SerializeField] float RotationSpeed;
    [SerializeField] float posMoveXstart;
    [SerializeField] float posMoveXend;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void FixedUpdate()
    {        
        if (controller.transform.position.x > posMoveXend)
        {
            rotationY = Quaternion.AngleAxis(RotationSpeed, Vector3.forward);
            transform.rotation *= rotationY;
            pos.x = -speed;
        }
        if (controller.transform.position.x == -posMoveXstart)
        {
            rotationY = Quaternion.AngleAxis(-RotationSpeed, Vector3.forward);
            transform.rotation *= rotationY;
            pos.x = speed;
        }
        controller.Move(pos * Time.fixedDeltaTime);
    }


}
