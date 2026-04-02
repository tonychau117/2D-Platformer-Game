using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    
    public float movementSpeed = 2f;
    public int direction = 1;
    public float jump;

    public PlayerInput playerInput;
    public Vector2 moveInput;
    
    //
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isGrounded;

    public Animator anim;
    public Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        Flip();
        AnimationController();

        float clampX = Mathf.Clamp(transform.position.x, -screenBounds.x + .5f, screenBounds.x - .5f);
        float clampY = Mathf.Clamp(transform.position.y, -screenBounds.y, screenBounds.y - .5f);
        Vector2 pos = transform.position;
        pos.x = clampX;
        pos.y = clampY;
        transform.position = pos;
    }

    void FixedUpdate()
    {
        float targetSpeed = moveInput.x * movementSpeed;
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
    }

    void Flip()
    {
        if(moveInput.x > 0.1f)
        {
            direction = 1;
        }
        else if(moveInput.x < -0.1f)
        {
            direction = -1;
        }
        transform.localScale = new Vector3(direction, 1, 1);
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if(value.isPressed && isGrounded == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    void AnimationController()
    {
        anim.SetBool("Jump", Mathf.Abs(rb.linearVelocity.y) > .1f);
        anim.SetBool("Idle", Mathf.Abs(moveInput.x) < .1f);
        anim.SetBool("Running", Mathf.Abs(moveInput.x) > .1f);
    }
}
