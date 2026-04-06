using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HogMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movementSpeed = 2f;
    public int direction = 1;

    public AudioSource audioSource;
    public Transform character;
    public float maxDist = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    // this calcs the audio
    void Update()
    {
        float distance = Vector3.Distance(character.position, transform.position);
        float volume = Mathf.Clamp01(1 - (distance / maxDist));
        audioSource.volume = volume;
    }
    void FixedUpdate()
    {
        rb.linearVelocity = Vector2.right * movementSpeed * direction; // sets the hog speed
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collidedWith = coll.gameObject;

        // flip hog direction if he hits the rock
        if (collidedWith.CompareTag("Obstacle"))
        {
            direction = -direction; // change direction
            
        }
        transform.localScale = new Vector3(direction, 1, 1); // flips
    }


}
