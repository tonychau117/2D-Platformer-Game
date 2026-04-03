using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeeMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float amplitude = 2f; // Height of the movement
    public float frequency = 1f; // Speed of the movement
    Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector2(transform.position.x, newY);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collidedWith = coll.gameObject;
    }
}
