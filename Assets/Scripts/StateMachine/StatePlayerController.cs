using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float runSpeed = 7.3f;
    public float accelerationTimeAirborne;
    public float accelerationTimeGrounded;
    private float velocityXSmoothing;
    public float moveAfterLaunchTime;
    private float moveAfterLaunchTimer;
    [HideInInspector]
    public Vector2 moveInput;

    //variables for variable jump height
    public float maxJumpVelocity;
    public float minJumpVelocity;

    //the player's rigidbody
    private Rigidbody2D rb;

    //Everything for being grounded
    [HideInInspector]
    public bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform groundCheck;

    public PlayerControls playerControls;
    public float dashTime;
    public float dashSpeed;
    public bool canDash = true;
    private Player playerManager;

    private void Awake() {
        playerControls = new PlayerControls();
    }

    void OnEnable() {
        playerControls.Enable();
    }

    void OnDisable() {
        playerControls.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerManager = GetComponent<Player>();
    }

    public void Update() {
        
    }

    public float CalculatePlayerVelocity(float RBvelocity, Vector2 input, float moveSpeed, float velocityXSmoothing, float accelerationTimeGrounded, float accelerationTimeAirborne, bool isGrounded)
    {
        float targetVelocityx = input.x * (playerControls.InGame.Dash.IsPressed() ? runSpeed : moveSpeed);
        return Mathf.SmoothDamp(RBvelocity, targetVelocityx, ref velocityXSmoothing, isGrounded ? accelerationTimeGrounded : accelerationTimeAirborne);
    }

    //if you jump it changes your y velocity to the maxJumpVelocity
    public void Jump()
    {
       rb.velocity = new Vector2(rb.velocity.x, maxJumpVelocity);
    }

    public void JumpRelease()
    {
        if (rb.velocity.y > minJumpVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, minJumpVelocity);
        }
    }

    public void WallJump(Vector2 jumpVelocity)
    {
        rb.velocity = jumpVelocity;
    }


    public void HandleMovement()
    {
        moveInput = playerControls.InGame.Move.ReadValue<Vector2>();
        float xVelocity = CalculatePlayerVelocity(rb.velocity.x, moveInput, moveSpeed, velocityXSmoothing, accelerationTimeGrounded, accelerationTimeAirborne, isGrounded);
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    public void HandleLerpMovement()
    {
        moveInput = playerControls.InGame.Move.ReadValue<Vector2>();
        rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(moveInput.x * moveSpeed, rb.velocity.y)), 1f * Time.deltaTime);
    }

    private Vector2 clampTo8Directions(Vector2 vectorToClamp) {
        if (vectorToClamp.x > 0.1f && (vectorToClamp.y < 0.1f && vectorToClamp.y > -0.1f))
        {
            //right
            return new Vector2(1,0);
        }
        if (vectorToClamp.x > 0.1f && vectorToClamp.y < -0.1f)
        {
            //down right
            return new Vector2(1,-1).normalized;
        }
        if ((vectorToClamp.x < 0.1f && vectorToClamp.x > -0.1) && vectorToClamp.y < -0.1f)
        {
            //down
            return new Vector2(0,-1);
        }
        if (vectorToClamp.x < -0.1f && vectorToClamp.y < -0.1f)
        {
            //down left
            return new Vector2(-1,-1).normalized;
        }
        if (vectorToClamp.x < -0.1f && (vectorToClamp.y < 0.1f && vectorToClamp.y > -0.1f))
        {
            //left
            return new Vector2(-1,0);
        }
        if (vectorToClamp.x < -0.1f && vectorToClamp.y > 0.1f)
        {
            //up left
            return new Vector2(-1,1).normalized;
        }
        if ((vectorToClamp.x < 0.1f && vectorToClamp.x > -0.1) && vectorToClamp.y > 0.1f)
        {
            //up
            return new Vector2(0,1);
        }
        if (vectorToClamp.x > 0.1f && vectorToClamp.y > 0.1f)
        {
            //up right
            return new Vector2(1,1).normalized;
        }

        return Vector2.zero;
    }

    public void throwCard(Vector2 throwDir, bool leftThrow) {
        // if (isLawMaker)
        // {
        //     Vector2 normalizedThrowDir = clampTo8Directions(throwDir);
        //     if (throwCooldownTimer <= 0)
        //     {
        //         throwCooldownTimer = throwCooldownTime;
        //         if (leftThrow)
        //         {
        //             PlayerStateInput playerStateInput = playerManager.GetStateInput();
        //             Rigidbody2D cardRB = Instantiate(cardPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(normalizedThrowDir.x, normalizedThrowDir.y, 0)).GetComponent<Rigidbody2D>();
        //             //use the first rule in the array
        //             cardPrefab.ruleIndexToApply = playerHand[0];
        //             if (throwDir.x < 0.1f && throwDir.x > -0.1f && throwDir.y < 0.1f && throwDir.y > -0.1f)
        //             {
        //                 Debug.Log(playerStateInput.lastXDir);
        //                 cardRB.velocity = new Vector2(playerStateInput.lastXDir * cardThrowSpeed, 0);
        //             }
        //             else
        //             {
        //                 cardRB.velocity = normalizedThrowDir * cardThrowSpeed;
        //             }
        //             playerHand[0] = -1;
        //         }
        //         else
        //         {
        //             //use the other rule in the array
        //             PlayerStateInput playerStateInput = playerManager.GetStateInput();
        //             Rigidbody2D cardRB = Instantiate(cardPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(normalizedThrowDir.x, normalizedThrowDir.y, 0)).GetComponent<Rigidbody2D>();
        //             cardPrefab.ruleIndexToApply = playerHand[1];
        //             if (throwDir.x < 0.1f && throwDir.x > -0.1f && throwDir.y < 0.1f && throwDir.y > -0.1f)
        //             {
        //                 Debug.Log(playerStateInput.lastXDir);
        //                 cardRB.velocity = new Vector2(playerStateInput.lastXDir * cardThrowSpeed, 0);
        //             }
        //             else
        //             {
        //                 cardRB.velocity = normalizedThrowDir * cardThrowSpeed;
        //             }
        //             playerHand[1] = -1;
        //         }
        //         cardPrefab.playerIndexOfShooter = playerIndex;
        //     }
        // }
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Platform"))
    //     {
    //         transform.parent = collision.transform;
    //     }
    // }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.parent = null;
    }

    //Checks to see if it can take lawmaker status. It's called by the dash collider's script in OnTriggerEnter2D
    public void checkDashCollision(Collider2D collider) {
        // if (collider.gameObject.layer == 9) {
        //     if (playerManager.GetState() is PlayerDashState) {
        //         if (collider.gameObject.GetComponent<StatePlayerController>().isLawMaker)
        //         {
        //             collider.gameObject.GetComponent<StatePlayerController>().isLawMaker = false;
        //             isLawMaker = true;
        //         }
        //             Debug.Log("I work!");
        //     }
        // }
    }

    public void checkCrownCollision(Collider2D collider)
    {
        // isLawMaker = true;
        // Destroy(collider.gameObject);

        // Debug.Log("YOU ARE LAWMAKER.");
    }

}
