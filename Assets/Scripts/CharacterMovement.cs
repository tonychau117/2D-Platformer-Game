using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    
    public float movementSpeed = 2f;
    public int direction = 1;
    public PlayerInput playerInput;
    public Vector2 moveInput;

    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        Flip();
        AnimationController();
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

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void AnimationController()
    {
        anim.SetBool("Idle", Mathf.Abs(moveInput.x) < .1f);
        anim.SetBool("Running", Mathf.Abs(moveInput.x) > .1f);
    }
}
