using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class PlayButton : MonoBehaviour
{
    // what happens when we click on the button
    public void loadGameLevel()
    {
        SceneManager.LoadScene("GameLevel"); // loads the game scene
    }
}
