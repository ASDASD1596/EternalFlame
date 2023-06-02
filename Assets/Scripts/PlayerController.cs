using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public LightControll LightControll;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    [SerializeField] private float speed;
    private float moveInput;
    [SerializeField] private float jumpForce;
    [SerializeField] private int deadHigh;
    [SerializeField] private float maxJumpforce;
    [SerializeField] private float Jumppersce;

    private bool isGrounded;
    public Transform feetPos;
    public Vector2 radiusCheck;
    public LayerMask groundMask;
    public bool canMove = true;
    public bool canJump = true;
    private Vector3 respawnPoint;
    private bool Dead = false;
    public float radiusCheck2;

    private Animator anim;
    private bool facingRight = true;



    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;

        SoundManager.instance.Play(SoundManager.SoundName.BGM1);
    }



    void Updated()
    {
        if (canMove == true)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
        }

        if (jumpForce == 0 && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        //isGrounded = Physics2D.OverlapBox(feetPos.position, radiusCheck, groundMask);

        //isGrounded = Physics2D.OverlapCircle(feetPos.position, radiusCheck2, groundMask);

        isGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, groundMask);

        if (Mathf.Abs(moveInput) > 0 && rb.velocity.y == 0 && isGrounded)
            anim.SetBool("isRunning", true);
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump == true)
        {
            anim.SetBool("isCrouching", true);
            jumpForce += Jumppersce;


        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump ==true)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //rb.bodyType = RigidbodyType2D.Kinematic;
            //anim.SetBool("isCrouching",true);
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }


        if (jumpForce >= maxJumpforce && isGrounded) // jump code
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //rb.bodyType = RigidbodyType2D.Dynamic;
            anim.SetBool("isCrouching", false);
            //anim.SetBool("isJumping",true);
            float tempx = moveInput * (speed * 2f); //เพิ่ม ดล ระยะโดด
            float tempy = jumpForce;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("JumpDelay", 0.1f);
            //canJump = true;

            SoundManager.instance.Play(SoundManager.SoundName.Jump);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded && canJump)
        {
            if (isGrounded)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                //rb.bodyType = RigidbodyType2D.Dynamic;
                anim.SetBool("isCrouching", false);
                //anim.SetBool("isJumping",true);
                rb.velocity = new Vector2(moveInput * (speed * 2f), jumpForce); //เพิ่ม ดล ระยะโดด
                jumpForce = 0;
                //Invoke("JumpDelay", 0.1f);

                SoundManager.instance.Play(SoundManager.SoundName.Jump);
            }
            canJump = true;
        }

        if (moveInput > 0 && !facingRight && isGrounded)
        {
            flip();
        }
        if (moveInput < 0 && facingRight && isGrounded)
        {
            flip();
        }

        if (rb.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }
        if (rb.velocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
        if (rb.velocity.y > 0)
        {
            //anim.SetBool("isCrouching",false);
            anim.SetBool("isJumping", true);
        }
    }

    IEnumerator RespawnDelay()
    {
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(4);
        /*transform.position = respawnPoint;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        LightControll.RestartLight();
        anim.SetBoolฃ("isDead",false);*/
        SceneManager.LoadScene("Map");
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Ground") && rb.velocity.y < deadHigh)
        {
            canMove = false;
            canJump = false;
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetBool("isDead", true);
            StartCoroutine(RespawnDelay());
            LightControll.ReduceLight();
            SoundManager.instance.Play(SoundManager.SoundName.Dead);
        }

        if (col.gameObject.tag == "Obstacle")
        {
            canJump = false;
            canMove = false;
            anim.SetBool("isDead", true);
            StartCoroutine(RespawnDelay());
            LightControll.ReduceLight();
            SoundManager.instance.Play(SoundManager.SoundName.Dead);
        }
    }
    void JumpDelay()
    {
        //canJump = false;
        jumpForce = 0;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        /*if (col.gameObject.tag == "Obstacle")
        {
            canJump = false;
            canMove = false;
            anim.SetBool("isDead", true);
            StartCoroutine(RespawnDelay());
            LightControll.ReduceLight();
            SoundManager.instance.Play(SoundManager.SoundName.Dead);
        }*/

        if (col.gameObject.tag == "Ground")
        {
            SoundManager.instance.Play(SoundManager.SoundName.Dead);
        }
    }

    void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }



}

