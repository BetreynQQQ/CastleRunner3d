using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private CapsuleCollider col;
    private Vector3 dir;
    private Animator anim;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject pauseBtnMenu;
    [SerializeField] private GameObject weapon;   
    [SerializeField] private GameObject WeaponDelayIcon;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Text coinsText;

    private bool isDie;
    private int coinScore;
    private bool isSliding;
    private readonly float attackWait = 1f;
    private bool isAttack,isWallRun,isJump;
    private int lineToMove = 1;
    private float lineDistance = 4;
    private float wallRunLineDistance = 6;
    private readonly float maxSpeed = 60;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        StartCoroutine(SpeedIncrease());
        isDie = false;
        isAttack = true;
        isWallRun = false;
        SwipeManager.instance.MoveEvent += MovePlayer;
        SwipeManager.instance.DoubleClickEvent += DoubleClick;
    }

    private void Update()
    {
        if (isDie)
            return;
        IsGroundedAnim();        
        HorizontalMove();
              
    }

    private void FixedUpdate()
    {        
        Run();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Stolknoveniye2");

        if (hit.gameObject.CompareTag("obstacle"))
        {            
            StartCoroutine(Die());
            anim.SetBool("Running", false);
            anim.SetBool("DieBool", true);
        }
    }



    private IEnumerator Die()
    {
        isDie = true;
        speed = 0;        
        yield return new WaitForSeconds(2f);
        losePanel.SetActive(true);
        pauseBtnMenu.SetActive(false);
        Time.timeScale = 0;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            coinScore++;
            coinsText.text = coinScore.ToString();
        }
        if (other.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("Столкновение");
            StartCoroutine(Die());
            anim.SetBool("Running", false);
            anim.SetBool("DieBool", true);
        }
    }

    private void MovePlayer(bool[] swipes)
    {
        if (swipes[(int)SwipeManager.Direction.Left] && !isWallRun)
        {
            if (lineToMove < 3)
                lineToMove++;

            if (lineToMove == 3 && controller.isGrounded)
                Debug.Log("WallRun right");
        }

        if (swipes[(int)SwipeManager.Direction.Right] && !isWallRun)
        {
            if (lineToMove > -1)
                lineToMove--;

            if (lineToMove == -1 && controller.isGrounded)
                Debug.Log("WallRun left");
        }

        if (swipes[(int)SwipeManager.Direction.Up] && !isWallRun)
        {
            if (controller.isGrounded)
                Jump();
        }

        if (swipes[(int)SwipeManager.Direction.Down] && !isWallRun)
        {
           if(!isSliding)
                StartCoroutine(Slide());
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
                targetPosition += Vector3.left * lineDistance;
            else if (lineToMove == 2)
                targetPosition += Vector3.right * lineDistance;
            if (transform.position == targetPosition)
                return;

            if (lineToMove == -1)
            {
                if (WallRun(50))
                {
                    targetPosition += Vector3.left * wallRunLineDistance;
                    StartCoroutine(ResetPosWallRun());
                }
                else
                    lineToMove = 0;
            }

            if (lineToMove == 3)
            {
                if (WallRun(-50))
                {
                    targetPosition += Vector3.right * wallRunLineDistance;
                    StartCoroutine(ResetPosWallRun());
                }
                else
                    lineToMove = 2;
            }
            
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

    private void Jump()
    {
        if (isDie)
            return;
        anim.ResetTrigger("Slide");
        dir.y = jumpForce;
        anim.SetTrigger("Jump");
    }

    private void Attack()
    {
        if (isDie)
            return;
        anim.SetTrigger("Attack");
        StartCoroutine(AnimAttack());        
    }

    private IEnumerator Slide()
    {        
        col.center = new Vector3(0, -0.4f, 0);
        col.height = 0.5f;      
        anim.SetTrigger("Slide");
        isSliding = true;
        dir.y = -jumpForce;        
        yield return new WaitForSeconds(0.5f);
        col.center = new Vector3(0, 0.7f, 0);
        col.height = 2;
        isSliding = false;
    }


    private void IsGroundedAnim()
    {
        if (controller.isGrounded && !isWallRun)
            anim.SetBool("Running", true);
        else
            anim.SetBool("Running", false);
    }

    private IEnumerator AttackWait()
    {
        WeaponDelayIcon.SetActive(false);
        Attack();
        isAttack = false;
        yield return new WaitForSeconds(attackWait);
        WeaponDelayIcon.SetActive(true);
        isAttack = true;
    }

    private IEnumerator AnimAttack()
    {       
        yield return new WaitForSeconds(0.2f);
        Vector3 posWeapon = new(transform.position.x, transform.position.y + 1f, transform.position.z + 1f);
        Instantiate(weapon, posWeapon, weapon.transform.rotation);
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(4);
        if (speed < maxSpeed)
        {
            if(!isDie)
                speed += 1;
            StartCoroutine(SpeedIncrease());
        }
    }

    private bool WallRun(float Direction)
    {
        RaycastHit hit;
        Vector3 target = new(transform.position.x - Direction, transform.position.y, transform.position.z);
        Ray ray = new(transform.position, target - transform.position);
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("WallRun"))
                return true;
        }
        return false;
    }

    private IEnumerator ResetPosWallRun()
    {      
        if (lineToMove == -1)
            anim.SetTrigger("LWallRun");
        if (lineToMove == 3)
            anim.SetTrigger("RWallRun");      
        isWallRun = true;
        yield return new WaitForSeconds(0.4f);
        anim.ResetTrigger("RWallRun");
        anim.ResetTrigger("LWallRun");
        if (lineToMove == -1)
            lineToMove = 0;                  
        if (lineToMove == 3)
            lineToMove = 2;
        isWallRun = false;        
    }

   
}
