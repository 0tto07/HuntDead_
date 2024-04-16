using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManger : MonoBehaviour
{
    public GameObject gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    } 
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void gameOver()
    {
      gameOverUI.SetActive(true);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("HuntDeadMap1");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
   
}
