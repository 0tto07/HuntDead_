using System.Collections;
using UnityEngine;

public class ZombieData : MonoBehaviour
{
    public int hitPoints = 3;
    Animator myAnimator;
    AudioManager myAudioManager;

    private void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();

        myAudioManager = GetComponent<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {

            myAnimator.SetTrigger("TakesDamage");
            hitPoints--;
            AudioManager.Instance.PlaySFX("ZombieDamage");
            if (hitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}