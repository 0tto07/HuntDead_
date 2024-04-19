using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.SceneManagement;


public class GameManger : MonoBehaviour
{
    public GameObject gameOverUI;
    public AudioClip soundEffect;
    private AudioSource audioSource;
   
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        
        audioSource.clip = soundEffect;
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
        StartCoroutine(LoadGameRoutine());

    }


    IEnumerator LoadGameRoutine()
    {
        Debug.Log("This happens");
        audioSource.Play();
        yield return new WaitForSeconds(3);
        Debug.Log("Then this happens");

        SceneManager.LoadScene("HuntDeadMap1");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
