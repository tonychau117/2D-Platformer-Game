using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayButton : MonoBehaviour
{
    public AudioSource src;
    public AudioClip loadLevel;

    // what happens when we click on the button
    public void loadGameLevel()
    {
        // start the coroutine instead of loading immediately
        StartCoroutine(PlaySoundAndLoad());
    }

    private IEnumerator PlaySoundAndLoad()
    {
        src.clip = loadLevel;
        src.Play();

        // wait for the exact duration of the audio clip
        yield return new WaitForSeconds(loadLevel.length);

        // load the game scene after the wait is over
        SceneManager.LoadScene("GameLevel");
    }
}