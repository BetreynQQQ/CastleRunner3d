using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.position;
    }
       
}
