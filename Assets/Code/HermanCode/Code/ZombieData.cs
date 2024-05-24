using System.Collections;
using UnityEngine;

public class ZombieData : MonoBehaviour
{
    public int hitPoints = 3;
    Animator myAnimator;
    AudioManager myAudioManager;
    private AudioSource SFX_ZombieDamage;

    private void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();
        SFX_ZombieDamage = GameObject.Find("SFX_ZombieDamage").GetComponent<AudioSource>();
        myAudioManager = GetComponent<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {

            myAnimator.SetTrigger("TakesDamage");
            hitPoints--;
            if (SFX_ZombieDamage != null)
            {
                SFX_ZombieDamage.Play();
            }
          
            if (hitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}