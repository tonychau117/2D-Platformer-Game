using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SettingsButton : MonoBehaviour
{
    // what happens when we click on the button
    public void loadSettings()
    {
        SceneManager.LoadScene("Settings"); // loads the settings scene
    }
}
