using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SettingsButton : MonoBehaviour
{
    public AudioSource src;
    public AudioClip settings;

    // what happens when we click on the button
    public void loadSettings()
    {
        // Start the coroutine instead of loading immediately
        StartCoroutine(PlaySoundAndLoad());
    }

    private IEnumerator PlaySoundAndLoad()
    {
        src.clip = settings;
        src.Play();

        // Wait for the exact duration of the audio clip
        yield return new WaitForSeconds(settings.length);

        // Load the game scene after the wait is over
        SceneManager.LoadScene("Settings");
    }
}