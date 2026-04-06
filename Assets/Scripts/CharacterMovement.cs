using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb; // rigidbody of character
    
    public float movementSpeed = 2f; // base movement speed
    public int direction = 1; // facing pos x axis
    public float jump; // jump amount

    public PlayerInput playerInput; // records player input
    public Vector2 moveInput; // movement input
    
    //
    public Transform groundCheck; // checks if we are on the ground
    public float groundCheckRadius; // circle to check if we are on the ground
    public LayerMask groundLayer; // layer of the ground
    private bool isGrounded; // t/f for on the ground

    public Animator anim; // animator to animate clips depending on state
    public Vector2 screenBounds; // screen bounds

    public AudioSource src; // source
    public AudioClip deathSound; // sound on death
    public AudioClip movementSound; // sound on movement
    public AudioClip jumpSound; // sound on jump

    public float footstepDelay = 0.3f;
    public float footstepTimer;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)); // sets screen grounds
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded(); // constantly checking if we are on ground
        Flip(); // constantly checking to see if char needs to be flipped
        AnimationController(); // controls animation state

        // ensuring that player stays in bounds of the screen
        float clampX = Mathf.Clamp(transform.position.x, -screenBounds.x + .5f, screenBounds.x - .5f);
        float clampY = Mathf.Clamp(transform.position.y, -screenBounds.y, screenBounds.y - .5f);
        Vector2 pos = transform.position;
        pos.x = clampX;
        pos.y = clampY;
        transform.position = pos;

        // esc to go back to start menu
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("StartMenu");
        }

        if (isGrounded && Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            footstepTimer -= Time.deltaTime; // cd

            if (footstepTimer <= 0)
            {
                src.PlayOneShot(movementSound);
                footstepTimer = footstepDelay; // reset
            }
        }
        else
        {
            // reset timer
            footstepTimer = 0f;
        }

    }

    void FixedUpdate()
    {
        float targetSpeed = moveInput.x * movementSpeed;
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y); // sets speed
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    // flips char based on dir
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

    // checks if we are on the ground via overlapcircle
    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // records move
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // prevents multi jump in air
    public void OnJump(InputValue value)
    {
        if(value.isPressed && isGrounded == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
            src.PlayOneShot(jumpSound);
        }        
    }

    // draw the overlapped circle to check ground
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    // animation controller
    void AnimationController()
    {
        if(isGrounded)
        {
            anim.SetBool("Idle", Mathf.Abs(moveInput.x) < .1f);
            anim.SetBool("Running", Mathf.Abs(moveInput.x) > .1f);
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Running", false);
        }
    }

    // on collision
    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collidedWith = coll.gameObject;

        // if char hits obstacle or enemy -> play sound & reload scene
        if (collidedWith.CompareTag("Obstacle") || collidedWith.CompareTag("Enemy"))
        {
            loadGameLevel();
        }
        // else if we reach the end -> play victory screen
        else if (collidedWith.CompareTag("Goal"))
        {
            SceneManager.LoadScene("Victory");
        }
    }

    public void loadGameLevel()
    {
        // start the coroutine instead of loading immediately
        StartCoroutine(PlaySoundAndLoad());
    }

    private IEnumerator PlaySoundAndLoad()
    {
        src.clip = deathSound;
        src.time = .2f;
        src.Play();

        // wait for the exact duration of the audio clip
        yield return new WaitForSeconds(.3f);

        // load the game scene after the wait is over
        SceneManager.LoadScene("GameLevel");
    }

}
