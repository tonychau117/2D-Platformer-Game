using UnityEngine;

public class Win : MonoBehaviour
{
    public AudioSource src;
    public AudioClip winSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // play winning sound on load
    void Start()
    {
        src.clip = winSound;
        src.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
