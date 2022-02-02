using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    private Animator anim;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] GameObject restartBtnMenu;
    [SerializeField] GameObject pauseBtnMenu;
    [SerializeField] GameObject weapon;   
    [SerializeField] Text textWeaponReload;
    [SerializeField] GameObject losePanel;

    private readonly float attackWait = 5;
    private bool isAttack;
    private int lineToMove = 1;
    public float lineDistance = 4;
    private readonly float maxSpeed = 90;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        StartCoroutine(SpeedIncrease());
        isAttack = true;
    }

    void Update()
    {
        InputMove();        
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

    

   

    private void InputMove()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
        }

        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        if (SwipeController.swipeUp)
        {
            if (controller.isGrounded)
                Jump();
        }

        if (SwipeController.doubleTap)
        {
            if (Time.timeScale > 0)
            { 
                if(isAttack)
                    StartCoroutine(AttackWait());
            }
        }
           
                
        IsGroundedAnim();
        HorizontalMove();            
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
       
        Vector3 posWeapon = new(transform.position.x, transform.position.y + 1f, transform.position.z + 1f);
        Instantiate(weapon,posWeapon, weapon.transform.rotation);
    }

    private void IsGroundedAnim()
    {
        if (controller.isGrounded)
            anim.SetBool("Running", true);
        else
            anim.SetBool("Running", false);
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
