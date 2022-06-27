using UnityEngine;

/// <summary>
/// A class for player movements
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Parameters")]
     /**  Speed      */
    [SerializeField] private float speed;
     /**  Power of jumping      */
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
     /** How much time the player can hang in the air before jumping       */
    [SerializeField] private float coyoteTime; 
     /**  How much time passed since the player ran off the edge      */
    private float coyoteCounter; 

    [Header("Multiple Jumps")]
     /**  How many extre jumpw player will have      */
    [SerializeField] private int extraJumps;
     /**  Counter of jumps      */
    private int jumpCounter;

    [Header("Wall Jumping")]
     /**  Horizontal wall jump force      */
    [SerializeField] private float wallJumpX; 
     /**  Vertical wall jump force      */
    [SerializeField] private float wallJumpY; 

    [Header("Layers")]

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private LayerMask wallLayer;

    [Header("Sounds")]
     /**  Jump sound      */
    [SerializeField] private AudioClip jumpSound;
     /**  Walk sound      */
    [SerializeField] private AudioClip walkSound;
     /**  Rigidbody2D Obj      */
    private Rigidbody2D body;
     /**  Animator  Obj     */
    private Animator anim;
     /**  BoxCollider2D Obj     */
    private BoxCollider2D boxCollider;
     /**  Cooldown between  jumping on wall     */
    private float wallJumpCooldown;

    private float horizontalInput;

    /// <summary>
    /// Method initializes componets of movement
    /// </summary>
    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Method for updating behaviour of player with animations, 
    /// </summary>
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

       

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
            if (Input.GetKey(KeyCode.B))
                Dash();
        }

        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (Input.GetKey(KeyCode.B))
                Dash();
        }
           
        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime; //Reset coyote counter when on the ground
                jumpCounter = extraJumps; //Reset jump counter to extra jump value
            }
            else
                coyoteCounter -= Time.deltaTime; //Start decreasing coyote counter when not on the ground
        }

        if (Input.GetKey(KeyCode.V))
            Crouch();
    }
    /// <summary>
    /// Method for  crouching
    /// </summary>
    private void Crouch()
    {
        if (isGrounded())
        {
            anim.SetTrigger("crouch");
        }
    }
    /// <summary>
    /// Method for  dashing
    /// </summary>
    private void Dash()
    {
        if (isGrounded())
        {
            anim.SetTrigger("dash");
        }
    }

    /// <summary>
    /// Method for  jumping
    /// </summary>
    private void Jump()
    {
        if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) return; 
        //If coyote counter is 0 or less and not on the wall and don't have any extra jumps don't do anything

        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
            WallJump();
        else
        {
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                //If not on the ground and coyote counter bigger than 0 do a normal jump
                if (coyoteCounter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else
                {
                    if (jumpCounter > 0) //If we have extra jumps then jump and decrease the jump counter
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            //Reset coyote counter to 0 to avoid double jumps
            coyoteCounter = 0;
        }
    }

    /// <summary>
    /// Method for jumping on wall
    /// </summary>
    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }


    /// <summary>
    /// Method for checks if player is grounded
    /// </summary>
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    /// <summary>
    /// Method for checks if player in on wall
    /// </summary>
    /// <returns>null in case of collision </returns>
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    /// <summary>
    /// Method for checks if player can attack
    /// </summary>
    /// <returns>false if player is in situation that he cann't attack</returns>
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}