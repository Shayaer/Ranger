using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private BoxCollider2D myFeet;
    private Animator myAnimator;

    public float speed;
    public float jumpSpeed;
    public float doubleJumpSpeed;

    private bool playeHasXAxisSpeed;
    private bool isGrounded;
    private bool canDoubleJump;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationFlip();
        Run();
        Jump();
        CheckGrounded();
        SwitchAnimation();
        //Attack();
    }

    void Run()
    {
        float moveDirection = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(moveDirection * speed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        playeHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;//一个极小的值，伊普希农
        myAnimator.SetBool("Run",playeHasXAxisSpeed);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                myAnimator.SetBool("Jump", true);
                Vector2 jumpVelocity = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVelocity;
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    myAnimator.SetBool("DoubleJump", true);
                    Vector2 doubleJumpVelocity = new Vector2(0.0f, doubleJumpSpeed);
                    myRigidbody.velocity = Vector2.up * doubleJumpVelocity;
                    canDoubleJump = false;
                }
            }
        }
    }

    void AnimationFlip()//动画左右翻转
    {
        playeHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playeHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)//用>0会有bug，来回翻转
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void SwitchAnimation()//跳跃动画切换
    {
        myAnimator.SetBool("Idle", false);
        if (myAnimator.GetBool("Jump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnimator.SetBool("Fall", true);
                myAnimator.SetBool("Jump", false);
            }
        }
        else if (isGrounded)
        {
            myAnimator.SetBool("Fall", false);
            myAnimator.SetBool("Idle", true);
        }

        if (myAnimator.GetBool("DoubleJump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnimator.SetBool("DoubleFall", true);
                myAnimator.SetBool("DoubleJump", false);
            }
        }
        else if (isGrounded)
        {
            myAnimator.SetBool("DoubleFall", false);
            myAnimator.SetBool("DoubleIdle", true);
        }
    }

    void CheckGrounded()
    {
        isGrounded = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        //Debug.Log(isGrounded);
    }

    //void Attack()
    //{
    //    if (Input.GetButtonDown("Attack"))
    //    {
    //        if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))//判断Attack动画是否结束//？？？？？？？？
    //        {
    //            myAnimator.SetTrigger("Attack");
    //            StartCoroutine(EnableHitBox());
    //        }
    //    }
    //}
    //IEnumerator EnableHitBox()//协程
    //{
    //    yield return new WaitForSeconds(0.2f);
    //}
}
