using System.Collections;
using UnityEngine;

public class ZombieData : MonoBehaviour
{
    public int hitPoints = 3;
    Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {

            myAnimator.SetTrigger("TakesDamage");
            hitPoints--;
            if (hitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}