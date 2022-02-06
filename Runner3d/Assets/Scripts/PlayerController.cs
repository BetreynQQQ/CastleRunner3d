using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    private Animator anim;    

    public static Vector3 PlayerPos;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] GameObject restartBtnMenu;
    [SerializeField] GameObject pauseBtnMenu;
    [SerializeField] GameObject weapon;   
    [SerializeField] Text textWeaponReload;
    [SerializeField] GameObject losePanel;


    private readonly float attackWait = 1.3f;
    private bool isAttack;
    private int lineToMove = 1;
    public float lineDistance = 4;
    private readonly float maxSpeed = 60;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        StartCoroutine(SpeedIncrease());
        isAttack = true;
        SwipeManager.instance.MoveEvent += MovePlayer;
        SwipeManager.instance.DoubleClickEvent += DoubleClick;
    }

    void Update()
    {
        PlayerPos = controller.transform.position;
        IsGroundedAnim();        
        HorizontalMove();
        WallRun();
    }

    void FixedUpdate()
    {
       Run();
    }

    private void Jump()
    {
        dir.y = jumpForce;       
        anim.SetTrigger("Jump");      
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("obstacle"))
        {
            losePanel.SetActive(true);
            pauseBtnMenu.SetActive(false);
            restartBtnMenu.SetActive(false);
            textWeaponReload.text = "";
            Time.timeScale = 0;
        }
    }

    private void MovePlayer(bool[] swipes)
    {
        if (swipes[(int)SwipeManager.Direction.Left])
        {
            if (lineToMove < 2)
                lineToMove++;


        }

        if (swipes[(int)SwipeManager.Direction.Right])
        {
            if (lineToMove > 0)
                lineToMove--;
        }



        if (swipes[(int)SwipeManager.Direction.Up])
        {
            if (controller.isGrounded)
                Jump();
        }       
    }

    private void DoubleClick(Vector2 pos)
    {
        if(pos != null)
            if (Time.timeScale > 0)
            {
                if (isAttack && controller.isGrounded)
                    StartCoroutine(AttackWait());
            }
    }

    private void HorizontalMove()
    {
      

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lineToMove == 0)
        {
            targetPosition += Vector3.left * lineDistance;
        }
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;      

        if (transform.position == targetPosition)
            return;
     
        Vector3 diff = targetPosition - transform.position;

        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);

       
    }

    private void Run()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        StartCoroutine(AnimAttack());
        //Vector3 posWeapon = new(transform.position.x, transform.position.y + 1f, transform.position.z + 1f);
        //Instantiate(weapon,posWeapon, weapon.transform.rotation);
    }

    private void IsGroundedAnim()
    {
        if (controller.isGrounded)
            anim.SetBool("Running", true);
        else
            anim.SetBool("Running", false);
    }

    private bool WallRun()
    {
        RaycastHit hit;
        Vector3 target = new(transform.position.x - 50f, transform.position.y, transform.position.z);
        Ray ray = new(transform.position, target - transform.position);
        Physics.Raycast(ray, out hit);
        if(hit.collider != null)
        {
            if (hit.collider.CompareTag("WallRun"))
                return true;
            Debug.DrawLine(ray.origin,hit.point,Color.blue);
        }
        return false;
    }

    private void StartWallRun()
    {
        if(WallRun())
        {
            transform.Rotate(Vector3.back, 90);
        }
       
    }

    private IEnumerator AttackWait()
    {
        textWeaponReload.text = "Нет топора";
        Attack();
        isAttack = false;
        yield return new WaitForSeconds(attackWait);
        textWeaponReload.text = "Топор готов";
        isAttack = true;
    }

    private IEnumerator AnimAttack()
    {       
        yield return new WaitForSeconds(0.3f);
        Vector3 posWeapon = new(transform.position.x, transform.position.y + 1f, transform.position.z + 1f);
        Instantiate(weapon, posWeapon, weapon.transform.rotation);
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(4);
        if (speed < maxSpeed)
        {
            speed += 1;
            StartCoroutine(SpeedIncrease());
        }
    }
  
}
