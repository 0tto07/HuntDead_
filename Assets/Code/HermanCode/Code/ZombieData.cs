using System.Collections;
using UnityEngine;

public class ZombieData : MonoBehaviour
{
    public int hitPoints = 3;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {
            hitPoints--;
            if (hitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}