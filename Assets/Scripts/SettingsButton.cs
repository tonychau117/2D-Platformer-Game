using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class SettingsButton : MonoBehaviour
{
    public AudioSource src;
    public AudioClip settings;

    // what happens when we click on the button
    public void loadSettings()
    {
        // start the coroutine instead of loading immediately
        StartCoroutine(PlaySoundAndLoad());
    }

    private IEnumerator PlaySoundAndLoad()
    {
        src.clip = settings;
        src.Play();

        // wait for the exact duration of the audio clip
        yield return new WaitForSeconds(settings.length);

        // load the game scene after the wait is over
        SceneManager.LoadScene("Settings");
    }
}