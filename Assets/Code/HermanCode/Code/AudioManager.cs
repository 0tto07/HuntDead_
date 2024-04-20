using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSoundCollection;
    public AudioSource musicSouce, sfxSource;

    private bool isRunningSoundPlaying = false;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        PlayMusic("BackGround");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
       
        if (s == null)
        
        {
            Debug.Log("Sound Not Found ");
        }

        else
        {
            musicSouce.clip=s.clip;
            musicSouce.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSoundCollection, x => x.name == name);

        if (s == null)

        {
            Debug.Log("Sound Not Found ");
        }

        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }

    }

    public void StopSoundEffect()
    {
        sfxSource.Stop();

    }


}