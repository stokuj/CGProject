using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontal;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
         horizontal = Input.GetAxis("Horizontal");


        //Flip player when moving left/right
        if (horizontal > 0.01f)
        {
            transform.localScale = Vector3.one;
            if (Input.GetKey(KeyCode.B))
                Dash();
        }
        else if (horizontal < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (Input.GetKey(KeyCode.B))
                Dash();
        }

        //Set animation parameters (Animator)
        anim.SetBool("swordRun", horizontal != 0);
        anim.SetBool("grounded", isGrounded());

        // Wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontal * speed, body.velocity.y);

            if (isOnWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 3;

            //if space pressed and if player is standing on the ground
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();

                if (Input.GetKeyDown(KeyCode.Space) && isGrounded() || isOnWall())
                    SoundManager.instance.PlaySound(jumpSound);
            }


            //walking sounds
            if(horizontal != 0 && isGrounded())
                GetComponent<AudioSource> ().UnPause();
            else
                GetComponent<AudioSource> ().Pause();

            if (Input.GetKey(KeyCode.V))
                Crouch();

        }
        else
            wallJumpCooldown += Time.deltaTime;

    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (isOnWall() && !isGrounded())
        {
            if(horizontal==0)
            {
                body.velocity = new Vector2(-Math.Sign(transform.localScale.x) * 3, 0);
                transform.localScale = new Vector3(-Math.Sign(transform.localScale.x) ,transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Math.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCooldown = 0;

        }
    }


    private void Crouch()
    {
        if (isGrounded())
        {
            anim.SetTrigger("crouch");
        }
    }

    private void Dash()
    {
        if (isGrounded())
        {
            anim.SetTrigger("dash");
        }
    }


    //returns true if player is on the ground
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }


    //returns true if player is on a wall
    private bool isOnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontal == 0 && isGrounded() && !isOnWall();
    }

}
