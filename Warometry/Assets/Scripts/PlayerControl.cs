using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;
    //Dash & Jump
    public float jumpForce = 20f;
    public float doublejumpForce = 10f;
    public Transform feet;
    public LayerMask groundLayers;
    public float dashDistance = 10f;
    float distance;
    public bool canDoubleJump;
    public bool canDash;
    public bool isDashing;
    public bool jumped;
    //Wall Jump
    public Transform wallGrabPoint;
    private bool canGrab;
    bool isGrabbing;
    private float gravityStore;
    public float wallJumpTime = 0.2f;
    private float wallJumpCounter;
    public Camera cam;
    public float defaultFov = 90;

    float mx;
    float my;

    void Start()
    {
        gravityStore = rb.gravityScale;

        cam = Camera.main;
    }

    private void Update()
    {
        cam.orthographicSize = defaultFov;
        jumped = false;
        if (wallJumpCounter <= 0)
        {
            mx = Input.GetAxisRaw("Horizontal");
            my = rb.linearVelocity.y;

            if (IsGrounded())
            {
                canDoubleJump = true;
                canDash = true;
            }
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                Jump();
                jumped = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
                {
                    doubleJump();
                    canDoubleJump = false;
                    jumped = true;
                }
            }

            //Dash
            isDashing = false;
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                RaycastHit2D left = Physics2D.Raycast(transform.position, -transform.right, dashDistance, groundLayers);
                RaycastHit2D right = Physics2D.Raycast(transform.position, transform.right, dashDistance, groundLayers);
                if (left.collider != null)
                {
                    distance = Mathf.Abs(left.point.x - transform.position.x);
                    distance = -distance;
                }
                else if (right.collider != null)
                {
                    distance = Mathf.Abs(right.point.x - transform.position.x);
                }
                else
                {
                    distance = dashDistance;
                }
                isDashing = true;

                Debug.Log("Dashed");
                Dash();
                canDash = false;
            }
            //Wall Grab
            canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, .4f, groundLayers);
            isGrabbing = false;
            if (canGrab && !IsGrounded())
            {
                if ((transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal") > 0) || (transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") < 0))
                {
                    isGrabbing = true;
                    canDoubleJump = true;
                    canDash = true;
                }
            }
            //Wall Jump
            if (isGrabbing)
            {
                rb.gravityScale = 0f;
                rb.linearVelocity = Vector2.zero;

                if (Input.GetButtonDown("Jump"))
                {
                    isGrabbing = false;
                    wallJumpCounter = wallJumpTime;
                    mx = -Input.GetAxisRaw("Horizontal");

                    rb.linearVelocity = new Vector2(mx * movementSpeed, jumpForce);
                    rb.gravityScale = gravityStore;
                }
            }
            else
            {
                rb.gravityScale = gravityStore;
            }
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(mx * movementSpeed, rb.linearVelocity.y);

        rb.linearVelocity = movement;
    }

    void Jump()
    {
        Vector2 movement = new Vector2(rb.linearVelocity.x, jumpForce);

        rb.linearVelocity = movement;
    }
    void doubleJump()
    {
        Vector2 movement = new Vector2(rb.linearVelocity.x, doublejumpForce);

        rb.linearVelocity = movement;
    }

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 1f, groundLayers);

        if (groundCheck != null)
        {
            return true;
        }

        return false;
    }
    //Dash
    private void Dash()
    {
        if (mx >= 0f)
        {
            transform.position = new Vector2(transform.position.x + distance, transform.position.y);
        }
        else if (mx <= 0f)
        {
            transform.position = new Vector2(transform.position.x + -distance, transform.position.y);
        }
    }

}