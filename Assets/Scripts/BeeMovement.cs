using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeeMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float amplitude = 2f; // height of the movement
    public float frequency = 1f; // speed of the movement

    public AudioSource audioSource;
    public Transform character;
    public float maxDist = 5f;
    Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    // this calcs the audio
    void Update()
    {
        float distance = Vector3.Distance(character.position, transform.position);
        float volume = Mathf.Clamp01(1 - (distance / maxDist));
        audioSource.volume = volume;
    }

    // Update is called once per frame
    // bee movement
    void FixedUpdate()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector2(transform.position.x, newY);
    }
}
