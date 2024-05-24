using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class GameManger : MonoBehaviour
{
    public GameObject gameOverUI;
    public AudioClip soundEffect;
    private AudioSource audioSource;

    [SerializeField] int currentLevel;
   
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
        StartCoroutine(LoadGameRoutineOptions());
    }
    public void Credits()
    {
        StartCoroutine(LoadGameRoutineCredits());
    }
    public void quit()
    {
        StartCoroutine(LoadGameRoutineQuit());
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void gameOver()
    {

        Debug.Log("THis is from gameManager and you are dead");
        if(currentLevel == 1)
        {
            SceneManager.LoadScene("DeathSceneLvl");
        }

        if (currentLevel == 2)
        {
            SceneManager.LoadScene("DeathSceneLvl2");
        }
        
    }
    public void LoadGame()
    {
        StartCoroutine(LoadGameRoutineLoadGame());

    }

    public void NextLvl()
    {
        StartCoroutine(LoadGameRoutineNextLvl());
    }

    IEnumerator LoadGameRoutineNextLvl()
    {
        Debug.Log("This happens");
        audioSource.Play();
        yield return new WaitForSeconds(2);
        Debug.Log("Then this happens");
        SceneManager.LoadScene("LevelsMap");
    }
    IEnumerator LoadGameRoutineLoadGame()
    {
        Debug.Log("This happens");
        audioSource.Play();
        yield return new WaitForSeconds(2);
        Debug.Log("Then this happens");

        SceneManager.LoadScene("HuntDeadMap1");


    }
    
    IEnumerator LoadGameRoutineOptions()
    {
        Debug.Log("This happens");
        audioSource.Play();
        yield return new WaitForSeconds(2);
        Debug.Log("Then this happens");

        SceneManager.LoadScene("Options");


    }
    IEnumerator LoadGameRoutineCredits()
    {
        Debug.Log("This happens");
        audioSource.Play();
        yield return new WaitForSeconds(2);
        Debug.Log("Then this happens");

        SceneManager.LoadScene("Credits");
    }
    IEnumerator LoadGameRoutineQuit()
    {
        Debug.Log("This happens");
        audioSource.Play();
        yield return new WaitForSeconds(2);
        Debug.Log("Then this happens");

        Application.Quit();
        Debug.Log("Quit");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
   
}
