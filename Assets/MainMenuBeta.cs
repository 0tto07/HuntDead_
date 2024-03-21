using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Make sure to include this namespace

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("HuntDeadMap1"); // Make sure your scene name is correct
    }
}
